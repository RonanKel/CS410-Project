using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform playerTransform;
    // The speed the player would like to rotate the playtform
    [SerializeField] float rotationSpeed = 1f;

    // These keep track of the players input
    private float forwardInput;
    private float horizontalInput;

    // This is the rotation variable the ends up rotation the level
    Quaternion rotation;
    // This is the axis that the level will rotate around to go forwards and backwards
    Vector3 forwardRotationAxis;
    // This is the axis that the level will rotate around to go right an left
    Vector3 direction;

    void Awake() 
    {
        cameraTransform = GameObject.Find("Main Camera").transform;
        playerTransform = GameObject.Find("GameBall").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Lock the cursor when the game is running
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

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

        // Rotate on the forward direction based on tilt speed and player input around the player
        transform.RotateAround(playerTransform.position, forwardRotationAxis, -forwardInput * rotationSpeed);
        // Rotate on the left/right direction based on tilt speed and player input around the player
        transform.RotateAround(playerTransform.position, direction, -horizontalInput * rotationSpeed);
    }

    public void SetWorldRotation(Quaternion rotation) {
        /*
        This will set the rotation of the world

        Input:
        Quaternion rotation: the rotation the world should be set to

        Returns:
        None.
        */
        transform.rotation = rotation;
    }
}
