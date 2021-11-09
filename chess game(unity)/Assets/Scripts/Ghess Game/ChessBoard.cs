using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoard : MonoBehaviour
{

    [SerializeField] private Transform bottomLeftCellTransform;
    [SerializeField] private float cellSize;


    internal Vector3 CalculateLoctionAtCoordinates(Vector2Int coordinates) {
        return bottomLeftCellTransform.position + new Vector3(coordinates.x * cellSize, 0f, coordinates.y * cellSize);
    }
}
