using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoard : MonoBehaviour
{

    [SerializeField] private float cellSize;
    [SerializeField] private Transform bottomLeftCell;

    public const int CHESS_BRD_SIZE = 8;

    private Piece[,] grid;
    private Piece pieceSelected;
    private GameController controller;


    private void Awake()
    {
        SetupGrid();
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
    public void CellSelected(Vector3 input)
    {
        Vector2Int coords = CalcCoordsFromPos(input);
        Piece piece = GetPieceOnSquare(coords);

        if (pieceSelected)
        {
            if (piece != null && pieceSelected == piece)
                DeselectPiece();
            else if (piece != null && pieceSelected != piece && controller.IsFromActivePlayer(piece.team))
                SelectPiece(piece);
            else if (pieceSelected.CanMoveTo(coords))
                OnSelectedPieceMoved(coords, pieceSelected);
        }
        else
        {
            if (piece != null && controller.IsFromActivePlayer(piece.team))
                SelectPiece(piece);
        }
    }



    private void SelectPiece(Piece piece)
    {
        pieceSelected = piece;
        List<Vector2Int> selection = pieceSelected.availableMoves;
    }


    private void DeselectPiece()
    {
        pieceSelected = null;
    }
    private void OnSelectedPieceMoved(Vector2Int coords, Piece piece)
    {
        UpdateBoardOnPieceMove(coords, piece.occupiedSquare, piece, null);
        pieceSelected.MovePiece(coords);
        DeselectPiece();
        CompleteTurn();
    }

    private void CompleteTurn()
    {
        controller.CompleteTurn();
    }

    private void UpdateBoardOnPieceMove(Vector2Int newCoords, Vector2Int oldCoords, Piece newPiece, Piece oldPiece)
    {
        grid[oldCoords.x, oldCoords.y] = oldPiece;
        grid[newCoords.x, newCoords.y] = newPiece;
    }

    public Piece GetPieceOnSquare(Vector2Int coords)
    {
        if (CheckIfCoordinatesAreOnBoard(coords))
            return grid[coords.x, coords.y];
        return null;
    }

    public bool CheckIfCoordinatesAreOnBoard(Vector2Int coords)
    {
        if (coords.x < 0 || coords.y < 0 || coords.x >= CHESS_BRD_SIZE || coords.y >= CHESS_BRD_SIZE)
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

    public void setPiecesOnBoard(Vector2Int coords, Piece piece)
    {
        if (CheckIfCoordinatesAreOnBoard(coords))
            grid[coords.x, coords.y] = piece;
    }

}