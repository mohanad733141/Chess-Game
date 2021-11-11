using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PieceCreator : MonoBehaviour
{
    [SerializeField] private GameObject[] chessPieceObj;
    [SerializeField] private Material blcMat;
    [SerializeField] private Material whiteMat;

    private Dictionary<string, GameObject> mapPieceToName = new Dictionary<string, GameObject>();

    private void Awake()
    {
        foreach (var chessObj in chessPieceObj)
        {
            mapPieceToName.Add(chessObj.GetComponent<Piece>().GetType().ToString(), chessObj);
        }
    }
    
    public GameObject MakeNewPieces(Type ChessPieceType)
    {
        GameObject tempPiece = mapPieceToName[ChessPieceType.ToString()];
        if(tempPiece)
        {
            GameObject newChessPiece = Instantiate(tempPiece);
            return tempPiece;
            //here we create the new piece 
        }
        return null;
    }

    public Material getEachPlayerMaterial(TeamColour team)
    {
        return team == TeamColour.White ? whiteMat : blcMat;
    }
}
