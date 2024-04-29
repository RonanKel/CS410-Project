using UnityEngine;

public class WorldTilter : MonoBehaviour
{
    public float tiltSpeed = 50f; // Adjust the speed of tilting
    public Transform cameraTransform; // Reference to the camera transform

    void Update()
    {
        // Lock the cursor when the game is running
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (cameraTransform == null) {
            Debug.LogError("Camera Transform is not assigned in the inspector");
            return;
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate tilt based on camera view
        Vector3 forward = cameraTransform.forward;
        forward.y = 0; // Keep the tilt horizontal
        forward.Normalize();

        Vector3 right = cameraTransform.right;
        right.y = 0;
        right.Normalize();

        Vector3 desiredMoveDirection = (forward * moveVertical + right * moveHorizontal).normalized;

        // Calculate the rotation to apply to the objects
        Vector3 rotation = Vector3.Cross(Vector3.up, desiredMoveDirection) * tiltSpeed * Time.deltaTime;

        // Apply the rotation to each tiltable object
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Tiltable"))
        {
            obj.transform.Rotate(rotation, Space.World);
        }
    }
}
