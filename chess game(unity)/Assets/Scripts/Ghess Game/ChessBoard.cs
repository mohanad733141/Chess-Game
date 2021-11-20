using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoard : MonoBehaviour
{
    [SerializeField] private float cellSize;
    [SerializeField] private Transform downLeftCellTrans;
    public const int CHESS_BOARD_SIZE = 8;

    private Piece[,] grid;// 2d array to keep track of which piece is in which square
    private GameController gameController;
    private Piece pieceSelected;

    private void Awake()
    {
        initialiseGrid();
    }

    public void SetDependencies(GameController gameController)
    {
        this.gameController = gameController;
    }

    private void initialiseGrid()
    {
        grid = new Piece[CHESS_BOARD_SIZE, CHESS_BOARD_SIZE];
    }

    public Vector3 CalculateLoctionAtCoordinates(Vector2Int coordinates)
    {
        return downLeftCellTrans.position + new Vector3(coordinates.x * cellSize, 0f, coordinates.y * cellSize);
    }

    // Convert the passed position to a square coordinate
    private Vector2Int CalculateLoctionAtCoordinates(Vector3 inputPos)
    {
        int x = Mathf.FloorToInt(transform.InverseTransformPoint(inputPos).x / cellSize) + CHESS_BOARD_SIZE / 2;
        int y = Mathf.FloorToInt(transform.InverseTransformPoint(inputPos).y / cellSize) + CHESS_BOARD_SIZE / 2;
        return new Vector2Int(x, y);
    }

    public void onCellSelected(Vector3 inputPos)
    {
        Vector2Int coords = CalculateLoctionAtCoordinates(inputPos);
        Piece p = getPieceAt(coords); // check which piece is located at the calculated coordinates
        if (pieceSelected)// a piece is selected
        {
            if (p != null && pieceSelected == p)
            {
                deselectPiece();
            }
            else if (p != null && gameController.isFromActivePlayer(p.teamColour) && pieceSelected != p)
            {
                choosePiece(p);
            }
            else if (pieceSelected.AbleToMoveTo(coords))
            {
                moveSelectedPiece(coords, pieceSelected);
            }
            else // no piece is selected
            {
                if (p != null && gameController.isFromActivePlayer(p.teamColour))
                {
                    choosePiece(p);
                }
            }
        }
    }

    private void moveSelectedPiece(Vector2Int coords, Piece p)
    {
        // update the board by passing the new coordinates
        addPieceMoveToBoard(coords, p.unavailableCell, p, null);
        pieceSelected.MoveChessPiece(coords);// move the piece to the given coordinates
        deselectPiece();
        turnCompleted();
    }

    private void turnCompleted()
    {
        gameController.completeTurn();
    }

    private void addPieceMoveToBoard(Vector2Int newCoordinates, Vector2Int oldCoordinates, Piece newPiece, Piece oldPiece)
    {
        grid[oldCoordinates.x, oldCoordinates.y] = oldPiece;
        grid[newCoordinates.x, newCoordinates.y] = newPiece;
    }

    private void choosePiece(Piece p)
    {
        pieceSelected = p;
    }

    private void deselectPiece()
    {
        pieceSelected = null;
    }

    private Piece getPieceAt(Vector2Int coords)
    {
        if (withinBounds(coords))// check if coordinates passed are not outside the bounds
            return grid[coords.x, coords.y];
        return null;// if coordinates are not within the bounds, return null
    }

    // Check if the chess board already contains the piece
    public bool alreadyContains(Piece p)
    {
        for (int i = 0; i < CHESS_BOARD_SIZE; i++) //iterate through the rows
        {
            for (int j = 0; j < CHESS_BOARD_SIZE; j++) //iterate through the columns
            {
                if (grid[i, j] == p)// grid contains the given piece
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void setPiecesOnBoard(Vector2Int coords, Piece newChessPiece)
    {
        // check if the coordinates passed through the parameter are within the bounds
        if (withinBounds(coords))
        {
            grid[coords.x, coords.y] = newChessPiece;// set the piece to the given coordinates
        }
    }

    // Check if coordinates passed are within the bounds
    private bool withinBounds(Vector2Int coords)
    {
        if (coords.x < 0 || coords.y < 0 || coords.x >= CHESS_BOARD_SIZE || coords.y >= CHESS_BOARD_SIZE)
            return false;
        return true;
    }
}
