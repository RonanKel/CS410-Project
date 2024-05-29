using UnityEngine;
using System.Collections;

public class CameraFollowGB : MonoBehaviour
{
    public Transform target; // Target to follow (game ball)
    public float distance = 10.0f; // Fixed distance from the target
    public float sensitivityX = 3.0f; // Sensitivity of mouse movement in X direction
    public float sensitivityY = 3.0f; // Sensitivity of mouse movement in Y direction
    public float zoomSensitivity = 2.0f; // Sensitivity of mouse wheel zoom
    public float minDistance = 5.0f; // Minimum distance of the camera from the target
    public float maxDistance = 15.0f; // Maximum distance of the camera from the target

    private float currentY = 0.0f; // Current angle in Y-axis
    private float currentX = 0.0f; // Current angle in X-axis

    [SerializeField] float maxShakeRotation = 1f;
    [SerializeField] float maxShakeIntensity = 1f;

    public AnimationCurve durationCurve;

    Vector3 shakeVector;

    private float xRot = 0f;
    private float yRot = 0f;

    private bool isFollowing = true;

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

        if (isFollowing) {
            // Update angles based on input
            currentX += Input.GetAxis("Mouse X") * sensitivityX;
            currentY -= Input.GetAxis("Mouse Y") * sensitivityY;
            // Clamp the vertical angle to prevent flipping
            currentY = Mathf.Clamp(currentY, -85, 85);

            // Update the distance based on mouse wheel scroll
            distance -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
            // Clamp the distance to stay within min and max limits
            distance = Mathf.Clamp(distance, minDistance, maxDistance);
        }
    }

    void LateUpdate()
    {
        if (target == null || !isFollowing)
        {
            return;
        }

        // Calculate rotation and position based on current angles and the fixed distance
        Quaternion rotation = Quaternion.Euler(currentY + xRot, currentX + yRot, 0);
        Vector3 position = target.position - (rotation * Vector3.forward * distance) + shakeVector;

        // Update camera position and rotation
        transform.position = position;
        transform.LookAt(target);
    }

    public void SetFollowingStatus(bool status) {
        isFollowing = status;
    }

    public void StartShaking(float intensity) {
        StartCoroutine(Shake(intensity));
    }

    IEnumerator Shake(float intensity) {
        float intensityRatio = intensity / 100f;
        float shakeIntensity = Mathf.Lerp(0f, maxShakeIntensity, intensityRatio);
        float shakeLength = durationCurve.Evaluate(intensityRatio);

        float elapsed = 0f;

        while (elapsed < shakeLength) {
            shakeVector = new Vector3(Random.Range(-shakeIntensity, shakeIntensity), Random.Range(-shakeIntensity, shakeIntensity), Random.Range(-shakeIntensity, shakeIntensity));
            xRot += Random.Range(-maxShakeRotation, maxShakeRotation);
            yRot += Random.Range(-maxShakeRotation, maxShakeRotation);
            elapsed += Time.deltaTime;
            yield return null;
        }

        shakeVector = Vector3.Lerp(Vector3.zero, shakeVector, .7f);
        yield return null;
        shakeVector = Vector3.Lerp(Vector3.zero, shakeVector, .7f);
        yield return null;
        shakeVector = Vector3.Lerp(Vector3.zero, shakeVector, .5f);
        yield return null;
        shakeVector = Vector3.Lerp(Vector3.zero, shakeVector, .5f);
        yield return null;
        shakeVector = Vector3.zero;
        xRot = 0f;
        yRot = 0f;
    }
}
