using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public ChessBoard board
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
    public List<Piece> activePieces
    {
        get;
        private set;
    }

    public Player(ChessBoard board, TeamColour teamColour)
    {
        activePieces = new List<Piece>();
        this.board = board;
        this.team = teamColour;
    }

    // Add the available pieces in the active player's pieces list
    public void AddActivePiece(Piece piece)
    {

        if (!activePieces.Contains(piece))
        {// the list does not contain the given piece
            activePieces.Add(piece);// add the piece to the list
        }
    }

    // Remove the piece if it already exists
    public void RemovePiece(Piece piece)
    {
        if (activePieces.Contains(piece))// list contains the given piece
        {
            activePieces.Remove(piece);// remove the piece from the list
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
        foreach (var piece in activePieces)
        {
            if (board.AlreadyContains(piece))
            {
                piece.SelectAvailableSquares();
            }
        }
    }
}
