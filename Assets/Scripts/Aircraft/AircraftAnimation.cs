using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftAnimation : MonoBehaviour
{
    [SerializeField]
    List<GameObject> afterburnerGraphics;
    [SerializeField]
    float afterburnerThreshold;
    [SerializeField]
    float afterburnerMinSize;
    [SerializeField]
    float afterburnerMaxSize;
    [SerializeField]
    float maxAileronDeflection;
    [SerializeField]
    float maxElevatorDeflection;
    [SerializeField]
    float maxRudderDeflection;
    [SerializeField]
    float airbrakeDeflection;
    [SerializeField]
    float flapsDeflection;
    [SerializeField]
    float deflectionSpeed;
    [SerializeField]
    Transform rightAileron;
    [SerializeField]
    Transform leftAileron;
    [SerializeField]
    List<Transform> elevators;
    [SerializeField]
    List<Transform> rudders;
    [SerializeField]
    Transform airbrake;
    [SerializeField]
    List<Transform> flaps;
    [SerializeField]
    List<GameObject> missileGraphics;

    Aircraft aircraft;
    List<Transform> afterburnersTransforms;
    Dictionary<Transform, Quaternion> neutralPoses;
    Vector3 deflection;
    float airbrakePosition;
    float flapsPosition;
    Quaternion landingGearUp = Quaternion.Euler(90f, 0f, 0f);
    Quaternion landingGearDown = Quaternion.Euler(0f, 0f, 0f);
    private Quaternion landingGearTargetQuaternion;

    void Start() {
        aircraft = GetComponent<Aircraft>();
        afterburnersTransforms = new List<Transform>();
        neutralPoses = new Dictionary<Transform, Quaternion>();

        foreach (var go in afterburnerGraphics) {
            afterburnersTransforms.Add(go.GetComponent<Transform>());
        }

        AddNeutralPose(leftAileron);
        AddNeutralPose(rightAileron);

        foreach (var t in elevators) {
            AddNeutralPose(t);
        }

        foreach (var t in rudders) {
            AddNeutralPose(t);
        }

        AddNeutralPose(airbrake);

        foreach (var t in flaps) {
            AddNeutralPose(t);
        }
    }

    public void ShowMissileGraphic(int index, bool visible) {
        missileGraphics[index].SetActive(visible);
    }

    void AddNeutralPose(Transform transform) {
        neutralPoses.Add(transform, transform.localRotation);
    }

    Quaternion CalculatePose(Transform transform, Quaternion offset) {
        return neutralPoses[transform] * offset;
    }

    void UpdateAfterburners() {
        float throttle = aircraft.Throttle;
        float afterburnerT = Mathf.Clamp01(Mathf.InverseLerp(afterburnerThreshold, 1, throttle));
        float size = Mathf.Lerp(afterburnerMinSize, afterburnerMaxSize, afterburnerT);

        if (throttle >= afterburnerThreshold) {
            for (int i = 0; i < afterburnerGraphics.Count; i++) {
                afterburnerGraphics[i].SetActive(true);
                afterburnersTransforms[i].localScale = new Vector3(size, size, size);
            }
        } else {
            for (int i = 0; i < afterburnerGraphics.Count; i++) {
                afterburnerGraphics[i].SetActive(false);
            }
        }
    }

    void UpdateAirbrakes(float dt) {
        var target = aircraft.AirbrakeDeployed ? 1 : 0;

        airbrakePosition = Utilities.MoveTo(airbrakePosition, target, deflectionSpeed, dt);

        airbrake.localRotation = CalculatePose(airbrake, Quaternion.Euler(-airbrakePosition * airbrakeDeflection, 0, 0));
    }

    void UpdateFlaps(float dt) {
        var target = aircraft.FlapsDeployed ? 1 : 0;

        flapsPosition = Utilities.MoveTo(flapsPosition, target, deflectionSpeed, dt);

        foreach (var t in flaps) {
            t.localRotation = CalculatePose(t, Quaternion.Euler(flapsPosition * flapsDeflection, 0, 0));
        }
    }

    void UpdateLandingGears()
    {
        if (!aircraft.landingGearStatus) return;
        if (!aircraft.landingGearDown) landingGearTargetQuaternion = landingGearUp;
        else landingGearTargetQuaternion = landingGearDown;
        foreach (var lg in aircraft.landingGearModels)
        {
            // lg.localRotation = Quaternion.RotateTowards(lg.localRotation, Quaternion.Euler(landingGearRotationAngle, 0f, 0f), 90f);
            lg.localRotation = Quaternion.Slerp(lg.localRotation, landingGearTargetQuaternion, Time.fixedDeltaTime);
            if (lg.localRotation == landingGearTargetQuaternion)
            {
                aircraft.landingGearStatus = false;
            }
        }
    }

    void LateUpdate() {
        float dt = Time.deltaTime;

        if (!aircraft.Dead)
        {
            UpdateAfterburners();
            UpdateAirbrakes(dt);
            UpdateFlaps(dt);
            UpdateLandingGears();
        }
    }
}
