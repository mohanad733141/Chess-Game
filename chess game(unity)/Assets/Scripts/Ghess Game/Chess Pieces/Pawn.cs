using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    public override List<Vector2Int> SelectAvailableSquares()
    {
        availableMoves.Clear();
        availableMoves.Add(unavaliableSquare + new Vector2Int(0, 1));
        return availableMoves;
    }
}
