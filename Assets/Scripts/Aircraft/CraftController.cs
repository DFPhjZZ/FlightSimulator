using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;

public class CraftController : MonoBehaviour
{
    [SerializeField] Aircraft aircraft;
    [SerializeField] AircraftHUD aircfratHUD;
    [SerializeField] new Camera camera;

    Vector3 controlInput;
    AircraftCamera aircraftCamera;

    public static bool gamePaused = false;

    void Start()
    {
        aircraftCamera = GetComponent<AircraftCamera>();
        SetAircraft(aircraft); //SetPlane if var is set in inspector
    }

    void SetAircraft(Aircraft aircraft)
    {
        this.aircraft = aircraft;

        if (aircfratHUD != null)
        {
            aircfratHUD.SetAircraft(aircraft);
            aircfratHUD.SetCamera(camera);
        }

        aircraftCamera.SetAircraft(aircraft);
    }

    // public void OnToggleHelp(InputAction.CallbackContext context) {
    //     if (plane == null) return;
    //
    //     if (context.phase == InputActionPhase.Performed) {
    //         planeHUD.ToggleHelpDialogs();
    //     }
    // }

    public void SetThrottleInput(InputAction.CallbackContext context)
    {
        if (aircraft == null) return;
        aircraft.SetThrottleInput(context.ReadValue<float>());
    }

    public void OnRollPitchInput(InputAction.CallbackContext context)
    {
        if (aircraft == null) return;

        var input = context.ReadValue<Vector2>();
        controlInput = new Vector3(input.y, controlInput.y, -input.x);
    }

    public void OnRollInput(InputAction.CallbackContext context)
    {
        if (aircraft == null) return;

        var input = context.ReadValue<float>();
        controlInput = new Vector3(controlInput.x, controlInput.y, -input);
    }

    public void OnPitchInput(InputAction.CallbackContext context)
    {
        if (aircraft == null) return;

        var input = context.ReadValue<float>();
        controlInput = new Vector3(input, controlInput.y, controlInput.z);
    }

    public void OnYawInput(InputAction.CallbackContext context)
    {
        if (aircraft == null) return;

        var input = context.ReadValue<float>();
        controlInput = new Vector3(controlInput.x, input, controlInput.z);
    }

    public void OnCameraInput(InputAction.CallbackContext context)
    {
        if (aircraft == null) return;

        var input = context.ReadValue<Vector2>();
        aircraftCamera.SetInput(input);
    }

    public void OnFireCannon(InputAction.CallbackContext context)
    {
        if (aircraft == null) return;

        if (context.phase == InputActionPhase.Started)
        {
            aircraft.SetCannonInput(true);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            aircraft.SetCannonInput(false);
        }
    }

    public void OnFireMissile(InputAction.CallbackContext context)
    {
        if (aircraft == null) return;

        if (context.phase == InputActionPhase.Performed)
        {
            aircraft.TryFireMissile();
        }
    }

    public void OnLandingGear(InputAction.CallbackContext context)
    {
        aircraft.landingGearDown = !aircraft.landingGearDown;
        aircraft.landingGearStatus = true;
    }

    public void OnRespawn(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && aircraft.respawnReady)
        {
            aircraft.Respawn();
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        gamePaused = !gamePaused;
        if (gamePaused) Time.timeScale = 0f;
        else Time.timeScale = 1f;
    }

    // public void OnFlapsInput(InputAction.CallbackContext context) {
    //     if (plane == null) return;
    //
    //     if (context.phase == InputActionPhase.Performed) {
    //         plane.ToggleFlaps();
    //     }
    // }

    void Update()
    {
        if (aircraft == null) return;

        aircraft.SetControlInput(controlInput);
    }
}
