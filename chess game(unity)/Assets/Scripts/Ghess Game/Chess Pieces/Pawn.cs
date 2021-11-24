using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
   public override List<Vector2Int> SelectAvailableSquares()
    {
        applicableChessMoves.Clear();

        Vector2Int pawnDirection = team == TeamColour.White ? Vector2Int.up : Vector2Int.down;
        float chessBoardRange = hasMoved ? 1 : 2;
        for (int i = 1; i <= chessBoardRange; i++)
        {
            Vector2Int followingCoordinates = unavaliableSquare + pawnDirection * i;
            Piece chessPiece = board.GetPieceOnCell(followingCoordinates);
            if (!board.WithinBounds(followingCoordinates))
                break;
            if (chessPiece == null)
                TryToAddMove(followingCoordinates);
            else 
                break;
        }
      
        Vector2Int[] followNewDirection = new Vector2Int[] { new Vector2Int(1, pawnDirection.y), new Vector2Int(-1, pawnDirection.y) };
        for (int i = 0; i < followNewDirection.Length; i++)
        {
            Vector2Int followingCoordinates = unavaliableSquare + followNewDirection[i];
            Piece chessPiece = board.GetPieceOnCell(followingCoordinates);
            if (!board.WithinBounds(followingCoordinates))
                break;
            if (chessPiece != null && !chessPiece.IsFromSameTeam(this))
            {
                TryToAddMove(followingCoordinates);
            }
        }
        return applicableChessMoves;
    }  
}