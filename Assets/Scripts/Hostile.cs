using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hostile : MonoBehaviour
{
    [SerializeField]
    Aircraft aircraft;
    [SerializeField]
    float maxDistance = 2000f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.active)
        {
            var vec = this.transform.position - aircraft.transform.position;
            var angle = Vector3.Angle(vec, aircraft.transform.forward);
            if (Vector3.Distance(transform.position, aircraft.transform.position) < maxDistance && (angle < 90.0f && angle > -90.0f))
            {
                if (!aircraft.hostileObjects.Contains(this))
                    aircraft.hostileObjects.Add(this);
            }
            else
            {
                if (aircraft.hostileObjects.Contains(this))
                {
                    aircraft.hostileObjects.Remove(this);
                    aircraft.target = null;
                }
            }
        }
    }

    private void OnDestroy()
    {
        aircraft.hostileObjects.Remove(this);
        aircraft.target = null;
    }
}
