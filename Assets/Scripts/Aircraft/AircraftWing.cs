using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AircraftWing : MonoBehaviour
{
    [Tooltip("Size of the wing. The bigger the wing, the more lift it provides.")]
	public Vector2 dimensions = new Vector2(5f, 2f);

	[Tooltip("When true, wing forces will be applied only at the center of mass.")]
	public bool applyForcesToCenter = false;
	public bool isMainWing = false;
	
	[Tooltip("Lift coefficient curve.")] 
	[SerializeField]
	private AnimationCurve lift;
	[SerializeField]
	private AnimationCurve drag;
	[SerializeField] 
	private AnimationCurve testL;
	[SerializeField] 
	private AnimationCurve testD;
	[Tooltip("The higher the value, the more lift the wing applies at a given angle of attack.")]
	public float liftMultiplier = 1f;
	[Tooltip("The higher the value, the more drag the wing incurs at a given angle of attack.")]
	public float dragMultiplier = 1f;

	private Rigidbody rigid;

	private Vector3 liftDirection = Vector3.up;

	[Header("Debug")]
	[SerializeField]
	private float liftCoefficient = 0f;
	[SerializeField]
	private float dragCoefficient = 0f;
	[SerializeField]
	private float liftForce = 0f;
	[SerializeField]
	private float dragForce = 0f;
	[SerializeField]
	private float angleOfAttack = 0f;

	public Vector3 actualLiftForce;
	
	public float AngleOfAttack
	{
		get
		{
			if (rigid != null)
			{
				Vector3 localVelocity = transform.InverseTransformDirection(rigid.velocity);
				return angleOfAttack * -Mathf.Sign(localVelocity.y);
			}
			else
			{
				return 0.0f;
			}
		}
	}

	public float WingArea
	{
		get { return dimensions.x * dimensions.y; }
	}

	public Rigidbody Rigidbody
	{
		set { rigid = value; }
	}

	private void Awake()
	{
		// I don't especially like doing this, but there are many cases where wings might not
		// have the rigidbody on themselves (e.g. they are on a child gameobject of a plane).
		rigid = GetComponentInParent<Rigidbody>();
		
		// if (isMainWing)
		// {
		// 	lift = new AnimationCurve(new Keyframe(0.0f, 0.0f),
		// 		new Keyframe(16f,   1.1f),
		// 		new Keyframe(20f,   0.6f),
		// 		new Keyframe(45f, 0.9f),
		// 		new Keyframe(90f, 0.0f),
		// 		new Keyframe(135f, -0.9f),
		// 		new Keyframe(160f, -0.6f),
		// 		new Keyframe(164f, -1.1f),
		// 		new Keyframe(180f,  0.0f));
		// }
		// if (!isMainWing)
		// {
		// 	lift = new AnimationCurve(new Keyframe(0.0f, 0.0f),
		// 		new Keyframe(45f, 1.0f),
		// 		new Keyframe(90f, 0.0f),
		// 		new Keyframe(135f, -1.0f),
		// 		new Keyframe(180f, 0.0f));
		// }
		//
		//
		// drag = new AnimationCurve(new Keyframe(0.0f, 0.025f),
		// 	new Keyframe(90f,  isMainWing ? 1.8f : 2.0f),
		// 	new Keyframe(180f, 0.025f));

		testL = lift;
		testD = drag;
	}

	private void Start()
	{
		if (rigid == null)
		{
			Debug.LogError(name + ": SimpleWing has no rigidbody on self or parent!");
		}

		if (lift == null || drag == null)
		{
			Debug.LogError(name + ": SimpleWing has no defined wing curves!");
		}
		
		// If needed, uncomment these two lines 
		// liftMultiplier *= 2.0f;
		// dragMultiplier *= 0.25f;
	}

	private void Update()
	{
		// Prevent division by zero.
		if (dimensions.x <= 0f)
			dimensions.x = 0.01f;
		if (dimensions.y <= 0f)
			dimensions.y = 0.01f;

		// DEBUG
		if (rigid != null)
		{
			Debug.DrawRay(transform.position, liftDirection * liftForce * 0.0001f, Color.blue);
			Debug.DrawRay(transform.position, -rigid.velocity.normalized * dragForce * 0.0001f, Color.red);
		}
	}

	private void FixedUpdate()
	{
		if (rigid != null)
		{
			Vector3 forceApplyPos = (applyForcesToCenter) ? rigid.transform.TransformPoint(rigid.centerOfMass) : transform.position;

			Vector3 localVelocity = transform.InverseTransformDirection(rigid.GetPointVelocity(transform.position));
			localVelocity.x = 0f;

			// Angle of attack is used as the look up for the lift and drag curves.
			angleOfAttack = Vector3.Angle(Vector3.forward, localVelocity);
			liftCoefficient = lift.Evaluate(angleOfAttack);
			dragCoefficient = drag.Evaluate(angleOfAttack);

			// Calculate lift/drag.
			liftForce = localVelocity.sqrMagnitude * liftCoefficient * WingArea * liftMultiplier;
			dragForce = localVelocity.sqrMagnitude * dragCoefficient * WingArea * dragMultiplier;

			// Vector3.Angle always returns a positive value, so add the sign back in.
			liftForce *= -Mathf.Sign(localVelocity.y);

			// Lift is always perpendicular to air flow.
			liftDirection = Vector3.Cross(rigid.velocity, transform.right).normalized;
			rigid.AddForceAtPosition(liftDirection * liftForce, forceApplyPos, ForceMode.Force);
			actualLiftForce = liftDirection * liftForce;

			// Drag is always opposite of the velocity.
			rigid.AddForceAtPosition(-rigid.velocity.normalized * dragForce, forceApplyPos, ForceMode.Force);
		}
	}
// Prevent this code from throwing errors in a built game.
#if UNITY_EDITOR
	private void OnDrawGizmosSelected()
	{
		Matrix4x4 oldMatrix = Gizmos.matrix;

		Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
		Gizmos.DrawWireCube(Vector3.zero, new Vector3(dimensions.x, 0f, dimensions.y));

		Gizmos.matrix = oldMatrix;
	}
#endif
}
