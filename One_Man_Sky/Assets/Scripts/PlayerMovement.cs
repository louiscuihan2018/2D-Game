using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement: MonoBehaviour
{
    //This movement set uses acceleration and rotation, but has fast speed and turn burnoff.
    #region player_variables
    Rigidbody2D rb;
    #endregion

    #region fixed_movement_variables
    float fastShipAccel = 1;
    float shipDecel = 0.5f;
    float fastShipDecel = 1;
    #endregion

    #region movement_variables
    [Tooltip("This is the player's top speed. (Default: 50)")]
    public float topSpeed = 50;

    [Tooltip("This is how fast the player is going. (Default: 0)")]
    public float currentSpeed = 0;

    [Tooltip("This is how fast the player accelerates past half the total speed. (Default: 0.1f)")]
    public float shipAccel = 0.1f;

    [Tooltip("This linearly scales the turning speed of the player. (Default: 7)")]
    public float turningSpeed = 7f;

    [Tooltip("This linearly scales the turning burnoff of the player. (Default: 0.1f)")]
    public float turningBurnoff = 0.1f;

    float currentTurnSpeed = 0;
    #endregion

    #region boost_variables
    [Tooltip("This is how long it takes for boost to reset. (Default: 10)")]
    public float boostCooldownTime = 10f;
    public float boostAccelModifier = 3f;
    public float boostStartup = 0.25f;
    public float boostDuration = 2f;
    float timeSinceBoost = 0;
    public Slider CD;
    #endregion

    #region unity_functions
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        timeSinceBoost = boostCooldownTime;
        CD.value = timeSinceBoost / boostCooldownTime;
    }

    // Update is called once per frame
    void Update()
    {
        WASDMovement();
        BoostMovement();
        HandleTurning();
        timeSinceBoost += Time.deltaTime;
        CD.value = timeSinceBoost / boostCooldownTime;
        if (timeSinceBoost > boostCooldownTime)
        {
            timeSinceBoost = boostCooldownTime + .1f;
        }
        //Debug.DrawRay(transform.position, transform.up*currentSpeed, Color.blue);
    }
    #endregion

    #region movement_functions
    void WASDMovement()
    {
        //Check if we're accelerating (W) or decelerating (S).
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && currentSpeed < topSpeed)
        {
            if (currentSpeed < topSpeed / 2)
            {
                currentSpeed += fastShipAccel;
            }
            else
            {
                currentSpeed += shipAccel;
            }
        }
        else if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && currentSpeed > topSpeed - 2f && currentSpeed < topSpeed + 2f)
        {
            currentSpeed = topSpeed;
        }
        else
        {
            if (currentSpeed > 0)
            {
                currentSpeed -= shipDecel;
            }
            if (currentSpeed > 2*topSpeed)
            {
                currentSpeed -= fastShipDecel;
            }
        }

        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && currentSpeed > 0)
        {
            currentSpeed -= fastShipDecel;
        }

        //If you have negative speed, drop it to zero. No reversing in space.
        if (currentSpeed < 0)
        {
            currentSpeed = 0;
        }

        //Update position according to the current speed.
        if (currentSpeed > 0)
        {
            transform.Translate(Vector3.up * Time.deltaTime * currentSpeed);

        }
    }

    void BoostMovement()
    {
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && Input.GetKey(KeyCode.LeftShift) && timeSinceBoost > boostCooldownTime)
        {
            timeSinceBoost = 0;
            StartCoroutine(boostMovement());
            StartCoroutine(boostCamera());
        }
    }

    void HandleTurning()
    {
        currentTurnSpeed += -Input.GetAxis("Horizontal") * turningSpeed;
        Quaternion desiredRotation = Quaternion.Euler(0, 0, currentTurnSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * turningBurnoff);    
    }

    public void DebuffSpeed(float amount)
    {
        if (currentSpeed > 0)
        {
            currentSpeed -= amount;
        }
        if (currentSpeed < 0)
        {
            currentSpeed = 0;
        }
    }
    #endregion

    #region movement_coroutines

    IEnumerator boostMovement()
    {
        float timer = 0;
        while (timer < boostStartup)
        {
            currentSpeed -= (boostAccelModifier/3)*fastShipDecel;
            timer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        while (timer < boostDuration)
        {
            currentSpeed += boostAccelModifier*fastShipAccel;
            timer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    IEnumerator boostCamera()
    {
        float defaultSize = Camera.main.orthographicSize;
        float timer = 0;
        while (timer < boostStartup)
        {
            timer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        while (timer < boostDuration)
        {
            Camera.main.orthographicSize = Camera.main.orthographicSize + Time.deltaTime*15;
            timer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return new WaitForSeconds(1f);
        while (Camera.main.orthographicSize > defaultSize)
        {
            Camera.main.orthographicSize = Camera.main.orthographicSize - Time.deltaTime*10;
            timer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Camera.main.orthographicSize = defaultSize;
    }
    #endregion
}
