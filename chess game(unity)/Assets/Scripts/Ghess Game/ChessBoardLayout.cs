using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Scriptable Objects/Board/Layout")]
public class ChessBoardLayout :  ScriptableObject
{
    [Serializable]
    private class ChessSquareSetup
    {
        public Vector2Int location;
        public TeamColour playerColour;
        public PieceType PieceType;
    }

    [SerializeField] private ChessSquareSetup[] ChessCells;

    public Vector2Int GetBoxLocationAtPosition(int position)
    {

        if (ChessCells.Length <= position)
        {
            Debug.LogError("index of piece is outside the board boundaries ");
            return new Vector2Int(-1, -1);
        }
        return new Vector2Int(ChessCells[position].location.x - 1, ChessCells[position].location.y - 1);

    }

    public int GetPiecesNum()
    {

        return ChessCells.Length;

    }



    public TeamColour GetBoxColourAtPosition(int location)
    {
        if (ChessCells.Length <= location)
        {
            Debug.LogError("index of piece is outside the chessboard boundaries ");
            return TeamColour.Black;
        }
        return ChessCells[location].playerColour;
    }

    public string GetBoxPieceNameAtPosition(int location)
    {
        if (ChessCells.Length <= location)
        {
            Debug.LogError("index of piece is outside the board boundaries ");
            return "";
        }
        return ChessCells[location].PieceType.ToString();
    }
}
