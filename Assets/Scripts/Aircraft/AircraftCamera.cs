using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using UnityEngine;
using UnityEngine.InputSystem;

public class AircraftCamera : MonoBehaviour
{
    [SerializeField]
    Vector3 cameraOffset;
    [SerializeField]
    Vector2 lookAngle;
    [SerializeField]
    float movementScale;
    [SerializeField]
    float lookAlpha;
    [SerializeField]
    float movementAlpha;
    [SerializeField]
    Vector3 deathOffset;
    [SerializeField]
    float deathSensitivity;
    [SerializeField]
    new Camera camera;
    [SerializeField]
    private float mouseSensitivity = 50.0f;
    [SerializeField]
    float mouseStopThreshold = 1.0f;
    [SerializeField]
    LayerMask rayCastMask;

    private CraftInput craftInput;
    private float distance;
    private float xAngle = 0.0f;
    private float yAngle = 0.0f;
    private float resetTimer = 0.0f;
    private Quaternion prevRot;

    Transform cameraTransform;
    Aircraft aircraft;
    Transform aircraftTransform;
    Vector2 cameraInput;
    Vector2 look;
    Vector2 lookAverage;
    Vector3 avAverage;
    bool dead;

    void Awake()
    {
        cameraTransform = camera.GetComponent<Transform>();

        // craftInput = new CraftInput();
        // craftInput.Aircraft.Camera.performed += ctx=> cameraInput = ctx.ReadValue<Vector2>();
        // craftInput.Aircraft.Camera.canceled += ctx => cameraInput = Vector2.zero;
    }

    void Start()
    {
        var angles = cameraTransform.eulerAngles;
        xAngle = angles.x;
        yAngle = angles.y;

        distance = cameraOffset.magnitude;
    }

    public void SetAircraft(Aircraft aircraft)
    {
        this.aircraft = aircraft;

        if (aircraft == null)
        {
            aircraftTransform = null;
        }
        else
        {
            aircraftTransform = aircraft.GetComponent<Transform>();
        }
        cameraTransform.SetParent(aircraftTransform);
        cameraTransform.localPosition = cameraOffset;
    }

    public void SetInput(Vector2 input)
    {
        cameraInput = input;
    }

    // Update is called once per frame
    void Update()
    {
        // if (cameraInput != Vector2.zero)
        // {
        //     resetTimer = 0.0f;
        // }
        // else
        // {
        //     resetTimer += Time.deltaTime;
        //     if (resetTimer > mouseStopThreshold)
        //     {
        //         xAngle = Mathf.Lerp(xAngle, defaultXAngle, Time.deltaTime);
        //         yAngle = Mathf.Lerp(yAngle, defaultYAngle, Time.deltaTime);
        //         
        //         cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, cameraTransform.rotation * aircraftTransform.rotation, Time.deltaTime);
        //         cameraTransform.position = Vector3.Lerp(cameraTransform.position,
        //             aircraft.transform.position + cameraOffset, Time.deltaTime);
        //     }
        // }
    }

    private void FixedUpdate()
    {
        if (aircraft == null) return;

        // Debug.Log(cameraInput);

        // Attempt 05 Closest one thus far 
        // TODO: Reset camera after releasing mouse
        //if (resetTimer <= mouseStopThreshold)
        // {
        xAngle += cameraInput.x * mouseSensitivity * Time.deltaTime;
        yAngle -= cameraInput.y * mouseSensitivity * Time.deltaTime;

        xAngle = Mathf.Clamp(xAngle, -170f, 170f);
        yAngle = Mathf.Clamp(yAngle, -80f, 80f);

        Quaternion rotation = Quaternion.Euler(yAngle, xAngle, 0f);
        // Vector3 pos = rotation * new Vector3(0f, 0f, -distance) + aircraft.transform.position;
        Vector3 pos = rotation * cameraOffset;

        cameraTransform.localRotation = rotation;
        cameraTransform.localPosition = pos;
        // }

        var tempPos = cameraTransform.position;
        var aircraft2Cam = (cameraTransform.position - aircraft.transform.position).normalized;
        Ray ray = new Ray(aircraft.transform.position, aircraft2Cam);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray, cameraOffset.magnitude, rayCastMask.value);
        RaycastHit adjustHit = new RaycastHit();
        bool flag = false;
        foreach (var hit in hits)
        {
            if (!hit.collider.CompareTag("MainCamera") && !hit.collider.CompareTag("Aircraft") && !hit.collider.CompareTag("WindArea"))
            {
                adjustHit = hit;
                flag = true;
                break;
            }
        }
        if (flag)
        {
            // Debug.Log(hits.Length + " " + adjustHit.transform.gameObject.name + " " + adjustHit.collider.tag + " aaa");
            // Debug.Log(adjustHit.transform.gameObject.name);
            cameraTransform.position = adjustHit.point;
            var dist = (adjustHit.point - aircraft.transform.position).normalized;
            if (Vector3.Distance(adjustHit.point, aircraft.transform.position) > distance)
            {
                cameraTransform.position = aircraft.transform.position + distance * dist;
            }
        }
        else
        {
            // Debug.Log(hits.Length + " bbb");
            cameraTransform.position = aircraft.transform.position + (tempPos - aircraft.transform.position).normalized * distance;
        }


        // Attempt 07
        // var currentLookAngle = cameraInput;
        // if (currentLookAngle.magnitude > 1f) currentLookAngle = currentLookAngle.normalized;
        // currentLookAngle = Vector2.Scale(currentLookAngle, lookAngle);
        //
        // // var rotation = Quaternion.Euler(-currentLookAngle.y, currentLookAngle.x, 0f);
        // // var offset = rotation * cameraOffset;
        // // cameraTransform.localPosition = offset;
        // // cameraTransform.localRotation = rotation;
        //
        // lookAverage = (lookAverage * (1f - lookAlpha)) + (currentLookAngle * lookAlpha);
        // var rotation = Quaternion.Euler(-lookAverage.y, lookAverage.x, 0f);
        //
        // if (cameraInput == Vector2.zero)
        // {
        //     cameraTransform.localPosition = rotation * cameraOffset;
        //     cameraTransform.localRotation = rotation;
        //     prevRot = rotation;
        // }
        // else if (prevRot != null && Quaternion.Angle(prevRot, rotation) >= 5f)
        // {
        //     cameraTransform.localPosition = rotation * cameraOffset;
        //     cameraTransform.localRotation = rotation;
        //         prevRot = rotation;
        // }

        // original
        // var cameraOffset = this.cameraOffset;
        // // Debug.Log("input: " + cameraInput + " magnitude: " + cameraInput.magnitude);
        // // cameraInput.x = Mathf.Clamp(cameraInput.x, -1f, 1f);
        // // cameraInput.y = Mathf.Clamp(cameraInput.y, -1f, 1f);
        // // cameraInput = cameraInput.normalized;
        // // Debug.Log("after input: " + cameraInput + " magnitude: " + cameraInput.magnitude);
        // if (aircraft.Dead)
        // {
        //     look += cameraInput * deathSensitivity * Time.deltaTime;
        //     look.x = (look.x + 360.0f) % 360.0f;
        //     look.y = Mathf.Clamp(look.y, -lookAngle.y, lookAngle.y);
        //
        //     lookAverage = look;
        //     avAverage = new Vector3();
        //
        //     cameraOffset = deathOffset;
        // }
        // else
        // {
        //     var targetLookAngle = Vector2.Scale(cameraInput, lookAngle);
        //     lookAverage = (lookAngle * (1 - lookAlpha)) + (targetLookAngle * lookAlpha);
        //
        //     var angularVelocity = aircraft.LocalAngularVelocity;
        //     angularVelocity.z = -angularVelocity.z;
        //
        //     avAverage = (avAverage * (1.0f - movementAlpha)) + (angularVelocity * movementAlpha);
        // }
        //
        // var rotation = Quaternion.Euler(-lookAverage.y, lookAverage.x, 0);
        // var turningRotation = Quaternion.Euler(new Vector3(-avAverage.x, -avAverage.y, avAverage.z) * movementScale);
        //
        // cameraTransform.localPosition = rotation * turningRotation * cameraOffset;
        // cameraTransform.localRotation = rotation * turningRotation;
    }
}
