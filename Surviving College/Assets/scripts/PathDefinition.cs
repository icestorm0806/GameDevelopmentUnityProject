﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathDefinition : MonoBehaviour
{

    public Transform[] Points;

    public IEnumerator<Transform> GetPathEnumerator()
    {
        if (Points == null || Points.Length < 1)
            yield break;

        int direction = 1;
        int index = 0;
        while (true)
        {
            yield return Points[index];

            if (Points.Length == 1.0f)
                continue;

            if (index <= 0)
                direction = 1;
            else if (index >= Points.Length - 1.0f)
                direction = -1;

            index = index + direction;
        }
    }

    public void OnDrawGizmos()
    {
            if (Points == null || Points.Length < 2)
                return;

        var points = Points.Where(tag => tag != null).ToList();
        if (points.Count < 2)
            return;

        for (int i = 1; i < points.Count; i++)
            {
                NewMethod(i);
            }
    }

    private void NewMethod(int i)
    {
        Gizmos.DrawLine(Points[i - 1].position, Points[i].position);
    }
}
