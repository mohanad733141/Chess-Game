using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PieceCreator))]
public class GameController : MonoBehaviour
{
    [SerializeField]private ChessBoardLayout boardLayout;
     
    private PieceCreator pieceCreator;
    [SerializeField]private ChessBoard ChessBoard;

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
            TeamColour team = boardLayout.GetSquareColourAtPosition(i);
            string name = boardLayout.GetSquarePieceNameAtPosition(i);

            Type type = Type.GetType(name);
            initializePieces(squareCoordinates, team, type);
        }
    }

    private void initializePieces(Vector2Int squareCoordinates, TeamColour teamColour, Type type)
    {
        Piece piece = pieceCreator.CreatePiece(type).GetComponent<Piece>();
        piece.SetData(squareCoordinates, teamColour, ChessBoard);

        Material teamMat = pieceCreator.getTeamMaterial(team);
        piece.SetMaterial(teamMat);
    }
   //might change piececreator to piecemaker
}



