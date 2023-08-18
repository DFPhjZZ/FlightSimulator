using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WindArea : MonoBehaviour
{
    public float Strength;
    public Vector3 Direction;
    private float changeTimer = 30f;

    private void Update()
    {
        changeTimer -= Time.deltaTime;
        if (changeTimer <= 0f)
        {
            Direction = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f),
                UnityEngine.Random.Range(-1f, 1f)).normalized;
            changeTimer = 30f;
        }
    }
}
