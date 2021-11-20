using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    public override List<Vector2Int> SelectAvailableCells()
    {
         movesAvailable.Clear();
        movesAvailable.Add(unavailableCell + new Vector2Int(0, 1));
        return movesAvailable;
    }
}
