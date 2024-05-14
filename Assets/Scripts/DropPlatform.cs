using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPlatform : MonoBehaviour
{
    bool isTicking = false;
    float timer = 0f;
    [SerializeField] Color startingColor;
    [SerializeField] Color selectedColor;
    [SerializeField] Material dropMaterial;

    CameraFollowGB camera;
    World world;

    Transform ball;

    // Rotation settings
    public float rotationDuration = 0.5f;
    private bool isRotating = false;

    // Start is called before the first frame update
    // Find the camera and world objects
    void Start()
    {
        dropMaterial.color = startingColor;
        camera = GameObject.Find("Main Camera").GetComponent<CameraFollowGB>();
        world = GameObject.Find("World").GetComponent<World>();
    }

    // Update is called once per frame
    // If the timer is ticking, increase the timer and change the color of the platform
    void Update()
    {
        if (isTicking)
        {
            timer += Time.deltaTime;
            dropMaterial.color = Color.Lerp(startingColor, selectedColor, timer / 3f);
        }
        if (timer > 3)
        {
            Drop();
        }

        // Rotate the platform after the timer ends
        if (isRotating)
        {
            transform.Rotate(Vector3.right, 180f * Time.deltaTime / rotationDuration);
        }
    }

    // Drop the platform
    void Drop()
    {
        EndTimer();

        //gameObject.SetActive(false);
        camera.SetFollowingStatus(false);
        world.SetRotationStatus(false);
        ball.SetParent(transform);
        RotatePlatform();
    }
    // End the timer
    void EndTimer()
    {
        isTicking = false;
        dropMaterial.color = Color.Lerp(startingColor, selectedColor, timer / 3f);
        timer = 0f;
        dropMaterial.color = Color.Lerp(startingColor, selectedColor, timer / 3f);

    }
    // Start the timer
    void StartTimer()
    {
        isTicking = true;
    }
    // If the player enters the trigger, set the ball to the player and start the timer
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            ball = col.transform;
            StartTimer();
        }
    }
    // If the player exits the trigger, end the timer
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            EndTimer();
        }
    }
    // Rotate the platform
    void RotatePlatform()
    {
        if (!isRotating) // Check if rotation is not already in progress
        {
            StartCoroutine(RotateCoroutine());
        }
    }
    // Coroutine to rotate the platform
    IEnumerator RotateCoroutine()
    {
        isRotating = true;

        float elapsedTime = 0;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(180, 0, 0); // Rotate 180 degrees around the x-axis

        while (elapsedTime < rotationDuration)
        {
            transform.localRotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = targetRotation;
        isRotating = false;
    }
}