using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldRotationScript : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform playerTransform;
    [SerializeField] float rotationSpeed;
    private float forwardInput;
    private float rightInput;

    Rigidbody rb;

    Quaternion rotation;

    private Vector3 inputVector = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Player input
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.z = Input.GetAxis("Vertical");

        // Find the direction the player is "facing"
        Vector3 direction = playerTransform.position - cameraTransform.position;
        // Normalize it so the vector length is one
        direction.Normalize();

        // The rotational axis of the for "forward" rotation is the cross product between the vertical vector (0,1,0) and the direction the player is facing
        Vector3 forwardRotationAxis = Vector3.Cross(direction, Vector3.up);
        //float rotationAmount = Mathf.Atan2(inputVector.x, inputVector.z);

        // Rotate on the forward direction based on tilt speed and player input
        rotation = Quaternion.AngleAxis(-inputVector.z * Time.deltaTime * rotationSpeed, forwardRotationAxis);
        // Rotate on the left/right direction based on tilt speed and player input
        rotation *= Quaternion.AngleAxis(-inputVector.x * Time.deltaTime * rotationSpeed, direction);

        // change the rotation of the
        //transform.rotation *= rotation;

        
        rb.MoveRotation(rb.rotation * rotation);


        Debug.Log(rotation);

        //Quaternion rotationQuaternion = new Quaternion(rotationVector.x * Time.deltaTime * rotationSpeed, 0f, -rotationVector.z * Time.deltaTime * rotationSpeed, 0f);        
    }
}
