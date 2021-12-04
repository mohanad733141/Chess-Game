using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArcTweener : MonoBehaviour, IObjectTweener
{
    [SerializeField] private float speed;
    [SerializeField] private float height;

    public void MoveTo(Transform transform, Vector3 positionToMoveTo)
    {
        float distance = Vector3.Distance(positionToMoveTo, transform.position);
        transform.DOJump(positionToMoveTo, height, 1, distance / speed);
    }
}
