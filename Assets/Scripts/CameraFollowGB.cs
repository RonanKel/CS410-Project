using UnityEngine;

public class CameraFollowGB : MonoBehaviour
{
    public Transform target; // Target to follow (game ball)
    public float distance = 10.0f; // Fixed distance from the target
    public float sensitivityX = 3.0f; // Sensitivity of mouse movement in X direction
    public float sensitivityY = 3.0f; // Sensitivity of mouse movement in Y direction

    private float currentY = 0.0f; // Current angle in Y-axis
    private float currentX = 0.0f; // Current angle in X-axis

    void Awake()
    {
        target = GameObject.Find("GameBall").transform;
    }
    
    void Update()
    {
        if (target == null)
        {
            return;
        }

        // Update angles based on input
        currentX += Input.GetAxis("Mouse X") * sensitivityX;
        currentY -= Input.GetAxis("Mouse Y") * sensitivityY;
        // Clamp the vertical angle to prevent flipping
        currentY = Mathf.Clamp(currentY, -85, 85);
    }

    void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        // Calculate rotation and position based on current angles and the fixed distance
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 position = target.position - (rotation * Vector3.forward * distance);

        // Update camera position and rotation
        transform.position = position;
        transform.LookAt(target);
    }
}
