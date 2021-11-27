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

    public Piece[] GetPiecesAttackingOpponentPieceOfType<T>() where T: Piece
    {
        return activePlayerPieces.Where(p => p.IsAttackingPieceOfType<T>()).ToArray();
    }



    public Piece[] GetPiecesOfType<T>() where T : Piece
    {
        return activePlayerPieces.Where(p => p is T).ToArray();
    }

    public void RemovesMovesEnablingAttackOnPieces<T>(Player opponent, Piece pieceSelected) where T: Piece 
    {
        List<Vector2Int> coordsToRemove = new List<Vector2Int>();
        foreach(var coords in pieceSelected.applicableChessMoves)
        {
            Piece GetPieceOnCell = chessBoard.GetPieceOnCell(coords);
            chessBoard.MovePiecesOnBoard(coords, pieceSelected.unavaliableSquare, pieceSelected, null);
            opponent.CreatePossibleMoves();
            if (opponent.CheckIfIsAttackingPiece<T>())
                coordsToRemove.Add(coords);
        }
        foreach(var coords in coordsToRemove)
        {
            pieceSelected.avaiableMoves.Remove(coords);
        }
    
    }

    private bool CheckIfIsAttackingPiece<T>() where T:Piece
    {
        foreach(var piece in activePlayerPieces)
        {
            if (chessBoard.AlreadyContains(piece) && piece.IsAttackingPieceOfType<T>())
                return true;
        }
        return false;
    }

    public bool CanHidePieceFromAttack<T>(Player opponent) where T:Piece
    {
        foreach(var piece in activePlayerPieces)
        {
            foreach(var coords in piece.applicableChessMoves)
            {
                Piece pieceOnCoords = chessBoard.GetPieceOnCell(coords);
                chessBoard.MovePiecesOnBoard(coords, piece.unavaliableSquare, piece, null);
                opponent.CreatePossibleMoves();
                if(!opponent.CheckIfIsAttackingPiece<T>())
                {
                    chessBoard.MovePiecesOnBoard(piece.unavaliableSquare, coords, piece, pieceOnCoords);
                    return true;
                }
                chessBoard.MovePiecesOnBoard(piece.unavaliableSquare, coords, piece, pieceOnCoords);
            }

        }
        return false;
    }





}
