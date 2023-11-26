using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayerMotor
{
    public static event Action<float> OnDamaged;

    [Header("Movement")]
    [Header("Controls")]
    [SerializeField]
    private KeyCode down = KeyCode.DownArrow;
    [SerializeField]
    private KeyCode up = KeyCode.UpArrow;
    [SerializeField]
    private KeyCode left = KeyCode.LeftArrow;
    [SerializeField]
    private KeyCode right = KeyCode.RightArrow;
    [SerializeField]
    private KeyCode jump = KeyCode.Space;

    [Header("Alt Movement")]
    [SerializeField]
    private KeyCode downAlt = KeyCode.S;
    [SerializeField]
    private KeyCode upAlt = KeyCode.W;
    [SerializeField]
    private KeyCode leftAlt = KeyCode.A;
    [SerializeField]
    private KeyCode rightAlt = KeyCode.D;
    [SerializeField]
    private KeyCode jumpAlt = KeyCode.Space;

    [Header("Misc")]
    [SerializeField]
    private KeyCode speedKey = KeyCode.LeftShift;
    [SerializeField]
    private KeyCode slowMoKey = KeyCode.Q;


    [Header("Stats")]
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private float speedBoost = 2f;
    [SerializeField]
    private float sprintDrain = 1f;
    [SerializeField]
    private float sprintRegainFactor = 0.5f;
    [SerializeField]
    private float healthCamShakeThreshold = 2f;

    [Header("GameObject")]
    [SerializeField]
    private Camera camera;

    public float sprintAmount = 1f;

    private float timeScaleNormal = 1f;
    private float timeScaleSlow = 0.05f;

    public float desiredTimeScale = 1f;

    public float maxHealth = 100f;
    private float _health = 100f;
    public float health
    {
        get
        {
            return _health;
        }
        set
        {
            if (_health - value > 0f)
                OnDamage(health - value);

            _health = value;
        }
    }

    public bool covered = false;

    public float maxSlowMo = 1f;
    public float slowMoStamina { get; private set; } = 1f;
    public float slowMoDrain = 0.2f;
    public float slowMoGain = 0.1f;

    public float slowMoTransitionSpeed = 2f;

    // special case: normally you would normally do:
    
    //  public void Update(){
    //      [lots of code]
    //  }
    
    protected override void OnUpdate() 
    {
        Movement();
        Rotation();
        SprintRegain();
        SlowMo();
    }

    private void Movement()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(down) || Input.GetKey(downAlt))
            movement += new Vector3(0f, 0f, -1f);
        if (Input.GetKey(up) || Input.GetKey(upAlt))
            movement += new Vector3(0f, 0f, 1f);
        if (Input.GetKey(right) || Input.GetKey(rightAlt))
            movement += new Vector3(1f, 0f, 0f);
        if (Input.GetKey(left) || Input.GetKey(leftAlt))
            movement += new Vector3(-1f, 0f, 0f);
        
            
        if (Input.GetKey(speedKey) && Sprint())
            movement *= speedBoost;

        desiredPosition += movement * speed * Time.deltaTime;
        desiredPosition = new Vector3(desiredPosition.x, 1f, desiredPosition.z);
    }

    private bool Sprint()
    {
        sprintAmount -= sprintDrain * Time.deltaTime;
        return sprintAmount > 0f;
    }

    // IGNORE THE ROTATION MATH
    private void Rotation()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(new Vector3(0f, 1f, 0f), transform.position);

        plane.Raycast(ray, out float distance);

        Vector3 hit = ray.GetPoint(distance);
        Debug.DrawLine(transform.position, hit);


        desiredRotation = Quaternion.LookRotation((hit - transform.position).normalized, Vector3.up);
    }


    private void SprintRegain()
    {
        sprintAmount += sprintDrain * Time.deltaTime * sprintRegainFactor;
        sprintAmount = Mathf.Clamp(sprintAmount, 0f, 1f);
    }

    private void SlowMo()
    {
        if (!PauseMenu.GamePaused)
        {
            if (Input.GetKey(slowMoKey))
            {
                if (CheckSlowMo())
                    desiredTimeScale = timeScaleSlow;
                else
                {
                    desiredTimeScale = timeScaleNormal;
                    slowMoStamina += Time.unscaledDeltaTime * slowMoGain;
                }
            }
            else
            {
                desiredTimeScale = timeScaleNormal;
                slowMoStamina += Time.unscaledDeltaTime * slowMoGain;
            }
        }
        slowMoStamina = Mathf.Clamp(slowMoStamina, -1f, maxSlowMo);

        if(!PauseMenu.GamePaused)
            Time.timeScale = Mathf.Lerp(Time.timeScale, desiredTimeScale, Time.unscaledDeltaTime * slowMoTransitionSpeed);
    }

    private bool CheckSlowMo()
    {
        slowMoStamina -= Time.unscaledDeltaTime * slowMoDrain;
        return slowMoStamina > 0f;
    }
    

    private void OnDamage(float amount)
    {
        if (amount > healthCamShakeThreshold)
            CamBase.CameraShake();

        desiredTimeScale = 0.1f;
        OnDamaged?.Invoke(amount);
    }

    // IGNORE THIS SECTION
    private void OnTriggerEnter(Collider other)
    {
        MapObject m = other.GetComponent<MapObject>();
        if (m)
        {
            covered = m.windCoverage;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        covered = false;
    }

}
