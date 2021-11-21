using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderInputReceiver : InputReceiver
{
    private Vector3 clickPosition;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))// left mouse button has been clicked
        {
            //Debug.Log("Player selected");

            // Reference: https://answers.unity.com/questions/532509/i-dont-understand-rayraycastraycasthit-.html#:~:text=The%20RaycastHit%20is%20the%20structure,in%20the%20raycast%20hit%20struct.
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                clickPosition = hit.point;
                OnInputReceived();
            }
        }
    }

    public override void OnInputReceived()
    {
        //for (int i = 0; i < inputHandlers.Length; i++)
        //{
        //    inputHandlers[i].processInput(selectedPos, null, null);
        //}
        foreach (var handler in inputHandlers)
        {
            handler.ProcessInput(clickPosition, null, null);
        }
    }
}
