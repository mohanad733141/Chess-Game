using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LineTweener : MonoBehaviour, IObjectTweener
{
    [SerializeField] private float speed;

    public void MoveTo(Transform transform, Vector3 positionToMoveTo)
    {
        float distance = Vector3.Distance(positionToMoveTo, transform.position);
        transform.DOMove(positionToMoveTo, distance / speed);
    }
}
