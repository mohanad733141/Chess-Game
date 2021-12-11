using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardInputHandler : MonoBehaviour, IInputHandler
{
    private ChessBoard board;

    private void Awake()
    {
        board = GetComponent<ChessBoard>();
    }

    public void ProcessInput(Vector3 inputPosition, GameObject selectedObject, Action callback)
    {
        board.CellSelected(inputPosition);
    }
}
