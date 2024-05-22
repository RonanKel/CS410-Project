using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thud : MonoBehaviour
{
    public AudioSource sound1;
    public AudioSource sound2;

    void OnCollisionEnter(Collision hit)
    {
        if(hit.relativeVelocity.magnitude >= 1)
        {
            float intensityRatio = hit.relativeVelocity.magnitude / 100f;
            Debug.Log(hit.relativeVelocity.magnitude);
            sound1.volume = Mathf.Lerp(0f, .4f, intensityRatio);
            sound2.volume = Mathf.Lerp(0f, .2f, intensityRatio); 
            sound1.pitch = Mathf.Lerp(1.8f, 1f, intensityRatio);
            sound2.pitch = Mathf.Lerp(2.5f, 2f, intensityRatio);
            sound1.Play();
            sound2.Play();
        }
    } 

}
