using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    public override List<Vector2Int> SelectAvailableCells()
    {
<<<<<<< HEAD
        movesAvailable.Clear();
=======
         movesAvailable.Clear();
>>>>>>> parent of f9e4564 (Revert "trying to fix some errors")
        movesAvailable.Add(unavailableCell + new Vector2Int(0, 1));
        return movesAvailable;
    }
}
