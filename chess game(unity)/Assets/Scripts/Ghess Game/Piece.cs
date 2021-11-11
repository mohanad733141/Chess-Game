using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(MaterialSetter))]

//[RequireComponent(typeof(IObjectTweener))] 
public abstract class Piece : MonoBehaviour
{
    public List<Vector2Int> movesAvailable;
    private MaterialSetter matSetter;
    

    public Vector2Int unavailableCell
    {
        get;
        set;
    }

    public ChessBoard chessBoard
    {
        protected get;
        set;
    }

    public bool piecemoved
    {
        get;
        private set;
    }

    public TeamColour teamColour
    {
        get;
        set;
    }

    //  Implemented in later on in the next parts(used to move the pieces)
    //private IObjectTweener tweener;

    public abstract List<Vector2Int> SelectAvailableCells();

    private void Awake()
    {
        //  Implemented in later on in the next parts
        // tweener = GetComponent<IObjectTweener>();
        matSetter = GetComponent<MaterialSetter>();
        movesAvailable = new List<Vector2Int>();
        piecemoved = false;
    }

    public void SetTeamMaterial(Material m)
    {
        if (matSetter == null)
            matSetter = GetComponent<MaterialSetter>();
        matSetter.setMat(m);
    }

    public bool sameTeam(Piece p)
    {
        return teamColour == p.teamColour;
    }

    // Check if the passed coordinate exists in the list
    public bool AbleToMoveTo(Vector2Int coordinates)
    {
        return movesAvailable.Contains(coordinates);
    }

    public virtual void MoveChessPiece(Vector2Int coordinates)
    {

    }

    // Add the coordinates to the list
    protected void AddMove(Vector2Int coordinates)
    {
        movesAvailable.Add(coordinates);
    }

    public void SetData(Vector2Int coordinates, TeamColour teamColour, ChessBoard chessBoard)
    {
        this.teamColour = teamColour;
        unavailableCell = coordinates;
        this.chessBoard = chessBoard;
        transform.position = chessBoard.CalculateLoctionAtCoordinates(coordinates);


    }

}
