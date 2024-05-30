using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField] float jumpAmount = 50000f;
   [SerializeField] ParticleSystem feedbackParticle;
   [SerializeField] AudioSource feedbackAudio;

    private void OnTriggerEnter(Collider other)
    {
        // if other collider is the player then do something
        if(other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Ball hit bumper");
            // get the players rigidbody
            Rigidbody playerRB = other.gameObject.GetComponent<Rigidbody>();
             //if we found a rigid body, apply the force
             if(playerRB != null)
             {
                //Debug.Log("Player RB found");
                //apply vetical velocity force
                playerRB.AddForce(Vector3.up * jumpAmount);
                //playerRB.velocity = new Vector3(0,jumpAmount,0);

                // visual feedback
                feedbackParticle.Play();
                feedbackAudio.Play();
             }

        }
    }
}
