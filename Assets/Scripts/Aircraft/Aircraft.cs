using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RayFire;
using Unity.VisualScripting;
using Random = System.Random;

public class Aircraft : MonoBehaviour
{
    [SerializeField]
    float maxHealth;
    [SerializeField]
    float health;
    [SerializeField]
    float maxThrust;
    [SerializeField]
    float throttleSpeed;
    [SerializeField]
    float gLimit;
    [SerializeField]
    float gLimitPitch;

    [Header("Misc")]
    [SerializeField]
    List<Collider> landingGear;
    [SerializeField]
    public List<Transform> landingGearModels;
    [SerializeField]
    PhysicMaterial landingGearBrakesMaterial;
    [SerializeField]
    List<GameObject> graphics;
    [SerializeField]
    GameObject damageEffect;
    [SerializeField]
    GameObject deathEffect;
    [SerializeField]
    private float respawnTime;
    [SerializeField]
    bool flapsDeployed;
    [SerializeField]
    float initialSpeed = 500f;
    [SerializeField]
    float flapsRetractSpeed;

    [Header("Weapons")]
    [SerializeField]
    List<Transform> hardpoints;
    [SerializeField]
    float missileReloadTime;
    [SerializeField]
    float missileDebounceTime;
    [SerializeField]
    GameObject missilePrefab;
    [SerializeField]
    float missileMaxLockDistance;
    [SerializeField]
    RayfireGun rayFireGun;
    [SerializeField]
    Transform rayFireGunTarget;
    [SerializeField]
    [Tooltip("Firing rate in Rounds Per Minute")]
    float cannonFireRate;
    [SerializeField]
    float cannonDebounceTime;
    [SerializeField]
    float cannonSpread;
    [SerializeField]
    Transform cannonSpawnPoint;
    [SerializeField]
    GameObject bulletPrefab;

    [Header("Surfaces")]
    public List<ControlSurface> elevators;
    public ControlSurface aileronLeft;
    public ControlSurface aileronRight;
    public List<ControlSurface> rudders;

    [Header("Audios")]
    [SerializeField]
    private AudioSource missileAudio;
    [SerializeField]
    private AudioSource cannonAudio;
    [SerializeField]
    private AudioSource deadAudio;
    [SerializeField] 
    private AudioSource engineAudio;

    new AircraftAnimation animation;

    float throttleInput;
    Vector3 controlInput;

    Vector3 lastVelocity;

    private float respawnTimer = 0f;
    private Vector3 respawnPoint;

    private float stillAltitude;
    private float inAirLimitHeight;

    // Weapons
    int missileIndex;
    List<float> missileReloadTimers;
    float missileDebounceTimer;
    Vector3 missileLockDirection;

    bool cannonFiring;
    float cannonDebounceTimer;
    float cannonFiringTimer;

    private bool inWindArea;
    private WindArea windArea;

    private Plane[] missileLockPlanes;
    private Camera cam;

    PhysicMaterial landingGearDefaultMaterial;

    private bool groundCheck = false;
    
    public float MaxHealth
    {
        get
        {
            return maxHealth;
        }
        set
        {
            maxHealth = Mathf.Max(0, value);
        }
    }

    public float Health
    {
        get
        {
            return health;
        }
        private set
        {
            health = Mathf.Clamp(value, 0, maxHealth);

            if (health <= MaxHealth * .5f && health > 0)
            {
                damageEffect.SetActive(true);
            }
            else
            {
                damageEffect.SetActive(false);
            }

            if (health == 0 && MaxHealth != 0 && !Dead)
            {
                Die();
            }
        }
    }

    public bool Dead { get; private set; }
    public bool respawnReady { get; set; }

    public Rigidbody Rb { get; private set; }
    public float Throttle { get; private set; }
    public Vector3 Velocity { get; private set; }
    public Vector3 LocalVelocity { get; private set; }
    public Vector3 LocalGForce { get; private set; }
    public Vector3 LocalAngularVelocity { get; private set; }
    public float AngleOfAttack { get; private set; }
    public float AngleOfAttackYaw { get; private set; }
    public bool AirbrakeDeployed { get; private set; }

    public Hostile target { get; set; }

    public int score { get; set; }
    public int objectiveID { get; set; }

    public bool FlapsDeployed
    {
        get
        {
            return flapsDeployed;
        }
        private set
        {
            flapsDeployed = value;
        }
    }

    public RayfireGun cannon
    {
        get
        {
            return rayFireGun;
        }
    }

    public Transform cannonTarget
    {
        get
        {
            return rayFireGunTarget;
        }
    }

    public List<Hostile> hostileObjects { get; set; }
    public bool landingGearDown { get; set; }
    public bool landingGearStatus { get; set; }

    private void Awake()
    {
        Dead = false;

        landingGearDown = true;
        landingGearStatus = false;

        hostileObjects = new List<Hostile>();

        respawnReady = false;

        objectiveID = 0;

        respawnPoint = new Vector3(5000f, 2750f, 0f);

    }

    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<AircraftAnimation>();
        Rb = GetComponent<Rigidbody>();

        if (landingGear.Count > 0)
        {
            landingGearDefaultMaterial = landingGear[0].sharedMaterial;
        }

        // Initiate Missile Timer
        missileReloadTimers = new List<float>(hardpoints.Count);
        foreach (var h in hardpoints)
        {
            missileReloadTimers.Add(0);
        }
        missileLockDirection = Vector3.forward;

        respawnTimer = 0f;

        // Rb.velocity = Rb.rotation * new Vector3(0, 0, initialSpeed);

        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        
        stillAltitude = Rb.transform.position.y;
        inAirLimitHeight = 20f;

        engineAudio.time = 0.1f;
    }

    public void SetThrottleInput(float input)
    {
        if (Dead) return;
        throttleInput = input;
    }

    public void SetControlInput(Vector3 input)
    {
        if (Dead) return;
        controlInput = input;
    }

    public void SetCannonInput(bool input)
    {
        if (Dead) return;
        cannonFiring = input;
    }

    public void TryFireMissile()
    {
        if (Dead) return;

        //try all available missiles
        for (int i = 0; i < hardpoints.Count; i++)
        {
            var index = (missileIndex + i) % hardpoints.Count;
            if (missileDebounceTimer == 0 && missileReloadTimers[index] == 0)
            {
                FireMissile(index);

                missileIndex = (index + 1) % hardpoints.Count;
                missileReloadTimers[index] = missileReloadTime;
                missileDebounceTimer = missileDebounceTime;

                animation.ShowMissileGraphic(index, false);
                break;
            }
        }
    }

    void FireMissile(int index)
    {
        var hardpoint = hardpoints[index];
        var missileGO = Instantiate(missilePrefab, hardpoint.position, hardpoint.rotation);
        var missile = missileGO.GetComponent<Missile>();
        if (target != null)
        {
            missile.target = target;
        }
        missile.Launch(this);
        missileAudio.Play();
    }

    void UpdateCannon(float dt)
    {
        if (cannonFiring && cannonFiringTimer == 0)
        {
            cannonFiringTimer = 60f / cannonFireRate;

            var spread = UnityEngine.Random.insideUnitCircle * cannonSpread;

            var bulletGO = Instantiate(bulletPrefab, cannonSpawnPoint.position, cannonSpawnPoint.rotation * Quaternion.Euler(spread.x, spread.y, 0));
            var bullet = bulletGO.GetComponent<Bullet>();
            bullet.Fire(this);
            cannonAudio.Play();
        }
    }

    private void UpdateWeaponsTimer(float dt)
    {
        missileDebounceTimer = Mathf.Max(0f, missileDebounceTimer - dt);
        cannonDebounceTimer = Mathf.Max(0, cannonDebounceTimer - dt);
        cannonFiringTimer = Mathf.Max(0, cannonFiringTimer - dt);

        for (int i = 0; i < missileReloadTimers.Count; i++)
        {
            missileReloadTimers[i] = Mathf.Max(0f, missileReloadTimers[i] - dt);

            if (missileReloadTimers[i] == 0f)
            {
                animation.ShowMissileGraphic(i, true);
            }
        }
    }

    void UpdateWeapons(float dt)
    {
        UpdateWeaponsTimer(dt);
        UpdateCannon(dt);
    }

    public void ToggleFlaps()
    {
        if (LocalVelocity.z < flapsRetractSpeed)
        {
            FlapsDeployed = !FlapsDeployed;
        }
    }

    public void ApplyDamage(float damage)
    {
        Health -= damage;
    }

    void Die()
    {
        throttleInput = 0;
        Throttle = 0;
        Dead = true;
        cannonFiring = false;

        damageEffect.GetComponent<ParticleSystem>().Pause();
        deathEffect.SetActive(true);

        deadAudio.Play();
    }

    public void Respawn()
    {
        Rb.position = respawnPoint;

        Throttle = maxThrust;
        Dead = false;
        respawnReady = false;

        health = maxHealth;

        deathEffect.SetActive(false);
        foreach (var g in graphics)
        {
            g.SetActive(true);
        }

        Rb.isKinematic = false;
        Rb.AddForce(Vector3.forward * 300f, ForceMode.VelocityChange);
    }

    void CalculateAngleOfAttack()
    {
        if (LocalVelocity.sqrMagnitude < 0.1f)
        {
            AngleOfAttack = 0;
            AngleOfAttackYaw = 0;
            return;
        }

        AngleOfAttack = Mathf.Atan2(-LocalVelocity.y, LocalVelocity.z);
        AngleOfAttackYaw = Mathf.Atan2(LocalVelocity.x, LocalVelocity.z);
    }

    void UpdateFlaps()
    {
        if (LocalVelocity.z > flapsRetractSpeed)
        {
            FlapsDeployed = false;
        }
    }

    void CalculateState(float dt)
    {
        var invRotation = Quaternion.Inverse(Rb.rotation);
        Velocity = Rb.velocity;
        LocalVelocity = invRotation * Velocity;  //transform world velocity into local space
        LocalAngularVelocity = invRotation * Rb.angularVelocity;  //transform into local space

        CalculateAngleOfAttack();
    }

    void CalculateGForce(float dt)
    {
        var invRotation = Quaternion.Inverse(Rb.rotation);
        var acceleration = (Velocity - lastVelocity) / dt;
        LocalGForce = invRotation * acceleration;
        lastVelocity = Velocity;
    }

    void UpdateThrottle(float dt)
    {
        float target = 0;
        if (throttleInput > 0)
        {
            target = 1;
        }
        // Debug.Log(throttleInput);

        //throttle input is [-1, 1]
        //throttle is [0, 1]
        Throttle = Utilities.MoveTo(Throttle, target, throttleSpeed * Mathf.Abs(throttleInput), dt);

        AirbrakeDeployed = Throttle == 0 && throttleInput == -1;

        if (AirbrakeDeployed)
        {
            foreach (var lg in landingGear)
            {
                lg.sharedMaterial = landingGearBrakesMaterial;
            }
        }
        else
        {
            foreach (var lg in landingGear)
            {
                lg.sharedMaterial = landingGearDefaultMaterial;
            }
        }
    }

    void UpdateThrust()
    {
        Rb.AddRelativeForce(Throttle * maxThrust * Vector3.forward);
    }

    Vector3 CalculateGForce(Vector3 angularVelocity, Vector3 velocity)
    {
        //estiamte G Force from angular velocity and velocity
        //Velocity = AngularVelocity * Radius
        //G = Velocity^2 / R
        //G = (Velocity * AngularVelocity * Radius) / Radius
        //G = Velocity * AngularVelocity
        //G = V cross A
        return Vector3.Cross(angularVelocity, velocity);
    }

    Vector3 CalculateGForceLimit(Vector3 input)
    {
        return Utilities.Scale6(input,
            gLimit, gLimitPitch,    //pitch down, pitch up
            gLimit, gLimit,         //yaw
            gLimit, gLimit          //roll
        ) * 9.81f;
    }

    float CalculateGLimiter(Vector3 controlInput, Vector3 maxAngularVelocity)
    {
        if (controlInput.magnitude < 0.01f)
        {
            return 1;
        }

        //if the player gives input with magnitude less than 1, scale up their input so that magnitude == 1
        var maxInput = controlInput.normalized;

        var limit = CalculateGForceLimit(maxInput);
        var maxGForce = CalculateGForce(Vector3.Scale(maxInput, maxAngularVelocity), LocalVelocity);

        if (maxGForce.magnitude > limit.magnitude)
        {
            //example:
            //maxGForce = 16G, limit = 8G
            //so this is 8 / 16 or 0.5
            return limit.magnitude / maxGForce.magnitude;
        }

        return 1;
    }

    Bounds CalculateBounds(Transform parent)
    {
        Vector3 center = Vector3.zero;
        Renderer[] renderers = parent.GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            center += renderer.bounds.center;
        }
        center /= parent.GetComponentsInChildren<Transform>().Length;

        Bounds bounds = new Bounds(center, Vector3.zero);
        foreach (var renderer in renderers)
        {
            bounds.Encapsulate(renderer.bounds);
        }

        return bounds;
    }

    // Update is called once per frame
    void Update()
    {
        // Control input for control surfaces (ailerons, rudders, and elevators)
        if (!CraftController.gamePaused)
        {
            if (elevators != null)
            {
                foreach (var elevator in elevators)
                {
                    elevator.targetDeflection = controlInput.x;
                }
            }
            if (aileronLeft != null)
            {
                aileronLeft.targetDeflection = -controlInput.z;
            }
            if (aileronRight != null)
            {
                aileronRight.targetDeflection = controlInput.z;
            }

            if (rudders != null)
            {
                foreach (var rudder in rudders)
                {
                    rudder.targetDeflection = controlInput.y;
                }
            }
        }

        // game stage (for HUD mission info update)
        if (objectiveID == 0 && transform.position.y >= stillAltitude + inAirLimitHeight)
        {
            objectiveID = 1;
        }
        if (groundCheck && !Dead && Rb.velocity.z <= 0.9f)
        {
            objectiveID = 3;
        }
        
        // engine audio
        if (!engineAudio.isPlaying && Throttle > 0f)
        {
            engineAudio.Play();
            engineAudio.volume = Throttle / 1.0f * 0.1f;
            engineAudio.SetScheduledEndTime(AudioSettings.dspTime + 0.36f);
        }
        else if (engineAudio.isPlaying && Throttle <= 0f)
        {
            engineAudio.Pause();
        }

        // check if there is any hostile in range, if so, lock it
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);
        Random rnd = new Random();
        // if (hostileObjects.Count != 0) Debug.Log("Hostiles In Range: " + hostileObjects.Count);
        if (hostileObjects.Count != 0 && target == null)
        {
            int idx = rnd.Next(0, hostileObjects.Count - 1);
            var potentialHostile = hostileObjects[idx];
            if (potentialHostile != null && potentialHostile.gameObject.activeSelf &&
                GeometryUtility.TestPlanesAABB(planes, CalculateBounds(potentialHostile.transform))
                && Vector3.Distance(potentialHostile.transform.position, transform.position) <= missileMaxLockDistance)
            {
                target = potentialHostile;
            }
        }

        // respawn logic
        if (Dead && !respawnReady)
        {
            respawnTimer += Time.deltaTime;
            if (respawnTimer >= respawnTime)
            {
                respawnReady = true;
                respawnTimer = 0f;
            }
        }
    }

    private void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;

        if (inWindArea)
        {
            // Debug.Log("111");
            Rb.AddForce(windArea.Direction * windArea.Strength, ForceMode.Force);
            Debug.DrawRay(Rb.position, windArea.Direction * windArea.Strength * 0.001f, Color.green);
        }

        //calculate at start, to capture any changes that happened externally
        CalculateState(dt);
        CalculateGForce(dt);
        UpdateFlaps();

        //handle user input
        UpdateThrottle(dt);

        if (!Dead)
        {
            //apply updates
            UpdateThrust();
        }
        else
        {
            Vector3 deadUp = Rb.rotation * Vector3.up;
            Vector3 deadForward = Rb.velocity.normalized;
            Rb.rotation = Quaternion.LookRotation(deadForward, deadUp);
        }

        //calculate again, so that other systems can read this plane's state
        CalculateState(dt);

        UpdateWeapons(dt);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WindArea"))
        {
            // Debug.Log("Enter");
            inWindArea = true;
            windArea = other.gameObject.GetComponent<WindArea>();
        }

        if (other.CompareTag("RespawnCheckbox"))
        {
            respawnPoint = other.transform.position + other.GetComponent<Respawn>().respawnPointOffset;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WindArea"))
        {
            inWindArea = false;
            windArea = null;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            var contact = collision.contacts[i];

            if (landingGear.Contains(contact.thisCollider))
            {
                if (objectiveID == 2) groundCheck = true;
                return;
            }

            Health = 0;
            Debug.Log(Dead);

            Rb.isKinematic = true;
            Rb.position = contact.point;
            Rb.rotation = Quaternion.Euler(0, Rb.rotation.eulerAngles.y, 0);

            foreach (var go in graphics)
            {
                go.SetActive(false);
            }

            return;
        }
    }
}
