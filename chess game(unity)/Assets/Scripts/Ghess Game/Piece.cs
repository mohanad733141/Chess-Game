using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MaterialSetter))]
[RequireComponent(typeof(IObjectTweener))]
public abstract class Piece : MonoBehaviour
{
    public List<Vector2Int> applicableChessMoves;
    private MaterialSetter matSetter;

    public Vector2Int unavaliableSquare
    {
        get;  
        set;
    }

    public ChessBoard board
    {
        protected get;
        set;
    }

    public bool hasMoved
    {
        get;
        private set;
    }

    public TeamColour team
    {
        get;
        set;
    }

    private IObjectTweener tweener;

    public abstract List<Vector2Int> SelectAvailableSquares();

    private void Awake()
    {
        tweener = GetComponent<IObjectTweener>();
        matSetter = GetComponent<MaterialSetter>();
        applicableChessMoves = new List<Vector2Int>();
        hasMoved = false;
    }

    public void SetTeamMaterial(Material material)
    {
        if (matSetter == null)
            matSetter = GetComponent<MaterialSetter>();
        matSetter.SetSingleMaterial(material);
    }

    public bool IsFromSameTeam(Piece piece)
    {
        return team == piece.team;
    }

    // Check if the passed coordinate exists in the list
    public bool CanMoveTo(Vector2Int coords)
    {
        return applicableChessMoves.Contains(coords);
    }

    public virtual void MoveChessPiece(Vector2Int coords)
    {
        Vector3 targetPosition = board.CalcPosFromCoords(coords);// calculate the position
        unavaliableSquare = coords;// cell is now unavailable after the player has moved there
        hasMoved = true;
        tweener.MoveTo(transform, targetPosition);
    }

    // Add the coordinates to the list
    protected void TryToAddMove(Vector2Int coords)
    {
        applicableChessMoves.Add(coords);
    }

    public void assignData(Vector2Int coords, TeamColour team, ChessBoard board)
    {
        this.team = team;
        unavaliableSquare = coords;
        this.board = board;
        transform.position = board.CalcPosFromCoords(coords);
    }

    public bool IsAttackingPieceOfType<T>() where T : Piece
    {
        foreach(var square in applicableChessMoves)
        {
            if (board.GetPieceOnCell(square) is T)
                return true;
        }
        return false;
    }

    protected Piece GetPieceInDirection<T>(TeamColour team, Vector2Int direction) where T : Piece
    {
        for(int i = 1; i < ChessBoard.CHESS_BRD_SIZE; i++)
        {
            Vector2Int nextCoords = unavaliableSquare + direction * i;
            Piece piece = board.GetPieceOnCell(nextCoords);
            if (!board.WithinBounds(nextCoords))
                return null;
            if(piece != null)
            {
                if (piece.team != team || !(piece is T))
                    return null;
                else if (piece.team == team && piece is T)
                    return piece;
            }
        }

        return null;
    }
}
