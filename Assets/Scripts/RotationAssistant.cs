using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAssistant : MonoBehaviour
{

    private Transform world;
    public bool activity = true;

    // Start is called before the first frame update
    void Start()
    {
        world = GameObject.Find("World").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = world.rotation;
    }
}
