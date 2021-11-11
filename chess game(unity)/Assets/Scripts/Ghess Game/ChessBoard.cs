using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoard : MonoBehaviour
{
    [SerializeField] private float cellSize;
    [SerializeField] private Transform downLeftCellTrans;

    internal Vector3 CalculateLoctionAtCoordinates(Vector2Int coordinates) {
        return downLeftCellTrans.position + new Vector3(coordinates.x * cellSize, 0f, coordinates.y * cellSize);
    }
}
