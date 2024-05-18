using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
 
public class RollingSoundOld : MonoBehaviour
{
    public Rigidbody rb;
    public AudioSource audio;
    public AudioClip thud;
    public AudioClip rolling;
 
    public float maxSpeed = 10.7f;
    public AnimationCurve volumeCurve;
    public AnimationCurve pitchCurve;
    public bool grounded;
    public bool onStay;
 
   
 
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis ("Horizontal");
        bool isRolling = (rb.velocity.magnitude> 0.05) ;
      

        if(isRolling && grounded){
            Debug.Log("is Rolling and Grounded");
            Debug.Log(rb.velocity.magnitude);
            if(!audio.isPlaying)
            {
                audio.Play();
                Debug.Log("Played");
            }
        }

        else
        {
            audio.Pause();
            Debug.Log("Paused");
        }

        var speed = rb.velocity.magnitude;
        Debug.Log("speed= " + speed);
 
        // normalize speed into 0-1
        var scaledVelocity = Remap(Mathf.Clamp(speed, 0, maxSpeed), 0, maxSpeed, 0, 1);
 
        // set volume based on volume curve
        audio.volume = volumeCurve.Evaluate(scaledVelocity);
 
        // set pitch based on pitch curve
        audio.pitch = pitchCurve.Evaluate(scaledVelocity);
        
        
    }

       void OnCollisionStay(Collision theCollision)
    {
        // if(theCollision.relativeVelocity.magnitude >= 5)
        // {
        //     audio.PlayOneShot(thud);
        // }
        //   if(theCollision.gameObject.CompareTag("Tiltable"))
        // {
            grounded = true;
        // }
    }

    void OnCollisionEnter(Collision theCollision)
    {
        if(theCollision.gameObject.CompareTag("Tiltable"))
        {
           grounded = true;
          // Debug.Log("grounded" + grounded);
        }
        //   if(theCollision.relativeVelocity.magnitude >= 5)
        // {
        //     audio.PlayOneShot(thud);
        // }
        
      
    }

    void OnCollisionExit(Collision theCollision)
    {
        if(theCollision.gameObject.CompareTag("Tiltable"))
        {
            grounded = false;
           // Debug.Log("Exit Grounded" + grounded);
        }
    }

 
 
 
    // https://forum.unity.com/threads/re-map-a-number-from-one-range-to-another.119437/
    public float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
 
}
 