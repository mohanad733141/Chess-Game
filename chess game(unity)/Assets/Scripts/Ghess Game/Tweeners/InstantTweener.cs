using UnityEngine;

public class InstantTweener : MonoBehaviour, IObjectTweener
{
    public void MoveTo(Transform trans, Vector3 positionToMoveTo)
    {
        trans.position = positionToMoveTo;
    }
}
