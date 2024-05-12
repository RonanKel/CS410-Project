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
    void Start()
    {
        dropMaterial.color = startingColor;
        camera = GameObject.Find("Main Camera").GetComponent<CameraFollowGB>();
        world = GameObject.Find("World").GetComponent<World>();
    }

    // Update is called once per frame
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

    void Drop()
    {
        EndTimer();

        //gameObject.SetActive(false);
        camera.SetFollowingStatus(false);
        world.SetRotationStatus(false);
        ball.SetParent(transform);
        RotatePlatform();
    }

    void EndTimer()
    {
        isTicking = false;
        dropMaterial.color = Color.Lerp(startingColor, selectedColor, timer / 3f);
        timer = 0f;
    }

    void StartTimer()
    {
        isTicking = true;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            ball = col.transform;
            StartTimer();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            EndTimer();
        }
    }

    void RotatePlatform()
    {
        if (!isRotating) // Check if rotation is not already in progress
        {
            StartCoroutine(RotateCoroutine());
        }
    }

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