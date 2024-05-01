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
    [SerializeField] SpawnPoint spawnPoint;
    // The balls rigidbody
    private Rigidbody rb;

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
        if (transform.position.y <= gameOverY) {
            Respawn();
        }
    }

    public void Respawn() {
        // Sets the position of the ball to its respawn point
        transform.position = spawnPoint.spawnPosition;
        // Sets the rotation of the world so the ball can nicely fall on the platform
        world.SetWorldRotation(spawnPoint.spawnRotation);
        // Locks the position so it will fall straight down onto the platform
        LockHorizontalPosition();
        // Unlocks the position after a delay
        Invoke("UnlockHorizontalPosition", .2f);
    }

    public void LockHorizontalPosition() {
        // THis will enable the X and Z axis position restraint placed on the ball
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
    }

    public void UnlockHorizontalPosition() {
        // This will remove the X and Z axis position restraint placed on the ball
        rb.constraints = RigidbodyConstraints.None;
    }

    public void SetSpawnPoint(SpawnPoint spawn) {
        // Changes the balls spawnpoint
        spawnPoint = spawn;
    }
}
