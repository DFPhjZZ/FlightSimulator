using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WindArea : MonoBehaviour
{
    [SerializeField]
    public float Strength;
    [SerializeField]
    public Vector3 Direction;

    private float changeTimer = 30f;
    private float baseStrength;


    private void Awake()
    {
        baseStrength = Strength;
    }

    private void Update()
    {
        changeTimer -= Time.deltaTime;
        if (changeTimer <= 0f)
        {
            Direction = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0f,
                UnityEngine.Random.Range(-1f, 1f)).normalized;
            Strength = baseStrength * UnityEngine.Random.Range(1.0f, 2.5f);
            changeTimer = 30f;
        }
    }
}
