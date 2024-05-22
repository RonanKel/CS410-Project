using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialParticleScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity;
        transform.position = transform.parent.position - new Vector3(0f, .45f, 0f);
    }
}
