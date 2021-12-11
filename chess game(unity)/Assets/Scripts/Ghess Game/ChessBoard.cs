using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CellSelector))]
public abstract class ChessBoard : MonoBehaviour
{

    [SerializeField] private float cellSize;
    [SerializeField] private Transform bottomLeftCell;

    public const int CHESS_BRD_SIZE = 8;

    private Piece[,] grid;
    private Piece pieceSelected;
    private GameController controller;
    private CellSelector cellSelector;

    public abstract void SelectPieceMoved(Vector2 coords);
    public abstract void SetSelectedPiece(Vector2 coords);


    private void Awake()
    {
        SetupGrid();
        cellSelector = GetComponent<CellSelector>();
    }

    public void SetDependencies(GameController controller)
    {
        this.controller = controller;
    }


    /*
     * Create the grid
     */
    private void SetupGrid()
    {
        grid = new Piece[CHESS_BRD_SIZE, CHESS_BRD_SIZE];
    }

    public Vector3 CalcPosFromCoords(Vector2Int coords)
    {
        return bottomLeftCell.position + new Vector3(coords.x * cellSize, 0f, coords.y * cellSize);
    }

    private Vector2Int CalcCoordsFromPos(Vector3 input)
    {
        int x = Mathf.FloorToInt(transform.InverseTransformPoint(input).x / cellSize) + CHESS_BRD_SIZE / 2;
        int y = Mathf.FloorToInt(transform.InverseTransformPoint(input).z / cellSize) + CHESS_BRD_SIZE / 2;
        return new Vector2Int(x, y);
    }

    /*
     * Allows the user to select/deselect a cell to move the piece
     */
    public void CellSelected(Vector3 atPostion)
    {
        if (!controller.IsGameInProgress())
            return;
        Vector2Int coords = CalcCoordsFromPos(atPostion);
        Piece p = GetPieceOnCell(coords);

        if (pieceSelected)
        {
            if (p != null && pieceSelected == p)
                DeselectPiece();
            else if (p != null && pieceSelected != p && controller.IsFromActivePlayer(p.team))
                SelectPiece(coords);
            else if (pieceSelected.CanMoveTo(coords))
                SelectPieceMoved(coords);
        }
        else
        {
            if (p != null && controller.IsFromActivePlayer(p.team))
                SelectPiece(coords);
        }
    }

    public void PromotePiece(Piece piece)
    {
        TakePiece(piece);
        controller.InitializePieces(piece.unavaliableSquare, piece.team, typeof(Queen));
    }

    /*
     * Select the given piece
     */
    private void SelectPiece(Vector2Int coords)
    {
        Piece piece = GetPieceOnCell(coords);
        controller.RemoveMovesEnablingAttackOnPieceOfType<King>(piece);
        SetSelectedPiece(coords);
        List<Vector2Int> selection = pieceSelected.applicableChessMoves;
        showSelectableCells(selection);
    }

    private void showSelectableCells(List<Vector2Int> selection)
    {
        Dictionary<Vector3, bool> cellInfo = new();
        for(int i = 0; i < selection.Count; i++)
        {
            Vector3 pos = CalcPosFromCoords(selection[i]);
            bool isCellFree = GetPieceOnCell(selection[i]) == null;
            cellInfo.Add(pos, isCellFree);
        }
        cellSelector.DisplaySelectedCell(cellInfo);
    }

    /*
     * Deselect the piece by setting it to null
     */
    private void DeselectPiece()
    {
        pieceSelected = null;
        cellSelector.ResetSelectedCell();
    }

    /*
     * Move the selected piece to the given coordinates
     */
    public void MoveSelected(Vector2Int coordinates)
    {
        TryToTakeOppositePiece(coordinates);
        MovePiecesOnBoard(coordinates, pieceSelected.unavaliableSquare, pieceSelected, null);
        pieceSelected.MoveChessPiece(coordinates);
        DeselectPiece();
        CompleteTurn();
    }

    public void OnSetSelectedPiece(Vector2Int coords)
    {
        Piece piece = GetPieceOnCell(coords);
        pieceSelected = piece;
    }


    private void TryToTakeOppositePiece(Vector2Int coords)
    {
        Piece piece = GetPieceOnCell(coords);
        if (piece != null && !pieceSelected.IsFromSameTeam(piece))
            TakePiece(piece);
    }

    private void TakePiece(Piece piece)
    {
        if(piece)
        {
            grid[piece.unavaliableSquare.x, piece.unavaliableSquare.y] = null;
            controller.OnPieceRemoved(piece);
        }
    }

    /*
     * Finish the turn
     */
    private void CompleteTurn()
    {
        controller.CompleteTurn();
    }

    /*
     * Update the board by moving the piece
     */
    public void MovePiecesOnBoard(Vector2Int newCoordinates, Vector2Int oldCoordinates, Piece newPiece, Piece oldPiece)
    {
        grid[oldCoordinates.x, oldCoordinates.y] = oldPiece;
        grid[newCoordinates.x, newCoordinates.y] = newPiece;
    }

    public Piece GetPieceOnCell(Vector2Int coordinates)
    {
        if (WithinBounds(coordinates))
        {
            return grid[coordinates.x, coordinates.y];
        } else
        {
            return null;
        }
    }

    internal void OnGameRestarted()
    {
        pieceSelected = null;
        SetupGrid();
        
    }

    /*
     * Check if the given coordinates are within the bounds
     */
    public bool WithinBounds(Vector2Int coordinates)
    {
        if (coordinates.x < 0 || coordinates.y < 0 || coordinates.x >= CHESS_BRD_SIZE || coordinates.y >= CHESS_BRD_SIZE)
            return false;
        return true;
    }

    /*
     * Check if the grid already contains the given piece
     */
    public bool AlreadyContains(Piece p)
    {
        for (int i = 0; i < CHESS_BRD_SIZE; i++)
        {
            for (int j = 0; j < CHESS_BRD_SIZE; j++)
            {
                if (grid[i, j] == p)
                    return true;
            }
        }
        return false;
    }

    public void PlacePiecesOnBoard(Vector2Int coordinates, Piece piece)
    {
        if (WithinBounds(coordinates))
        {
            grid[coordinates.x, coordinates.y] = piece;
        }
    }

}
//testing