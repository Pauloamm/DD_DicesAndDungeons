using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    private float defaultY;
    private float direction;

    float maxYOffset = 0.1f;
    float yStep = 0.1f;
    float rotationStep = 100f;

    protected virtual void Awake()
    {
        defaultY = transform.localPosition.y;
        direction = 1;
    }

    protected virtual void Update()
    {
        Float();
    }

    void Float()
    {
        // Rotates
        transform.Rotate(transform.up, rotationStep * Time.deltaTime, Space.World);

        // Goes up and down
        if (transform.localPosition.y > defaultY + maxYOffset)
        {
            direction = -1;
        }
        else if (transform.localPosition.y < defaultY)
        {
            direction = 1;
        }

        transform.localPosition = new Vector3(
            transform.localPosition.x,
            transform.localPosition.y + yStep * direction * Time.deltaTime, 
            transform.localPosition.z);
    }
}