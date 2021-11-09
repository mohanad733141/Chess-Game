using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MaterialSetter))]

[RequireComponent(typeof(IObjectTweener))] 
public abstract class Piece
{
    public List<Vector2Int> movesAvailable;
    private MaterialSetter materialSetter;

    public Vector2Int unavailableCell {
        get;
        set;
    }
    
    public ChessBoard chessBoard { 
        protected get; 
        set;
    }

    public bool moved {
        get;
        private set;
    }

    public TeamColour team {
        get;
        set;
    }

//  Implemented in later on in the next parts
    // private IObjectTweener tweener;

    public abstract List<Vector2Int> SelectAvailableCells();

    private void Awake() {
        //  Implemented in later on in the next parts
        // tweener = GetComponent<IObjectTweener>();
        materialSetter = GetComponent<MaterialSetter>();
        movesAvailable = new List<Vector2Int>();
        moved = false;
    }

    public void SetMaterial(Material m) {
        MaterialSetter.setMat(m);
    }

    public bool sameTeam(Piece p) {
        return team == piece.team;
    }

    // Check if the passed coordinate exists in the list
    public bool canMoveTo(Vector2Int coordinates) {
        return movesAvailable.Contains(coordinates);
    }

    public virtual void MovePiece(Vector2Int coordinates) {

    }

    // Add the coordinates to the list
    protected void addMove(Vector2Int coordinates) {
        movesAvailable.Add(coordinates);
    }

    public void SetData(Vector2Int coordinates, TeamColour team, ChessBoard chessBoard) {
        this.team = team;
        unavailableCell = coordinates;
        this.chessBoard = chessBoard;
        transform position = chessBoard.CalculatePositionFromCoordinates(coordinates);
    }

}
