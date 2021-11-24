using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
     private Vector2Int[] directions = new Vector2Int[]
    {
        new Vector2Int(1, 1),
        new Vector2Int(1, -1),
        new Vector2Int(-1, 1),
        new Vector2Int(-1,- 1),
    };
public override List<Vector2Int> SelectAvailableSquares()
    {
        applicableChessMoves.Clear();
        float chessBoardRange = ChessBoard.CHESS_BRD_SIZE;
        foreach (var direction in directions)
        {
            for (int i = 1; i < chessBoardRange; i++)
            {

                Vector2Int followingCoordinates = unavaliableSquare + direction * i;
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
        return applicableChessMoves;
    }
}
