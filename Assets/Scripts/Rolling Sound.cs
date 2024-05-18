using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingSound : MonoBehaviour
{
   public Rigidbody rb;
   public AudioSource audio;

   void OnCollisionEnter (Collision ground)
   {
    if(rb.velocity.magnitude > 0.05)
    {
        if (!audio.isPlaying)
        {
            audio.Play();
        }
    }
    else
    {
        audio.Pause();
    }
    
   }

   void OnCollisionStay (Collision ground)
   {
    if(rb.velocity.magnitude > 0.1)
    {
        if (!audio.isPlaying)
        {
            audio.Play();
        }
   }
     else
        {
            audio.Pause();
        }
    }
    
}
