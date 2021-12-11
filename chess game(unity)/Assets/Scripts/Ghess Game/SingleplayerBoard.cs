using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleplayerBoard : ChessBoard
{

    public override void SelectPieceMoved(Vector2 coords)
    {
        Vector2Int intCoords = new Vector2Int(Mathf.RoundToInt(coords.x), Mathf.RoundToInt(coords.y));
        MoveSelected(intCoords);
    }

    public override void SetSelectedPiece(Vector2 coords)
    {
        Vector2Int intCoords = new Vector2Int(Mathf.RoundToInt(coords.x), Mathf.RoundToInt(coords.y));
        OnSetSelectedPiece(intCoords);


    }
}
