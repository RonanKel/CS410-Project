using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] bool isLevelCheckpoint = false;
    [SerializeField] Vector2 xYRotation = new Vector2(15f, 0f);
    [SerializeField] Vector3 worldRotationEuler = new Vector3(0f, 0f, 0f);  // Desired world rotation at this checkpoint

    private Quaternion spawnRotation = new Quaternion(0f, 0f, 0f, 0f);
    private GameBallScript gB;
    private CameraFollowGB cam;
    private World world;

    void Awake()
    {
        Vector3 eulerAngles = transform.localRotation.eulerAngles;
        spawnRotation = Quaternion.Euler(-eulerAngles.x, 0f, -eulerAngles.z);
    }

    void Start()
    {
        gB = GameObject.Find("GameBall").GetComponent<GameBallScript>();
        cam = GameObject.Find("Main Camera").GetComponent<CameraFollowGB>();
        world = GameObject.Find("World").GetComponent<World>();
    }

    void OnTriggerEnter(Collider col) {
        if (col.transform.CompareTag("Player")) {
            if (isLevelCheckpoint) {
                gB.SetLevelCheckpoint(this);
            }
            gB.SetCurrentCheckpoint(this);
        }
    }

    public Vector2 GetCameraXYRotation() {
        return xYRotation;
    }

    public Vector3 GetSpawnPosition() {
        return transform.GetChild(0).position;
    }

    public Quaternion GetSpawnRotation() {
        return spawnRotation;
    }
}