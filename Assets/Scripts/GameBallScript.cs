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

    void Awake()
    {
        world = GameObject.Find("World").GetComponent<World>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Finds the rigidbody component 
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Checks to see if the ball has fallen too far and will trigger the respawn
        if (transform.position.y <= world.gameObject.transform.position.y + gameOverY) {
            Respawn();
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

}
