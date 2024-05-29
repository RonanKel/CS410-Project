using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingSound : MonoBehaviour
{
   [SerializeField] Rigidbody rb;
   [SerializeField] AudioSource audio;

   

   void Update () 
   {
        

   }

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

    void OnCollisionExit (Collision ground) {
        audio.Stop();
    }

    
    
}
