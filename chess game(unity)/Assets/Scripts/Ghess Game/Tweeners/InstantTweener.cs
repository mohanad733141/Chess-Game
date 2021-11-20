using UnityEngine;

public class InstantTweener : MonoBehaviour, IObjectTweener
{
    void IObjectTweener.MoveTo(Transform transform, Vector3 positionToMoveTo)
    {
        transform.position = positionToMoveTo;
    }
}
