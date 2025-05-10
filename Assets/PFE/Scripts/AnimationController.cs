using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;

    private string currentAnimaton;
    const string PLAYER_IDLE = "idle";
    const string PLAYER_IDLE2 = "idle2";
    const string PLAYER_RUN = "RUN";
    const string PLAYER_RUN2 = "RUN2";
    const string PLAYER_JUMP1 = "JUMP1";
    private Vector3 lastPosition; // Store the last frame position
    public float movementThreshold = 1f; // Adjust this value to reduce jitter
    private int evolution = 1;

    void Start()
    {
        animator = GetComponent<Animator>();
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {


        float movement = Vector3.Distance(transform.position, lastPosition);

        if (Input.GetKey(KeyCode.A))
        {

            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (evolution == 1)
            {
                ChangeAnimationState(PLAYER_RUN);
            }
            if (evolution == 2)
            {
                ChangeAnimationState(PLAYER_RUN2);
            }

        }
        else
        {
            if (evolution == 1)
            {
                ChangeAnimationState(PLAYER_IDLE);
            }
            if (evolution == 2)
            {
                ChangeAnimationState(PLAYER_IDLE2);
            }

        }

        // Update last position for the next frame
        lastPosition = transform.position;
    }

    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimaton == newAnimation) return;

        animator.Play(newAnimation);
        currentAnimaton = newAnimation;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Evolution1")) 
        {
            evolution = 2 ;
            Debug.Log("Variable chang�e � 1");
        }
    }
}
