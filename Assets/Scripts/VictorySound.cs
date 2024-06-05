using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictorySound : MonoBehaviour
{
    [SerializeField] ParticleSystem feedbackParticle;
    [SerializeField] AudioSource feedbackAudio;
    bool played;
   

    void Start ()
    {
        played = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(feedbackParticle != null)
        {
            feedbackParticle.Play();
        }
        if(feedbackAudio !=null && !played)
        {
        feedbackAudio.Play();
        played = true;
        }
    }
}