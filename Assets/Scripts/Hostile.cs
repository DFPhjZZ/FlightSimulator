using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RayFire;

public class Hostile : MonoBehaviour
{
    Aircraft aircraft;
    [SerializeField]
    float maxDistance = 2000f;
    [SerializeField]
    GameObject intactObject;
    [SerializeField]
    GameObject shatteredObject;
    [SerializeField] 
    GameObject audioObject;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    public int point;

    public float health { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        if (aircraft == null)
        {
            aircraft = GameObject.FindWithTag("Player").gameObject.GetComponent<Aircraft>();
        }

        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf)
        {
            if (health <= 0f)
            {
                Destroy(intactObject);
                shatteredObject.SetActive(true);
                this.transform.DetachChildren();
                
                // RayFire Demolition
                var rfr = shatteredObject.GetComponent<RayfireRigid>();
                if (rfr != null)
                {
                    rfr.Demolish();
                    Destroy(this.gameObject);
                    aircraft.target = null;
                }
                
                // Play Demolition Audio
                var dc = audioObject.GetComponent<AudioControl>();
                if (dc != null)
                {
                    dc.PlaySound();
                }
                
                // Mission Info Update (i.e. score)
                if (aircraft.objectiveID == 1)
                {
                    aircraft.score += point;
                }
            }
            else
            {
                var vec = this.transform.position - aircraft.transform.position;
                var angle = Vector3.Angle(vec, aircraft.transform.forward);
                if (Vector3.Distance(transform.position, aircraft.transform.position) < maxDistance &&
                    (angle < 90.0f && angle > -90.0f))
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
    }

    private void OnDestroy()
    {
        aircraft.hostileObjects.Remove(this);
        aircraft.target = null;
    }
}
