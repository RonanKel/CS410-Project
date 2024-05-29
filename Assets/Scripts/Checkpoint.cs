using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] bool isLevelCheckpoint = false;

    private Quaternion spawnRotation = new Quaternion(0f, 0f, 0f, 0f);

    private GameBallScript gB;

    // Start is called before the first frame update
    // Sets the spawn rotation to the opposite of the current rotation
    void Awake()
    {
        Vector3 eulerAngles = transform.localRotation.eulerAngles;
        spawnRotation = Quaternion.Euler(-eulerAngles.x, 0f, -eulerAngles.z);
    }

    void Start()
    {
        gB = GameObject.Find("GameBall").GetComponent<GameBallScript>();
    }
    // When the player enters the checkpoint, the player's current checkpoint is set to this checkpoint
    void OnTriggerEnter(Collider col) {
        if (col.transform.CompareTag("Player")) {
            if (isLevelCheckpoint) {
                gB.SetLevelCheckpoint(this);
                //col.transform.GetComponent<GameBallScript>().SetLevelCheckpoint(this);
            }
            gB.SetCurrentCheckpoint(this);
            //col.transform.GetComponent<GameBallScript>().SetCurrentCheckpoint(this);
        }
    }

    // Returns the position of the spawn point
    public Vector3 GetSpawnPosition() {
        return transform.GetChild(0).position;
    }
    // Returns the rotation of the spawn point
    public Quaternion GetSpawnRotation() {
        return spawnRotation;
    }
}
