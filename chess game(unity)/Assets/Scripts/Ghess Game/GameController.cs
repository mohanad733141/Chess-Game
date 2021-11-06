using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        newGame();
    }

    private void newGame()
    {
        makePieces(boardLayout);
    }

    private void makePieces(ChessBoardLayout boardLayout)
    {
        for (int i = 0; i < boardLayout.GetPiecesNum(); i++)
        {
            Vector2Int squareCoordinates = boardLayout.GetSquareLocationAtPosition(i);
            TeamColor team = boardLayout.GetSquareColourAtPosition(i);
            string name = boardLayout.GetSquarePieceNameAtPosition(i);

            Type type = Type.getType(typeName);

        }
    }
}
