using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PieceCreator : MonoBehaviour
{
    [SerializeField] private GameObject[] piecePrefabs;
    [SerializeField] private Material blcMat;
    [SerializeField] private Material whiteMat;

    private Dictionary<string, GameObject> nameToPieceDict = new Dictionary<string, GameObject>();

    private void Awake()
    {
        foreach (var piece in piecePrefabs)
        {
            nameToPieceDict.Add(piece.GetComponent<Piece>().GetType().ToString(), piece);
        }
    }

    public GameObject MakeNewPieces(Type chessType)
    {
        GameObject chessObj = nameToPieceDict[chessType.ToString()];
        if (chessObj)
        {
            GameObject newChessPiece = Instantiate(chessObj);
            return newChessPiece;
        }
        return null;
    }

    public Material getEachPlayerMaterial(TeamColour team)
    {
        return team == TeamColour.White ? whiteMat : blcMat;
    }
}
