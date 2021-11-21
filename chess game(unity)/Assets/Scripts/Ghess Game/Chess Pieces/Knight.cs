using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    public override List<Vector2Int> SelectAvailableSquares()
    {
        availableMoves.Clear();
        availableMoves.Add(occupiedSquare + new Vector2Int(0, 1));
        return availableMoves;
    }
}
