using UnityEngine;

public abstract class InputReceiver : MonoBehaviour
{
    protected IInputHandler[] inputHandlers;

    public abstract void onInput();

    private void Awake() 
    {
        inputHandlers = GetComponents<IInputHandler>();
    }
}
