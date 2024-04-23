using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowGB : MonoBehaviour
{
    public Transform target; 
    public float distance = 10.0f;
    public Vector3 offset = new Vector3(0, 5, -10);
    public float sensitivityX = 4.0f;
    public float sensitivityY = 1.0f;

    private float currentY = 0.0f;
    private float currentX = 0.0f;

    void Update()
    {
        if (target != null)
        {
            // Update angles from input
            currentX += Input.GetAxis("Mouse X") * sensitivityX;
            currentY -= Input.GetAxis("Mouse Y") * sensitivityY;
            currentY = Mathf.Clamp(currentY, -85, 85);
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            transform.position = target.position - (rotation * Vector3.forward * distance) + offset;
            transform.LookAt(target);
        }
    }
}
