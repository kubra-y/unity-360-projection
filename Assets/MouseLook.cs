using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity; 
    public Transform playerBody; 

    private float xRotation = 0f;  // Track vertical rotation (up/down)
    private bool isDragging = false; // Track if the mouse is being dragged

    void Start()
    {
        // Lock the cursor to the center of the screen and make it invisible
        Cursor.lockState = CursorLockMode.None;  // Initially, don't lock the cursor
        Cursor.visible = true;  // Keep the cursor visible
    }

    void Update()
    {
        // Check if the left mouse button is clicked and held
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            Cursor.lockState = CursorLockMode.Locked;  // Lock the cursor when dragging starts
            Cursor.visible = false;  // Hide the cursor when dragging
        }

        // Check if the left mouse button is released
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            Cursor.lockState = CursorLockMode.None;  // Unlock the cursor when dragging stops
            Cursor.visible = true;  // Show the cursor when not dragging
        }

        // Only rotate the camera when dragging
        if (isDragging)
        {
            // Get mouse movement input
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // Invert the rotations
            mouseX = -mouseX;
            mouseY = -mouseY;

            // Adjust vertical rotation (looking up and down)
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // Limit vertical rotation so you can't flip over

            // Apply vertical rotation to the camera (looking up/down)
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // Apply horizontal rotation to the player body (looking left/right)
            playerBody.Rotate(Vector3.up * mouseX);  // Rotates around the Y-axis for left/right movement
        }
    }
}

