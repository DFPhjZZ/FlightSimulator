using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RayFire;

public class Missile : MonoBehaviour
{
    [SerializeField]
    float lifetime;
    [SerializeField]
    float speed;
    [SerializeField]
    float trackingAngle;
    [SerializeField]
    float damage;
    [SerializeField]
    float damageRadius;
    [SerializeField]
    float turningGForce;
    [SerializeField]
    LayerMask collisionMask;
    [SerializeField]
    new MeshRenderer renderer;
    [SerializeField]
    GameObject explosionGraphic;

    Aircraft owner;
    bool exploded;
    Vector3 lastPosition;
    float timer;
    RayfireBomb rayFireBomb;
    private float maxDistance;
    private float minDistance = 50.0f;
    private Transform hitTransform;

    public GameObject target { get; set; }
    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        maxDistance = speed * lifetime;
    }

    public void Launch(Aircraft owner)
    {
        this.owner = owner;
        // this.target = target;

        Rigidbody = GetComponent<Rigidbody>();
        rayFireBomb = GetComponent<RayfireBomb>();

        lastPosition = Rigidbody.position;
        timer = lifetime;

        // if (target!= null) target.NotifyMissileLaunched(this, true);
    }

    void Explode()
    {
        if (exploded) return;

        timer = lifetime;
        Rigidbody.isKinematic = true;
        renderer.enabled = false;
        exploded = true;
        explosionGraphic.SetActive(true);

        var hits = Physics.OverlapSphere(Rigidbody.position, damageRadius, collisionMask.value);

        //foreach (var hit in hits)
        //{
        //    Aircraft other = hit.gameObject.GetComponent<Aircraft>();

        //    if (other != null && other != owner)
        //    {
        //        other.ApplyDamage(damage);
        //    }
        //}

        rayFireBomb.Explode(0.0f);
        if (hitTransform != null && hitTransform.gameObject.tag == "hostile")
        {
            if (target != null)
            {
                var rfr = target.GetComponent<RayfireRigid>();
                RayfireRigid[] subRigids = target.GetComponentsInChildren<RayfireRigid>();
                if (rfr != null)
                {
                    rfr.Demolish();
                }
                target = null;
            }
            hitTransform = null;
        }

        // if (target != null) target.NotifyMissileLaunched(this, false);
    }

    void CheckCollision()
    {
        //missile can travel very fast, collision may not be detected by physics system
        //use raycasts to check for collisions

        var currentPosition = Rigidbody.position;
        var error = currentPosition - lastPosition;
        var ray = new Ray(lastPosition, error.normalized);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, error.magnitude, collisionMask.value))
        {
            // Aircraft other = hit.collider.gameObject.GetComponent<Aircraft>();
            //
            // if (other == null || other != owner) {
            //     Rigidbody.position = hit.point;
            //     Explode();
            // }
            Rigidbody.position = hit.point;
            hitTransform = hit.transform;
            Explode();
            owner.target = null;
        }

        lastPosition = currentPosition;
    }

    void TrackTarget(float dt)
    {
        if (target == null) return;

        var targetPosition = Utilities.FirstOrderIntercept(Rigidbody.position, Vector3.zero, speed, target.transform.position, target.GetComponent<Rigidbody>().velocity);

        var error = targetPosition - Rigidbody.position;
        var targetDir = error.normalized;
        var currentDir = Rigidbody.rotation * Vector3.forward;

        //if angle to target is too large, explode
        // if (Vector3.Angle(currentDir, targetDir) > trackingAngle) {
        //     Explode();
        //     return;
        // }

        //calculate turning rate from G Force and speed
        float maxTurnRate = (turningGForce * 9.81f) / speed;  //radians / s
        var dir = Vector3.RotateTowards(currentDir, targetDir, maxTurnRate * dt, 0);

        Rigidbody.rotation = Quaternion.LookRotation(dir);
    }

    void TrackTarget()
    {
        if (target == null) return;

        var leadTimePercentage = Mathf.InverseLerp(minDistance, maxDistance,
            Vector3.Distance(transform.position, target.gameObject.transform.position));
        var predictionTime = Mathf.Lerp(0, lifetime, leadTimePercentage);
        // Vector3 prediction = target.GetComponent<Rigidbody>().position + target.GetComponent<Rigidbody>().velocity *
        //                      predictionTime;
        Vector3 prediction = target.transform.position;
        var heading = prediction - transform.position;
        var rotation = Quaternion.LookRotation(heading);
        Rigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, 90f * Time.fixedDeltaTime));
    }

    void FixedUpdate()
    {
        timer = Mathf.Max(0, timer - Time.fixedDeltaTime);

        //explode missile automatically after lifetime ends
        //timer is reused to keep missile graphics alive after explosion
        if (timer == 0)
        {
            if (exploded)
            {
                Destroy(gameObject);
            }
            else
            {
                Explode();
            }
        }

        if (exploded) return;

        CheckCollision();
        // TrackTarget(Time.fixedDeltaTime);
        TrackTarget();

        //set speed to direction of travel
        Rigidbody.velocity = Rigidbody.rotation * new Vector3(0, 0, speed);
    }
}
