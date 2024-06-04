using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    [SerializeField] float bounceAmount = 300f;
    [SerializeField] ParticleSystem feedbackParticle;
    [SerializeField] AudioSource feedbackAudio;
    [SerializeField] float rotationSpeed = 0f; // Rotation speed in degrees per second

    private void Update()
    {
        // Continuously rotate the bumper based on rotationSpeed
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // if other collider is the player then do something
        if(other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Ball hit bumper");
            // get the players rigidbody
            Rigidbody playerRB = other.gameObject.GetComponent<Rigidbody>();
             //if we gound a rigid body, apply the force
             if(playerRB != null)
             {
                //Debug.Log("Player RB found");
                //get the player's velocity and invert it
                Vector3 bounceDirection = -playerRB.velocity;
                //Debug.Log("Bounce Direction: " + bounceDirection * bounceAmount);
                //apply this force
                playerRB.AddForce(bounceDirection * bounceAmount);

                // visual feedback
                feedbackParticle.Play();
                feedbackAudio.Play();
             }

        }
    }
}
