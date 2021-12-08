using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public GameObject sound;

    public void Play()
    {
        Instantiate(sound, transform.position, transform.rotation);
    }
}
