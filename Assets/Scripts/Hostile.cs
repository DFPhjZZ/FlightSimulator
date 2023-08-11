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
            if (Vector3.Distance(transform.position, aircraft.transform.position) < maxDistance)
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
