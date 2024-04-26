using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldRotationScript : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform playerTransform;
    // The speed the player would like to rotate the playtform
    [SerializeField] float rotationSpeed;

    // These keep track of the players input
    private float forwardInput;
    private float horizontalInput;

    // This is the rigidbody of the level
    Rigidbody rb;

    // This is the rotation variable the ends up rotation the level
    Quaternion rotation;
    // This is the axis that the level will rotate around to go forwards and backwards
    Vector3 forwardRotationAxis;
    // This is the axis that the level will rotate around to go right an left
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Player input
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

    }
    void FixedUpdate() {
        // Find the direction the player is "facing"
        direction = playerTransform.position - cameraTransform.position;
        // Normalize it so the vector length is one
        direction.Normalize();

        // The rotational axis of the for "forward" rotation is the cross product between the vertical vector (0,1,0) and the direction the player is facing
        forwardRotationAxis = Vector3.Cross(direction, Vector3.up);

        // Rotate on the forward direction based on tilt speed and player input
        rotation = Quaternion.AngleAxis(-forwardInput * rotationSpeed, forwardRotationAxis);
        // Rotate on the left/right direction based on tilt speed and player input
        rotation *= Quaternion.AngleAxis(-horizontalInput * rotationSpeed, direction);

        // change the rotation of the level

        //transform.rotation *= rotation;
        rb.MoveRotation(rb.rotation * rotation);

    }
}
