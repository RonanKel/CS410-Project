using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] bool isLevelCheckpoint = false;

    private Quaternion spawnRotation = new Quaternion(0f, 0f, 0f, 0f);

    // Start is called before the first frame update
    void Awake()
    {
        Vector3 eulerAngles = transform.localRotation.eulerAngles;
        spawnRotation = Quaternion.Euler(-eulerAngles.x, 0f, -eulerAngles.z);
    }

    void OnTriggerEnter(Collider col) {
        if (col.transform.CompareTag("Player")) {
            if (isLevelCheckpoint) {
                col.transform.GetComponent<GameBallScript>().SetLevelCheckpoint(this);
            }

            col.transform.GetComponent<GameBallScript>().SetCurrentCheckpoint(this);
        }
    }

    public Vector3 GetSpawnPosition() {
        return transform.GetChild(0).position;
    }
    
    public Quaternion GetSpawnRotation() {
        return spawnRotation;
    }
}
