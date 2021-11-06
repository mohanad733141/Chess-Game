using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PieceCreator))]
public class GameController : MonoBehaviour
{
    [System.Serializable] private ChessBoardLayout boardLayout;

    private PieceCreator pieceCreator;

    private void Awake()
    {
        setDependencies();
    }

    private void setDependencies()
    {
        pieceCreator = GetComponent<PieceCreator>();
    }

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
            initializePieces(squareCoordinates, team, type);
        }
    }

    private void initializePieces(Vector2Int squareCoordinates, TeamColor team, Type type)
    {
        Piece piece = pieceCreator.CreatePiece(type).GetComponent<pieceCreator>();
        piece.SetData(squareCoordinates, team, boardLayout);
    }

}

