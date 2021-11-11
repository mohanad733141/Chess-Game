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

    [SerializeField] private ChessSquareSetup[] ChessSquares;

    public Vector2Int GetBoxLocationAtPosition(int position)
    {

        if (ChessSquares.Length <= position)
        {
            Debug.LogError("index of piece is outside the board boundaries ");
            return new Vector2Int(-1, -1);
        }
        return new Vector2Int(ChessSquares[position].location.x - 1, ChessSquares[position].location.y - 1);

    }

    public int GetPiecesNum()
    {

        return ChessSquares.Length;

    }



    public TeamColour GetBoxColourAtPosition(int position)
    {
        if (ChessSquares.Length <= position)
        {
            Debug.LogError("index of piece is outside the chessboard boundaries ");
            return TeamColour.Black;
        }
        return ChessSquares[position].playerColour;
    }

    public string GetBoxPieceNameAtPosition(int position)
    {
        if (ChessSquares.Length <= position)
        {
            Debug.LogError("index of piece is outside the board boundaries ");
            return "";
        }
        return ChessSquares[position].PieceType.ToString();
    }
}
