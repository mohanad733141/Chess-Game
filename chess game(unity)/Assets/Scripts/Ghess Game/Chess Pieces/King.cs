using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    Vector2Int[] moves = new Vector2Int[]
	{
		new Vector2Int(-1, 1),
		new Vector2Int(0, 1),
		new Vector2Int(1, 1),
		new Vector2Int(-1, 0),
		new Vector2Int(1, 0),
		new Vector2Int(-1, -1),
		new Vector2Int(0, -1),
		new Vector2Int(1, -1),
	};

    private Vector2Int leftCastlingMove;
    private Vector2Int rightCastlingMove;
    private Piece leftRook;
    private Piece rightRook;

    public override List<Vector2Int> SelectAvailableSquares()
    {
        applicableChessMoves.Clear();
        AssignStandardMoves();
        AssignCastlingMoves(); 
        return applicableChessMoves;
    }

    private void AssignCastlingMoves()
    {
        if (hasMoved)
            return;

        leftRook = GetPieceInDirection<Rook>(team, Vector2Int.left);
        if(leftRook && !leftRook.hasMoved)
        {
            leftCastlingMove = unavaliableSquare + Vector2Int.left * 2;
            applicableChessMoves.Add(leftCastlingMove);
        }

        rightRook = GetPieceInDirection<Rook>(team, Vector2Int.right);
        if (rightRook && !rightRook.hasMoved)
        {
            rightCastlingMove = unavaliableSquare + Vector2Int.right * 2;
            applicableChessMoves.Add(rightCastlingMove);
        }
    }

    private void AssignStandardMoves()
    {
        float chessBoardRange = ChessBoard.CHESS_BRD_SIZE;
        foreach (var kingMovesList in moves)
        {
            for (int i = 1; i < chessBoardRange; i++)
            {

                Vector2Int followingCoordinates = unavaliableSquare + kingMovesList * i;
                Piece ChessPiece = board.GetPieceOnCell(followingCoordinates);
                if (!board.WithinBounds(followingCoordinates))
                    break;
                if (ChessPiece == null)
                    TryToAddMove(followingCoordinates);
                else if (!ChessPiece.IsFromSameTeam(this))
                {
                    TryToAddMove(followingCoordinates);
                    break;
                }
                else if (ChessPiece.IsFromSameTeam(this))
                    break;
            }
        }
    }

    public override void MoveChessPiece(Vector2Int coords)
    {
        base.MoveChessPiece(coords);
        if(coords == leftCastlingMove)
        {
            board.MovePiecesOnBoard(coords + Vector2Int.right, leftRook.unavaliableSquare, leftRook, null);
            leftRook.MoveChessPiece(coords + Vector2Int.right);
        } else if(coords == rightCastlingMove)
        {
            board.MovePiecesOnBoard(coords + Vector2Int.left, rightRook.unavaliableSquare, rightRook, null);
            rightRook.MoveChessPiece(coords + Vector2Int.left);
        }
    }
}
