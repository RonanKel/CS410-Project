using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereControl : MonoBehaviour
{
    public float tiltSpeed = 100f;

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 rotation = new Vector3(moveVertical, 0.0f, -moveHorizontal) * tiltSpeed * Time.deltaTime;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
    }
}
