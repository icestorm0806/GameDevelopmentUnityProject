using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformVertical : MonoBehaviour
{

    private Vector2 posA;
    private Vector2 posB;
    private Vector2 nexPos;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform childTransform;

    [SerializeField]
    private Transform transformB;

    void Start()
    {
        posA = childTransform.localPosition;
        posB = transformB.localPosition;
        nexPos = posB;
    }
    void Update()
    {
        Move();
    }

    private void Move()
    {
        childTransform.localPosition = Vector2.MoveTowards(childTransform.localPosition, nexPos, speed * Time.deltaTime);
        if (Vector2.Distance(childTransform.localPosition, nexPos) <= 0.1)
        {
            ChangeDestination();
        }
    }

    private void ChangeDestination()
    {
        nexPos = nexPos != posA ? posA : posB;
    }
}
