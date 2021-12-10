using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[RequireComponent(typeof(PieceCreator))]
public class GameController : MonoBehaviour
{
    private enum GameState { Init, Play, Finished };

    [SerializeField] private ChessBoardLayout brdLayout;
    [SerializeField] private Menu menu;

    private PieceCreator pieceMaker;
    // 2 players for the game
    private Player playerWhite;
    private Player playerBlack;
    // active player making the move
    private Player playerActive;
    [SerializeField] private ChessBoard brd;
    private GameState state;

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
        menu.HideUI();
        SetGameState(GameState.Init);
        brd.SetDependencies(this);
        MakePieces(brdLayout);
        playerActive = playerWhite;// white player is the first player to choose a move
        CreatePossibleMoves(playerActive);
        SetGameState(GameState.Play);
    }

    private void SetGameState(GameState state)
    {
        this.state = state;
    }

    public bool IsGameInProgress()
    {
        return state == GameState.Play;
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

    public void InitializePieces(Vector2Int squareCoordinates, TeamColour teamColour, Type type)
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
        if (CheckIfGameIsFinished())
            EndGame();
        NextPlayerTurn();
    }

    private bool CheckIfGameIsFinished()
    {
        Piece[] kingAttackingPieces = playerActive.GetPiecesAttackingOppositePieceOfType<King>();
        if(kingAttackingPieces.Length > 0)
        {
            Player oppositePlayer = ChangeTurn(playerActive);
            Piece attackedKing = oppositePlayer.GetPiecesOfType<King>().FirstOrDefault();
            oppositePlayer.RemoveMovesEnablingAttackOnPiece<King>(playerActive, attackedKing);

            int availableKingMoves = attackedKing.applicableChessMoves.Count;
            if(availableKingMoves == 0)
            {
                bool canCoverKing = oppositePlayer.CanHidePieceFromAttack<King>(playerActive);
                if (!canCoverKing)
                    return true;
            }
        }

        return false;
    }

    public void OnPieceRemoved(Piece piece)
    {
        Player pieceOwner = (piece.team == TeamColour.White) ? playerWhite : playerBlack;
        pieceOwner.RemovePiece(piece);
        Destroy(piece.gameObject);
    }

    private void EndGame()
    {
        menu.OnGameFinished(playerActive.playerColour.ToString());
        SetGameState(GameState.Finished);
    }

    public void RestartGame()
    {
        DestroyPieces();
        brd.OnGameRestarted();
        playerWhite.OnGameRestarted();
        playerBlack.OnGameRestarted();
        NewGame();
    }

    private void DestroyPieces()
    {
        playerWhite.activePlayerPieces.ForEach(p => Destroy(p.gameObject));
        playerBlack.activePlayerPieces.ForEach(p => Destroy(p.gameObject));
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

    public void RemoveMovesEnablingAttackOnPieceOfType<T>(Piece piece) where T : Piece
    {
        playerActive.RemoveMovesEnablingAttackOnPiece<T>(ChangeTurn(playerActive), piece);
    }
}