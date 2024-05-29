using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameballSound : MonoBehaviour
{
    public AudioSource collisionSound1;
    public AudioSource collisionSound2;

    public AudioSource rollingSound;
    [SerializeField] float radius = .5f;
    private Rigidbody rb;
    private bool isPlaying = false;
    public float maxSpeed = 10.7f;
    public AnimationCurve volumeCurve;
    public AnimationCurve pitchCurve;

    int layerMask = 1 << 3;

    bool isRolling;
    float speed;
    

    void Start() 
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        isRolling = (rb.velocity.magnitude > 0.05f);

        if (CheckIfRolling()) {
            if (!isPlaying) {
                rollingSound.Play();
                Debug.Log("play");
                isPlaying = true;
            }
        }
        else {
            rollingSound.Pause();
            isPlaying = false;
        }

        speed = rb.velocity.magnitude;

        // normalize speed into 0-1
        var scaledVelocity = Remap(Mathf.Clamp(speed, 0, maxSpeed), 0, maxSpeed, 0, 1);

        // set volume based on volume curve
        GetComponent<AudioSource>().volume = volumeCurve.Evaluate(scaledVelocity);
 
        // set pitch based on pitch curve
        GetComponent<AudioSource>().pitch = pitchCurve.Evaluate(scaledVelocity);
    }

    void OnCollisionEnter(Collision hit)
    {
        if(hit.relativeVelocity.magnitude >= 1)
        {
            float intensityRatio = hit.relativeVelocity.magnitude / 100f;
            //Debug.Log(hit.relativeVelocity.magnitude);
            collisionSound1.volume = Mathf.Lerp(0f, .4f, intensityRatio);
            collisionSound2.volume = Mathf.Lerp(0f, .2f, intensityRatio);
            collisionSound1.pitch = Mathf.Lerp(1.8f, 1f, intensityRatio);
            collisionSound2.pitch = Mathf.Lerp(2.5f, 2f, intensityRatio);
            collisionSound1.Play();
            collisionSound2.Play();
        }
    } 

    bool CheckIfRolling () {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, layerMask);

        if (colliders.Length > 0) {
            return true;
        }
        return false;
    }

    // https://forum.unity.com/threads/re-map-a-number-from-one-range-to-another.119437/
    public float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

}
