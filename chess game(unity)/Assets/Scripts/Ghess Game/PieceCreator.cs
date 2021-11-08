using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PieceCreator : MonoBehaviour
{
    [SerializeField] private GameObject[] piecesPrefabs;
    [SerializeField] private Material blcMat;
    [SerializeField] private Material whiteMat;

    private Dictionary<string, GameObject> nameToPieceDict = new Dictionary<string, GameObject>();

    private void Awake()
    {
        foreach (var piece in piecesPrefabs)
        {
            nameToPieceDict.Add(piece.GetComponent<Piece>().GetType().ToString(), piece);
        }
    }

    public GameObject CreatePiece(Type type)
    {
        GameObject prefab = nameToPieceDict[type.ToString()];
        if(prefab)
        {
            GameObject Piece = Instantiate(prefab);
        }
        return null;
    }

    public Material getTeamMaterial(TeamColour team)
    {
        return team == TeamColour.White ? whiteMat : blcMat;
    }
}
