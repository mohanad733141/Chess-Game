using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PieceCreator : MonoBehaviour
{
    [SerializeField] private GameObject[] piecePrefabs;
    [SerializeField] private Material blackMaterial;
    [SerializeField] private Material whiteMaterial;

    private Dictionary<string, GameObject> nameToPieceDict = new Dictionary<string, GameObject>();

    private void Awake()
    {
        foreach (var piece in piecePrefabs)
        {
            nameToPieceDict.Add(piece.GetComponent<Piece>().GetType().ToString(), piece);
        }
    }

    public GameObject MakeNewPieces(Type type)
    {
        GameObject prefab = nameToPieceDict[type.ToString()];
        if (prefab)
        {
            GameObject newPiece = Instantiate(prefab);
            return newPiece;
        }
        return null;
    }

    public Material getEachPlayerMaterial(TeamColour team)
    {
        return team == TeamColour.White ? whiteMaterial : blackMaterial;
    }
}
