using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thud : MonoBehaviour
{
    public AudioClip impact;

    void OnCollisionEnter (Collision hit)
    {
        if(hit.relativeVelocity.magnitude >=5)
        {
            GetComponent<AudioSource>().PlayOneShot(impact);
        }
    } 

}
