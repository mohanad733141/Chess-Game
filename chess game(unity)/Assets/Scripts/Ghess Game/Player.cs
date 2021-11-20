using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public ChessBoard ChessBoard
    {
        get;
        set;
    }

    public TeamColour team
    {
        get;
        set;
    }

    // List containing pieces of the active player's team
    public List<Piece> activePlayerPieces
    {
        get;
        private set;
    }

    public Player(ChessBoard chessBoard, TeamColour team)
    {
        activePlayerPieces = new List<Piece>();
        this.ChessBoard = chessBoard;
        this.team = team;
    }

    // Add the available pieces in the active player's pieces list
    public void addActivePiece(Piece p)
    {

        if (!activePlayerPieces.Contains(p))
        {// the list does not contain the given piece
            activePlayerPieces.Add(p);// add the piece to the list
        }
    }

    // Remove the piece if it already exists
    public void removeActivePiece(Piece p)
    {
        if (activePlayerPieces.Contains(p))// list contains the given piece
        {
            activePlayerPieces.Remove(p);// remove the piece from the list
        }
    }

    public void createPossibleMoves()
    {
        //for (int i = 0; i < activePlayerPieces.Count; i++)// iterate through the list
        foreach (var p in activePlayerPieces )
        {
            if (ChessBoard.alreadyContains(p))
            {// piece is on the chess board
                p.SelectAvailableCells();// select the cells available
            }
        }
    }
}
