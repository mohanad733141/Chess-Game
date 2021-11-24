using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    Vector2Int[] moves = new Vector2Int[]
	{
		new Vector2Int(2, 1),
		new Vector2Int(2, -1),
		new Vector2Int(1, 2),
		new Vector2Int(1, -2),
		new Vector2Int(-2, 1),
		new Vector2Int(-2, -1),
		new Vector2Int(-1, 2),
		new Vector2Int(-1, -2),
	};

	public override List<Vector2Int> SelectAvailableSquares()
	{
		applicableChessMoves.Clear();

		for (int i = 0; i < moves.Length; i++)
		{
			Vector2Int followingCoordinates = unavaliableSquare + moves[i];
			Piece ChessPiece = board.GetPieceOnCell(followingCoordinates);
			if (!board.WithinBounds(followingCoordinates)){
				continue;
            }
			if (ChessPiece == null || !ChessPiece.IsFromSameTeam(this)){
				TryToAddMove(followingCoordinates);
		}
        }
		return applicableChessMoves;
	}
}
