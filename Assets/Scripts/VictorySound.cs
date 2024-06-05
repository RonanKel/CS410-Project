using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictorySound : MonoBehaviour
{
    [SerializeField] ParticleSystem feedbackParticle;
    [SerializeField] AudioSource feedbackAudio;
    [SerializeField] bool played = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!played) {
            if (feedbackParticle != null)
            {
                feedbackParticle.Play();
            }
            if (feedbackAudio != null)
            {
                feedbackAudio.Play();
            }
            played = true;
        }
    }
}