using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Board/Layout")]
public class ChessBoardLayout : ScriptableObject
{
    [System.Serializable]
    private class ChessSquareSetup
    {
        public Vector2Int location;
        public TeamColour playerColour;
        public PieceType PieceType;
    }

    [SerializeField] private ChessSquareSetup[] ChessSquares;

    public Vector2Int GetSquareLocationAtPosition(int position)
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



    public TeamColour GetSquareColourAtPosition(int position)
    {
        if (ChessSquares.Length <= position)
        {
            Debug.LogError("index of piece is outside the chessboard boundaries ");
            return TeamColour.Black;
        }
        return ChessSquares[position].playerColour;
    }

    public string GetSquarePieceNameAtPosition(int position)
    {
        if (ChessSquares.Length <= position)
        {
            Debug.LogError("index of piece is outside the board boundaries ");
            return "";
        }
        return ChessSquares[position].PieceType.ToString();
    }
}
