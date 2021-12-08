using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAudioFromStartMenu : MonoBehaviour
{
    // Destroy the object after 2 seconds
    void Start()
    {
        Destroy(gameObject, 2f);
    }

}
