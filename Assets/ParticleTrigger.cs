using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTrigger : MonoBehaviour
{
    public ParticleSystem particleSystem;
    private bool hasTriggered = true;  // Starts as true, can be changed once the particles finish.

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && hasTriggered)
        {
            // Lancez le système de particules
            if (particleSystem != null)
            {
                particleSystem.Play();  // Lance les particules
                hasTriggered = false;   // Set to false after triggering the particles
            }
        }
    }
}