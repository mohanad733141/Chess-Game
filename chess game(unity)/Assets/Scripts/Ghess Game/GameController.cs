using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PieceCreator))]
public class GameController : MonoBehaviour
{
    [SerializeField] private ChessBoardLayout boardLayout;

    private PieceCreator pieceMaker;
    [SerializeField] private ChessBoard ChessBoard;

    private void Awake()
    {
        setDependencies();
    }

    private void setDependencies()
    {
        pieceMaker = GetComponent<PieceCreator>();
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
            Vector2Int squareCoordinates = boardLayout.GetBoxLocationAtPosition(i);
            TeamColour playerColour = boardLayout.GetBoxColourAtPosition(i);
            string name = boardLayout.GetBoxPieceNameAtPosition(i);

            Type pieceType = Type.GetType(name);
            initializePieces(squareCoordinates, playerColour, pieceType);
        }
    }

    private void initializePieces(Vector2Int squareCoordinates, TeamColour teamColour, Type type)
    {
        Piece newChessPiece = pieceMaker.MakeNewPieces(type).GetComponent<Piece>();
        newChessPiece.assignData(squareCoordinates, teamColour, ChessBoard);

        Material tMat = pieceMaker.getEachPlayerMaterial(teamColour);
        newChessPiece.SetTeamMaterial(tMat);
    }
   
}



