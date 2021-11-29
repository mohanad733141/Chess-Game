using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSelector : MonoBehaviour
{
    [SerializeField] private Material freeCellMaterial;// cell where the piece can be moved
    [SerializeField] private Material opponentCellMaterial;// opponent's cell
    private List<GameObject> selectors = new List<GameObject>();
    [SerializeField] private GameObject pieceSelector;


    public void displaySelectedCell(Dictionary<Vector3, bool> cellInfo)
    {
        resetSelectedCell();
        foreach(var info in cellInfo)
        {
            GameObject selector = Instantiate(pieceSelector, info.Key, Quaternion.identity);
            selectors.Add(selector);
            foreach(var s in selector.GetComponentsInChildren<MaterialSetter>())
            {
                s.SetSingleMaterial(info.Value ? freeCellMaterial : opponentCellMaterial);
            }
        }
    }

    /*
     * Reset any previously selected cells
     */
    public void resetSelectedCell()
    {
        for(int i = 0; i < selectors.Count; i++)
        {
            Destroy(selectors[i]);
        }
        selectors.Clear();
    }

}
