using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public ChessBoard chessBoard
    {
        get;
        set;
    }

    public TeamColour playerColour
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

    public Player(ChessBoard board, TeamColour teamColour)
    {
        activePlayerPieces = new List<Piece>();
        this.chessBoard = board;
        this.playerColour = teamColour;
    }

    // Add the available pieces in the active player's pieces list
    public void AddActivePiece(Piece piece)
    {

        if (!activePlayerPieces.Contains(piece))
        {// the list does not contain the given piece
            activePlayerPieces.Add(piece);// add the piece to the list
        }
    }

    // Remove the piece if it already exists
    public void RemovePiece(Piece piece)
    {
        if (activePlayerPieces.Contains(piece))// list contains the given piece
        {
            activePlayerPieces.Remove(piece);// remove the piece from the list
        }
    }

    public void CreatePossibleMoves()
    {
        //for (int i = 0; i < activePlayerPieces.Count; i++)// iterate through the list
        //{
        //    if (ChessBoard.alreadyContains(activePlayerPieces[i]))
        //    {// piece is on the chess board
        //        activePlayerPieces[i].SelectAvailableCells();// select the cells available
        //    }
        //}
        foreach (var p in activePlayerPieces)
        {
            if (chessBoard.AlreadyContains(p))
            {
                p.SelectAvailableSquares();
            }
        }
    }
}
