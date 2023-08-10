using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : MonoBehaviour
{
    [Header("Surfaces")]
    public List<ControlSurface> elevators;
    public ControlSurface aileronLeft;
    public ControlSurface aileronRight;
    public List<ControlSurface> rudders;
    public Engine engine;
    
    public Rigidbody Rigidbody { get; internal set; }

    private float throttle = 1.0f;
    private bool yawDefined = false;
    
    private void Awake()
	{
		Rigidbody = GetComponent<Rigidbody>();
	}

	private void Start()
	{
		if (elevators == null)
			Debug.LogWarning(name + ": Airplane missing elevator!");
		if (aileronLeft == null)
			Debug.LogWarning(name + ": Airplane missing left aileron!");
		if (aileronRight == null)
			Debug.LogWarning(name + ": Airplane missing right aileron!");
		if (rudders == null)
			Debug.LogWarning(name + ": Airplane missing rudder!");
		if (engine == null)
			Debug.LogWarning(name + ": Airplane missing engine!");

		try
		{
			Input.GetAxis("Y");
			yawDefined = true;
		}
		catch (ArgumentException e)
		{
			Debug.LogWarning(e);
			Debug.LogWarning(name + ": \"Yaw\" axis not defined in Input Manager. Rudder will not work correctly!");
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (elevators != null)
		{
			foreach (var elevator in elevators)
			{
				elevator.targetDeflection = -Input.GetAxis("V");
			}
		}
		if (aileronLeft != null)
		{
			aileronLeft.targetDeflection = -Input.GetAxis("H");
		}
		if (aileronRight != null)
		{
			aileronRight.targetDeflection = Input.GetAxis("H");
		}
		if (rudders != null && yawDefined)
		{
			// YOU MUST DEFINE A YAW AXIS FOR THIS TO WORK CORRECTLY.
			// Imported packages do not carry over changes to the Input Manager, so
			// to restore yaw functionality, you will need to add a "Yaw" axis.
			foreach (var rudder in rudders)
			{
				rudder.targetDeflection = Input.GetAxis("Y");
			}
		}

		if (engine != null)
		{
			// Fire 1 to speed up, Fire 2 to slow down. Make sure throttle only goes 0-1.
			throttle += Input.GetAxis("Fire1") * Time.deltaTime;
			throttle -= Input.GetAxis("Fire2") * Time.deltaTime;
			throttle = Mathf.Clamp01(throttle);

			engine.throttle = throttle;
		}
	}

	private float CalculatePitchG()
	{
		// Angular velocity is in radians per second.
		Vector3 localVelocity = transform.InverseTransformDirection(Rigidbody.velocity);
		Vector3 localAngularVel = transform.InverseTransformDirection(Rigidbody.angularVelocity);

		// Local pitch velocity (X) is positive when pitching down.

		// Radius of turn = velocity / angular velocity
		float radius = (Mathf.Approximately(localAngularVel.x, 0.0f)) ? float.MaxValue : localVelocity.z / localAngularVel.x;

		// The radius of the turn will be negative when in a pitching down turn.

		// Force is mass * radius * angular velocity^2
		float verticalForce = (Mathf.Approximately(radius, 0.0f)) ? 0.0f : (localVelocity.z * localVelocity.z) / radius;

		// Express in G (Always relative to Earth G)
		float verticalG = verticalForce / -9.81f;

		// Add the planet's gravity in. When the up is facing directly up, then the full
		// force of gravity will be felt in the vertical.
		verticalG += transform.up.y * (Physics.gravity.y / -9.81f);

		return verticalG;
	}
}
