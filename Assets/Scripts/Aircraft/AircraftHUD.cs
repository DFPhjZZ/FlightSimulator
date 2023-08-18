using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AircraftHUD : MonoBehaviour
{
    [SerializeField]
    float updateRate;
    [SerializeField]
    Color normalColor;
    [SerializeField]
    Color lockColor;
    [SerializeField]
    List<GameObject> helpDialogs;
    [SerializeField]
    Compass compass;
    [SerializeField]
    PitchLadder pitchLadder;
    [SerializeField]
    Bar throttleBar;
    [SerializeField]
    Transform hudCenter;
    [SerializeField]
    Transform velocityMarker;
    [SerializeField]
    Text airspeed;
    [SerializeField]
    Text aoaIndicator;
    [SerializeField]
    Text gforceIndicator;
    [SerializeField]
    Text altitude;
    [SerializeField]
    Bar healthBar;
    [SerializeField]
    Text healthText;
    [SerializeField]
    Transform targetBox;
    [SerializeField]
    Text pauseText;
    [SerializeField]
    Text respawnText;
    [SerializeField]
    GameObject helpPromptGO;
    [SerializeField]
    Text objectiveText;
    [SerializeField]
    Text scoreText;

    Aircraft aircraft;
    Transform aircraftTransform;
    new Camera camera;
    Transform cameraTransform;

    GameObject hudCenterGO;
    GameObject velocityMarkerGO;
    GameObject targetBoxGO;
    Image targetBoxImage;
    GameObject pauseTextGO;
    GameObject respawnTextGO;
    GameObject scoreGO;

    float lastUpdateTime;

    const float metersToKnots = 1.94384f;
    const float metersToFeet = 3.28084f;

    const string objective1 = "Take Off";
    const string objective2 = "Eliminate Hostiles";
    const string objective3 = "Touch Down";
    const string objective4 = "Mission Complete";
    private float timer = 300f;
    private int finalScore;

    void Start()
    {
        hudCenterGO = hudCenter.gameObject;
        velocityMarkerGO = velocityMarker.gameObject;
        targetBoxGO = targetBox.gameObject;
        targetBoxImage = targetBox.GetComponent<Image>();
        pauseTextGO = pauseText.gameObject;
        respawnTextGO = respawnText.gameObject;
        scoreGO = scoreText.gameObject;
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

        if (compass != null)
        {
            compass.SetAircraft(aircraft);
        }

        if (pitchLadder != null)
        {
            pitchLadder.SetAircraft(aircraft);
        }
    }

    public void SetCamera(Camera camera)
    {
        this.camera = camera;

        if (camera == null)
        {
            cameraTransform = null;
        }
        else
        {
            cameraTransform = camera.GetComponent<Transform>();
        }

        if (compass != null)
        {
            compass.SetCamera(camera);
        }

        if (pitchLadder != null)
        {
            pitchLadder.SetCamera(camera);
        }
    }

    public void ToggleHelpDialogs()
    {
        helpPromptGO.SetActive(!helpPromptGO.activeSelf);
    }

    void UpdateVelocityMarker()
    {
        var velocity = aircraftTransform.forward;

        if (aircraft.LocalVelocity.sqrMagnitude > 1)
        {
            velocity = aircraft.Rb.velocity;
        }

        var hudPos = TransformToHUDSpace(cameraTransform.position + velocity);

        if (hudPos.z > 0)
        {
            velocityMarkerGO.SetActive(true);
            velocityMarker.localPosition = new Vector3(hudPos.x, hudPos.y, 0);
        }
        else
        {
            velocityMarkerGO.SetActive(false);
        }
    }

    void UpdateAirspeed()
    {
        var speed = aircraft.LocalVelocity.z * metersToKnots;
        airspeed.text = string.Format("{0:0}", speed);
    }

    void UpdateAOA()
    {
        aoaIndicator.text = string.Format("{0:0.0} AOA", aircraft.AngleOfAttack * Mathf.Rad2Deg);
    }

    void UpdateGForce()
    {
        var gforce = aircraft.LocalGForce.y / 9.81f;
        gforceIndicator.text = string.Format("{0:0.0} G", gforce);
    }

    void UpdateAltitude()
    {
        var altitude = aircraft.Rb.position.y;// * metersToFeet;
        this.altitude.text = string.Format("{0:0}", altitude);
    }

    Vector3 TransformToHUDSpace(Vector3 worldSpace)
    {
        var screenSpace = camera.WorldToScreenPoint(worldSpace);
        return screenSpace - new Vector3(camera.pixelWidth / 2, camera.pixelHeight / 2);
    }

    void UpdateHUDCenter()
    {
        var rotation = cameraTransform.localEulerAngles;
        var hudPos = TransformToHUDSpace(cameraTransform.position + aircraftTransform.forward);

        if (hudPos.z > 0)
        {
            hudCenterGO.SetActive(true);
            hudCenter.localPosition = new Vector3(hudPos.x, hudPos.y, 0);
            hudCenter.localEulerAngles = new Vector3(0, 0, -rotation.z);
        }
        else
        {
            hudCenterGO.SetActive(false);
        }
    }

    void UpdateHealth()
    {
        healthBar.SetValue(aircraft.Health / aircraft.MaxHealth);
        healthText.text = string.Format("{0:0}", aircraft.Health);
    }

    void UpdateWeapon()
    {
        if (aircraft.Dead) return;

        targetBoxImage.color = normalColor;
        var rotation = cameraTransform.localEulerAngles;
        var targetBoxPos = TransformToHUDSpace(cameraTransform.position + aircraftTransform.forward);
        if (targetBoxPos.z > 0)
        {
            targetBoxGO.SetActive(true);
            targetBox.localPosition = new Vector3(targetBoxPos.x, targetBoxPos.y, 0);
        }
        else
        {
            targetBoxGO.SetActive(false);
        }

        if (aircraft.target != null && Vector3.Angle(aircraft.target.transform.position - aircraft.transform.position, aircraft.transform.forward) <= 60f)
        {
            var targetPos = TransformToHUDSpace(aircraft.target.transform.position);
            if (targetPos.z > 0)
            {
                targetBoxImage.color = lockColor;
                targetBox.localPosition = new Vector3(targetPos.x, targetPos.y, 0);
            }
        }
    }

    void UpdatePause()
    {
        if (CraftController.gamePaused)
        {
            pauseTextGO.SetActive(true);
        }
        else
        {
            pauseTextGO.SetActive(false);
        }
    }

    void UpdateRespawn()
    {
        if (aircraft.respawnReady)
        {
            respawnTextGO.SetActive(true);
        }
        else
        {
            respawnTextGO.SetActive(false);
        }
    }

    void UpdateObjective()
    {
        if (aircraft.objectiveID == 0)
        {
            objectiveText.text = objective1;
        }
        if (aircraft.objectiveID == 1)
        {
            objectiveText.text = objective2;
        }
        if (aircraft.objectiveID == 2)
        {
            objectiveText.text = objective3;
        }

        if (aircraft.objectiveID == 3)
        {
            objectiveText.text = objective4;
        }
    }

    void UpdateScore()
    {
        if (aircraft.objectiveID != 1 && aircraft.objectiveID != 3)
        {
            scoreGO.SetActive(false);
            if (aircraft.objectiveID == 2)
                finalScore = aircraft.score;
        }
        if (aircraft.objectiveID == 1)
        {
            if (timer <= 0f)
            {
                aircraft.objectiveID = 2;
                return;
            }
            scoreGO.SetActive(true);
            int minute = (int)(timer / 60f);
            int second = (int)(timer % 60f);
            scoreText.text = "Remaining Time: " + minute.ToString() + ":" + second.ToString() + "\n" + "Your Score: " + aircraft.score.ToString();
            timer -= Time.deltaTime;
        }

        if (aircraft.objectiveID == 3)
        {
            scoreGO.SetActive(true);
            scoreText.text = "Your Total Score is: " + finalScore.ToString();
        }
    }

    void LateUpdate()
    {
        if (aircraft == null) return;
        if (camera == null) return;

        throttleBar.SetValue(aircraft.Throttle);

        if (!aircraft.Dead)
        {
            UpdateVelocityMarker();
            UpdateHUDCenter();
            UpdateWeapon();
        }
        else
        {
            hudCenterGO.SetActive(false);
            velocityMarkerGO.SetActive(false);
            targetBoxGO.SetActive(false);
        }

        UpdateAirspeed();
        UpdateAltitude();
        UpdateHealth();
        
        UpdatePause();
        UpdateRespawn();
        UpdateObjective();
        UpdateScore();

        //update these elements at reduced rate to make reading them easier
        if (Time.time > lastUpdateTime + (1f / updateRate))
        {
            UpdateAOA();
            UpdateGForce();
            lastUpdateTime = Time.time;
        }
    }
}
