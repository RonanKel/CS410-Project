using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBallScript : MonoBehaviour
{
    // How far the ball must fall to "lose"
    [SerializeField] float gameOverY = -100f;
    // The world (This should be the script attached to the parent for the world
    [SerializeField] World world;
    // The place the ball will respawn, use a SpawnPoint prefab.
    [SerializeField] Checkpoint levelCheckpoint;
    [SerializeField] Checkpoint currentCheckpoint;
    // The balls rigidbody
    private Rigidbody rb;

    private CameraFollowGB cam;

    [SerializeField] CameraFollowGB rotationAssistantCamera;

    [SerializeField] float hitPauseDuration = .1f;

    [SerializeField] AnimationCurve stopLengthCurve;


    // Particle system stuff
    [SerializeField] ParticleSystem ps;

    void Awake()
    {
        world = GameObject.Find("World").GetComponent<World>();
        cam = GameObject.Find("Main Camera").GetComponent<CameraFollowGB>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(transform.rotation);
        // Finds the rigidbody component 
        rb = GetComponent<Rigidbody>();
        if (currentCheckpoint) {
            //Debug.Log("THis");
            Respawn();
            rb.AddForce(Vector3.down * 1000f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Checks to see if the ball has fallen too far and will trigger the respawn
        if (transform.position.y <= world.gameObject.transform.position.y + gameOverY) {
            Respawn();
        }

    }

    void FixedUpdate() 
    {
        // This is to accounts for little movements that wouldn't usually alert or "wake up" the rigidbody to move
        if (rb.IsSleeping()) {
            rb.WakeUp();
        }
    }

    public void RestartLevel() {
        world.SetWorldRotation(levelCheckpoint.GetSpawnRotation());
        transform.position = levelCheckpoint.GetSpawnPosition();
        rb.velocity = Vector3.zero;
        currentCheckpoint = levelCheckpoint;
    }

    public void Respawn() {
        // Locks the position so it will fall straight down onto the platform
        LockHorizontalPosition();

        // Sets the rotation of the world so the ball can nicely fall on the platform
        world.SetWorldRotation(currentCheckpoint.GetSpawnRotation());

        // Sets the position of the ball to its respawn point
        transform.position = currentCheckpoint.GetSpawnPosition();

        // Turns the camera in the direction the player should go
        cam.SetCameraOrientation(currentCheckpoint.GetCameraXYRotation());

        // Corrects Rotation Assistant Camera
        if (rotationAssistantCamera) {
            rotationAssistantCamera.SetCameraOrientation(currentCheckpoint.GetCameraXYRotation().y);
        }

        // Unlocks the position after a delay
        Invoke("UnlockHorizontalPosition", .7f);
    }


    public void LockHorizontalPosition() {
        // THis will enable the X and Z axis position restraint placed on the ball
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
    }

    public void UnlockHorizontalPosition() {
        // This will remove the X and Z axis position restraint placed on the ball
        rb.constraints = RigidbodyConstraints.None;
    }

    public void SetCurrentCheckpoint(Checkpoint spawn) {
        // Changes the ball's last checkpoint
        currentCheckpoint = spawn;
    }

    public void SetLevelCheckpoint(Checkpoint spawn) {
        // Changes the ball's level checkpoint (level switching)
        levelCheckpoint = spawn;
    }

    private void SetParticleSpawner(Collision col, float collisionSpeed, ParticleSystem particleSystem, float intensityScalar) {
        // The point of contact 
        Vector3 contactPoint = col.GetContact(col.contactCount-1).point;
        // The position of the gameball
        Vector3 currPosition = transform.position;
        // The direction the particles should be facing
        Vector3 direction = Vector3.Normalize(currPosition - contactPoint);

        // Make the particle system look in the right direction
        particleSystem.transform.LookAt(particleSystem.transform.position + direction);
        // Change the position to the right place
        particleSystem.transform.position = Vector3.Lerp(contactPoint, contactPoint + direction, .15f);

        float intensityRatio = (collisionSpeed * intensityScalar) / 100f;

        // Set the speed and size of the particles
        var mainModule = particleSystem.main;
        //Debug.Log(intensityRatio);
        mainModule.startSpeed = new ParticleSystem.MinMaxCurve(Mathf.Lerp(0f, 12f, intensityRatio), Mathf.Lerp(0f, 30f, intensityRatio));
        mainModule.startSize = new ParticleSystem.MinMaxCurve(Mathf.Lerp(.05f, 1.5f, intensityRatio), Mathf.Lerp(.1f, .175f, intensityRatio));

        // Set the amount of particles to be made
        var emissionModule = particleSystem.emission;
        ParticleSystem.Burst burst = emissionModule.GetBurst(0);
        burst.count = (int) Mathf.Lerp(1f, 100f, intensityRatio);
        emissionModule.SetBurst(0, burst);

        // Play the effect!
        particleSystem.Play();
        
    }

    void OnCollisionEnter(Collision col) {

        if (!col.gameObject.CompareTag("Player") && !col.gameObject.CompareTag("Checkpoint")) {
            if (ps) {
                float collisionSpeed = col.relativeVelocity.magnitude;
                if (collisionSpeed > 1f) {
                    SetParticleSpawner(col, collisionSpeed, ps, 1f);
                }
                // Debug.Log(collisionSpeed);
                
                if (collisionSpeed > 10f) {
                    
                    cam.StartShaking(collisionSpeed);
                    StartCoroutine(HitPause(collisionSpeed));
                }
            }
        }
    }

    void OnCollisionStay(Collision col) {
        if (!col.gameObject.CompareTag("Player") && !col.gameObject.CompareTag("Checkpoint") && ps) {
            float collisionSpeed = col.relativeVelocity.magnitude;
            //float collisionSpeed = rb.velocity.magnitude;
            if (collisionSpeed > .001f) {
                SetParticleSpawner(col, collisionSpeed, ps, .25f);
            }
        }
    }

    IEnumerator HitPause(float collisionSpeed) {
        Time.timeScale = 0f;

        hitPauseDuration = stopLengthCurve.Evaluate(collisionSpeed / 100f);
        yield return new WaitForSecondsRealtime(hitPauseDuration);
        Time.timeScale = 1f;
    }

}
