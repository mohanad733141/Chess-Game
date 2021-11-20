using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PieceCreator))]
public class GameController : MonoBehaviour
{
    [SerializeField] private ChessBoardLayout boardLayout;

    private PieceCreator pieceMaker;
    // 2 players for the game
    private Player playerWhite;
    private Player playerBlack;
    // active player making the move
    private Player playerActive;
    [SerializeField] private ChessBoard ChessBoard;

    private void Awake()
    {
        setDependencies();
        setUpPlayers();
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
        ChessBoard.SetDependencies(this);
        makePieces(boardLayout);
        playerActive = playerWhite;// white player is the first player to choose a move
        createPossibleMoves(playerActive);
    }

    // Create the players
    private void setUpPlayers()
    {
        playerWhite = new Player(ChessBoard, TeamColour.White);
        playerBlack = new Player(ChessBoard, TeamColour.Black);
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

    public bool isFromActivePlayer(TeamColour team)
    {
        //if(playerActive.team == team)
        // {
        //     return true;
        // }
        // return false;
        return playerActive.team == team;
    }

    private void initializePieces(Vector2Int squareCoordinates, TeamColour teamColour, Type type)
    {
        Piece newChessPiece = pieceMaker.MakeNewPieces(type).GetComponent<Piece>();
        newChessPiece.assignData(squareCoordinates, teamColour, ChessBoard);

        Material tMat = pieceMaker.getEachPlayerMaterial(teamColour);
        newChessPiece.SetTeamMaterial(tMat);
        ChessBoard.setPiecesOnBoard(squareCoordinates, newChessPiece);// move the piece to the new location
        //Player playerCurrent = null;
        //if (teamColour == TeamColour.White)
        //{
        //    playerCurrent = playerWhite;
        //}
        //else if (teamColour == TeamColour.Black)
        //{
        //    playerCurrent = playerBlack;
        //}
        //playerCurrent.addActivePiece(newChessPiece);
        Player playerCurrent = teamColour == TeamColour.White ? playerWhite : playerBlack;
        playerCurrent.addActivePiece(newChessPiece);
    }

    public void completeTurn()
    {
        createPossibleMoves(playerActive);
        createPossibleMoves(changeTurn(playerActive));
        nextPlayerTurn();
    }

    // change the active player
    private void nextPlayerTurn()
    {
        //if (playerActive == playerBlack)
        //{
        //    playerActive = playerWhite;
        //}
        //else if (playerActive == playerWhite)
        //{
        //    playerActive = playerBlack;
        //}
        playerActive = playerActive == playerWhite ? playerBlack : playerWhite;
    }

    // Change the active player once they had their turn
    private Player changeTurn(Player playerActive)
    {
        //if(playerActive == playerWhite)
        // {
        //     playerActive = playerBlack;
        // } else if(playerActive == playerBlack)
        // {
        //     playerActive = playerWhite;
        // }
        // return playerActive;
        return playerActive == playerWhite ? playerBlack : playerWhite;
    }

    // create the possible moves for the given player
    private void createPossibleMoves(Player p)
    {
        p.createPossibleMoves();
        
    }
    

}