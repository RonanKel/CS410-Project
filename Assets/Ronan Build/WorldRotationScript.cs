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

    // Layermask and hit variable for player ground touch recast
    int layerMask = 1 << 3;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //playerTransform.position = new Vector3(0f, 4f, -5f);
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


        // Checks to see if the player is touching the floor, if they are then the world rotates around them, if not it has standard rotation
       if (Physics.Raycast(playerTransform.position, Vector3.down, out hit, .6f, ~layerMask)) {
            // Rotate on the forward direction based on tilt speed and player input around the player
            transform.RotateAround(playerTransform.position, forwardRotationAxis, -forwardInput * rotationSpeed);
            // Rotate on the left/right direction based on tilt speed and player input around the player
            transform.RotateAround(playerTransform.position, direction, -horizontalInput * rotationSpeed);

        } else {
            // Rotate on the forward direction based on tilt speed and player input
            rotation = Quaternion.AngleAxis(-forwardInput * rotationSpeed, forwardRotationAxis);
            // Rotate on the left/right direction based on tilt speed and player input
            rotation *= Quaternion.AngleAxis(-horizontalInput * rotationSpeed, direction);
            // change the rotation of the level

            rb.MoveRotation(rb.rotation * rotation);
            Debug.Log("Regular rotation");
        }
    }
}
