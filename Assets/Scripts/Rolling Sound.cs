using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingSound : MonoBehaviour
{
   [SerializeField] Rigidbody rb;
   [SerializeField] AudioSource sound;

   

   void Update () 
   {
        

   }

   void OnCollisionEnter (Collision ground)
   {
    if(rb.velocity.magnitude > 0.05)
    {
        if (!sound.isPlaying)
        {
            sound.Play();
        }
    }
    else
    {
        sound.Pause();
    }
    
   }

   void OnCollisionStay (Collision ground)
   {
    if(rb.velocity.magnitude > 0.1)
    {
        if (!sound.isPlaying)
        {
            sound.Play();
        }
   }
     else
        {
            sound.Pause();
        }
    }

    void OnCollisionExit (Collision ground) {
        sound.Stop();
    }

    
    
}
