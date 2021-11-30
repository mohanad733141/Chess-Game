using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PieceCreator))]
public class GameController : MonoBehaviour
{
    [SerializeField] private ChessBoardLayout brdLayout;

    private PieceCreator pieceMaker;
    // 2 players for the game
    private Player playerWhite;
    private Player playerBlack;
    // active player making the move
    private Player playerActive;
    [SerializeField] private ChessBoard brd;

    private void Awake()
    {
        SetDependencies();
        SetUpPlayers();
    }

    private void SetDependencies()
    {
        pieceMaker = GetComponent<PieceCreator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        brd.SetDependencies(this);
        MakePieces(brdLayout);
        playerActive = playerWhite;// white player is the first player to choose a move
        CreatePossibleMoves(playerActive);
<<<<<<< HEAD
        SetGameState(GameState.Play);
    }

    private void SetGameState(GameState state)
    {
        this.state = state;
    }

    public bool IsGameInProgress()
    {
        return state == GameState.Play;
=======
>>>>>>> parent of e47a884 (gamestate)
    }

    /* 
     * Create the players
     */
    private void SetUpPlayers()
    {
        playerWhite = new Player(brd, TeamColour.White);
        playerBlack = new Player(brd, TeamColour.Black);
    }

    /*
     * Generate the pieces
     */
    private void MakePieces(ChessBoardLayout brdLayout)
    {
        for (int i = 0; i < brdLayout.GetPiecesNum(); i++)
        {
            Vector2Int squareCoordinates = brdLayout.GetBoxLocationAtPosition(i);
            TeamColour playerColour = brdLayout.GetBoxColourAtPosition(i);
            string name = brdLayout.GetBoxPieceNameAtPosition(i);

            Type pieceType = Type.GetType(name);
            InitializePieces(squareCoordinates, playerColour, pieceType);
        }
    }

    public bool IsFromActivePlayer(TeamColour team)
    {
        if (playerActive.playerColour == team)
        {
            return true;
        }
        return false;
    }

    private void InitializePieces(Vector2Int squareCoordinates, TeamColour teamColour, Type type)
    {
        Piece newChessPiece = pieceMaker.MakeNewPieces(type).GetComponent<Piece>();
        newChessPiece.assignData(squareCoordinates, teamColour, brd);

        Material tMat = pieceMaker.getEachPlayerMaterial(teamColour);
        newChessPiece.SetTeamMaterial(tMat);
        brd.PlacePiecesOnBoard(squareCoordinates, newChessPiece);// move the piece to the new location
        Player playerCurrent = null;
        if (teamColour == TeamColour.White)
        {
            playerCurrent = playerWhite;
        }
        else if (teamColour == TeamColour.Black)
        {
            playerCurrent = playerBlack;
        }
        playerCurrent.AddActivePiece(newChessPiece);
    }

    /*
     * Finish the turn for the player who just had their turn
     */
    public void CompleteTurn()
    {
        CreatePossibleMoves(playerActive);
        CreatePossibleMoves(ChangeTurn(playerActive));
<<<<<<< HEAD
        if (CheckIfGameIsFinished())
            EndGame();
        else
            NextPlayerTurn();
    }

    private bool CheckIfGameIsFinished()
    {
        Piece[] kingAttackingPieces = playerActive.GetPiecesAttackingOpponentPieceOfType<King>();
        if (kingAttackingPieces.Length > 0) 
        {
            Player oppositePlayer = ChangeTurn(playerActive);
            Piece attackedKing = oppositePlayer.GetPiecesOfType<King>().FirstOrDefault();
            oppositePlayer.RemovesMovesEnablingAttackOnPieces<King>(playerActive, attackedKing);

            int avaiableKingMoves = attackedKing.applicableChessMoves.Count;
            if(avaiableKingMoves==0)
            {
                bool canCoverKing = oppositePlayer.CanHidePieceFromAttack<King>(playerActive);
                if (!canCoverKing)
                    return true;
            }
        }
        return false;
    }

    private void EndGame()
    {
        Debug.Log("Game Finished");
        SetGameState(GameState.Finished);
=======
        NextPlayerTurn();
>>>>>>> parent of e47a884 (gamestate)
    }

    /*
     * Switch between the players' turns
     */
    private void NextPlayerTurn()
    {
        if (playerActive == playerBlack)
        {
            playerActive = playerWhite;
        }
        else if (playerActive == playerWhite)
        {
            playerActive = playerBlack;
        }
    }
    /* 
     * Change the active player once they had their turn
     */
    private Player ChangeTurn(Player playerActive)
    {
        if (playerActive == playerWhite)
        {
            playerActive = playerBlack;
        }
        else if (playerActive == playerBlack)
        {
            playerActive = playerWhite;
        }
        return playerActive;
    }

    /*
     * Create all the possible moves for the given player
     */
    private void CreatePossibleMoves(Player p)
    {
        p.CreatePossibleMoves();
    }
}