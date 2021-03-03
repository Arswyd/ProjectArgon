using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    // *** NEW INPUT SYSTEM EXAMPLE ***
    // 1, Install Input System from package manager
    // 2, Project Settings -> Player -> Active Input Handling -> Both for legacy code
    // 3, using UnityEngine.InputSystem;
    // [SerializeField] InputAction movement;
    // [SerializeField] InputAction fire;
    // Bind movement in the inspector for 2D Vector (WASD) or Add Binding -> Path -> Listen -> move Gamepad Stick for recognition

    [Header("General")]
    [Tooltip("Player control speed")][SerializeField] float controlSpeed = 30f;
    [Tooltip("Player movement range in x axis")] [SerializeField] float xRange = 12f;
    [Tooltip("Player movement range in y axis")] [SerializeField] float yRange = 10f;

    [Header("Laser array")]
    [Tooltip("Add player lasers here")] [SerializeField] GameObject[] lasers;

    [Header("Position based tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float yawFactor = 2f;

    [Header("Control based tuning")]
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float rollFactor = -15f;

    float xThrow;
    float yThrow;

    void Start()
    {
        
    }

    // *** NEW INPUT SYSTEM EXAMPLE ***
    // Enable and disable movement input action
    //void OnEnable()
    //{
        // movement.Enable();
        // fire.Enable();
    //}

    //void OnEnable()
    //{
        // movement.Disable();
        // fire.Disable();
    //}

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        // *** NEW INPUT SYSTEM EXAMPLE ***
        // float horizontalThrow = movement.ReadValue<Vector2>().x;
        // float verticalThrow = movement.ReadValue<Vector2>().y;

        float xOffset = xThrow * controlSpeed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * controlSpeed * Time.deltaTime;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * yawFactor;
        float roll = xThrow * rollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            ToggleLasers(true);
        }
        else
        {
            ToggleLasers(false);
        }

        // *** NEW INPUT SYSTEM EXAMPLE ***
        //if (fire.ReadValue<float>() > 0.5)
        //{
        //    Debug.Log("Fire");
        //}
    }

    void ToggleLasers(bool isLaserActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isLaserActive;
        }
    }
}
