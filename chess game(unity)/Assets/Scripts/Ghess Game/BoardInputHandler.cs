using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ChessBoard))]
public class BoardInputHandler : MonoBehaviour, IInputHandler
{
    private ChessBoard chessBoard;

    private void Awake()
    {
        chessBoard = GetComponent<ChessBoard>();
    }

    void IInputHandler.processInput(Vector3 inputPos, GameObject objsSelected, Action callback)
    {
        chessBoard.onCellSelected(inputPos);
    }
}
