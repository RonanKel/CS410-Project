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

    [SerializeField] ParticleSystem particleSystem;

    [SerializeField] float stuff = .2f;

    void Awake()
    {
        world = GameObject.Find("World").GetComponent<World>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Finds the rigidbody component 
        rb = GetComponent<Rigidbody>();
        if (currentCheckpoint) {
            Debug.Log("THis");
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

    public void OnCollisionEnter(Collision col) {

        if (!col.gameObject.CompareTag("Player") && !col.gameObject.CompareTag("Checkpoint") && particleSystem) {

            //Debug.Log("hit");
            Vector3 contactPoint = col.GetContact(col.contactCount-1).point;
            Vector3 currPosition = particleSystem.transform.position;

            
            if (col.relativeVelocity.magnitude > 1f) {
                Vector3 direction = Vector3.Normalize(currPosition - contactPoint);
                //Debug.Log(direction);
                particleSystem.transform.LookAt(particleSystem.transform.position + direction);
                particleSystem.transform.position = Vector3.Lerp(contactPoint, contactPoint + direction, stuff);
                particleSystem.Play();

                // Set the speed and size of the particles
                var mainModule = particleSystem.main;
                mainModule.startSpeed = new ParticleSystem.MinMaxCurve(Mathf.Lerp(2f, 7f, col.relativeVelocity.magnitude / 100f), Mathf.Lerp(5f, 20f, col.relativeVelocity.magnitude / 100f));
                mainModule.startSize = new ParticleSystem.MinMaxCurve(Mathf.Lerp(.05f, 1f, col.relativeVelocity.magnitude / 100f), Mathf.Lerp(.1f, 2f, col.relativeVelocity.magnitude / 100f));

                // Set the amount to be made
                var emissionModule = particleSystem.emission;
                ParticleSystem.Burst burst = emissionModule.GetBurst(0);
                burst.count = (int) Mathf.Lerp(20f, 100f, col.relativeVelocity.magnitude / 100f);
                emissionModule.SetBurst(0, burst);

                // Set the size of the particles


                

                


               

                //particleSystem.main.startSpeed = Mathf.Lerp(2f, 10f, col.relativeVelocity.magnitude / 100f);
            }
        }
    }

}
