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
    // Bind movement in the inspector for 2D Vector (WASD) or Add Binding -> Path -> Listen -> move Gamepad Stick for recognition

    [SerializeField] float controlSpeed = 30f;

    void Start()
    {
        
    }

    // *** NEW INPUT SYSTEM EXAMPLE ***
    // Enable and disable movement input action
    //void OnEnable()
    //{
        // movement.Enable();
    //}

    //void OnEnable()
    //{
        // movement.Disable();
    //}

    void Update()
    {
        float xThrow = Input.GetAxis("Horizontal");
        float yThrow = Input.GetAxis("Vertical");

        // *** NEW INPUT SYSTEM EXAMPLE ***
        // float horizontalThrow = movement.ReadValue<Vector2>().x;
        // float verticalThrow = movement.ReadValue<Vector2>().y;

        float xOffset = xThrow * controlSpeed * Time.deltaTime;
        float newXPos = transform.localPosition.x + xOffset;

        float yOffset = yThrow * controlSpeed * Time.deltaTime;
        float newYPos = transform.localPosition.y + yOffset;

        transform.localPosition = new Vector3(newXPos, newYPos, transform.localPosition.z);
    }
}
