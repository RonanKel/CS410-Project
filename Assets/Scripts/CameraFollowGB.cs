using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowGB : MonoBehaviour
{
    public Transform target; 
    public float distance = 10.0f;
    public Vector3 offset = new Vector3(0, 5, -10);

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
            transform.LookAt(target);
        }
    }
}
