using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereControl : MonoBehaviour
{
    public float tiltSpeed = 100f;
    public Transform cameraTransform; // Reference to the camera transform

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody not found on the game object");
        }

        if (cameraTransform == null)
        {
            Debug.LogError("Camera Transform is not assigned in the inspector");
        }
    }

    void Update()
    {
        // Lock the cursor when the game is running
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (cameraTransform == null) return;

        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Calculate the rotation vector based on camera orientation
        Vector3 forwardFromCamera = cameraTransform.forward;
        forwardFromCamera.y = 0; // Ensure the tilt is only horizontal
        forwardFromCamera.Normalize();

        Vector3 rightFromCamera = cameraTransform.right;
        rightFromCamera.y = 0;
        rightFromCamera.Normalize();

        Vector3 desiredMoveDirection = (forwardFromCamera * moveVertical + rightFromCamera * moveHorizontal).normalized;

        // Apply the rotation to the world
        Vector3 rotation = Vector3.Cross(Vector3.up, desiredMoveDirection) * tiltSpeed * Time.deltaTime;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
    }
}