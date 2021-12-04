using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject UIParent;
    [SerializeField] private Text resultText;

    public void HideMenu()
    {
        UIParent.SetActive(false);
    }

    public void IsGameFinished(string winner)
    {
        UIParent.SetActive(true);
        resultText.text = string.Format("{0} has won", winner); 
    }
}
