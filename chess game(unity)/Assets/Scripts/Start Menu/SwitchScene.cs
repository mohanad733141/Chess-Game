using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    /*
     * Load the Scene pointing to the given index
     */
    public void LoadScene(int i)
    {
        SceneManager.LoadScene(i);
    }
}
