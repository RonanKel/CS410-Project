using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    /*
    This is the class to keep track of spawn points.
    It is attached to the "SpawnPoint" prefab which should be used.
    */
    public Vector3 spawnPosition = Vector3.zero;
    public Quaternion spawnRotation = new Quaternion(0f, 0f, 0f, 0f);


    void Awake()
    {
        transform.position = spawnPosition;
    }
}
