using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmovableObject : MonoBehaviour
{
    private Vector3 point;
    private void Awake()
    {
        point = transform.position;
    }

    private void Update()
    {
        if (point != transform.position)
        {
            transform.position = point;
        }
    }
}
