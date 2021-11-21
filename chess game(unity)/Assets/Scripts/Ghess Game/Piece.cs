using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MaterialSetter))]
[RequireComponent(typeof(IObjectTweener))]
public abstract class Piece : MonoBehaviour
{
    public List<Vector2Int> availableMoves;
    private MaterialSetter materialSetter;

    public Vector2Int occupiedSquare
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
        materialSetter = GetComponent<MaterialSetter>();
        availableMoves = new List<Vector2Int>();
        hasMoved = false;
    }

    public void SetTeamMaterial(Material material)
    {
        if (materialSetter == null)
            materialSetter = GetComponent<MaterialSetter>();
        materialSetter.SetSingleMaterial(material);
    }

    public bool IsFromSameTeam(Piece piece)
    {
        return team == piece.team;
    }

    // Check if the passed coordinate exists in the list
    public bool CanMoveTo(Vector2Int coords)
    {
        return availableMoves.Contains(coords);
    }

    public virtual void MovePiece(Vector2Int coords)
    {
        Vector3 targetPosition = board.CalcPosFromCoords(coords);// calculate the position
        occupiedSquare = coords;// cell is now unavailable after the player has moved there
        hasMoved = true;
        tweener.MoveTo(transform, targetPosition);
    }

    // Add the coordinates to the list
    protected void TryToAddMove(Vector2Int coords)
    {
        availableMoves.Add(coords);
    }

    public void assignData(Vector2Int coords, TeamColour team, ChessBoard board)
    {
        this.team = team;
        occupiedSquare = coords;
        this.board = board;
        transform.position = board.CalcPosFromCoords(coords);
    }

}
