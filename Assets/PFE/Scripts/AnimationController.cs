// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class AnimationController : MonoBehaviour
// {
//     private Animator animator;

//     private string currentAnimaton;
//     const string PLAYER_IDLE = "idle";
//     const string PLAYER_IDLE2 = "idle2";
//     const string PLAYER_RUN = "RUN";
//     const string PLAYER_RUN2 = "RUN2";
//     const string PLAYER_JUMP1 = "JUMP1";
//     private Vector3 lastPosition; // Store the last frame position
//     public float movementThreshold = 1f; // Adjust this value to reduce jitter
//     private int evolution = 1;

//     void Start()
//     {
//         animator = GetComponent<Animator>();
//         lastPosition = transform.position;
//     }

//     // Update is called once per frame
//     void Update()
//     {


//         float movement = Vector3.Distance(transform.position, lastPosition);

//         if (Input.GetKey(KeyCode.A))
//         {

//             transform.eulerAngles = new Vector3(0, 0, 0);
//         }
//         else if (Input.GetKey(KeyCode.D))
//         {
//             transform.eulerAngles = new Vector3(0, 180, 0);
//         }

//         if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
//         {
//             if (evolution == 1)
//             {
//                 ChangeAnimationState(PLAYER_RUN);
//             }
//             if (evolution == 2)
//             {
//                 ChangeAnimationState(PLAYER_RUN2);
//             }

//         }
//         else
//         {
//             if (evolution == 1)
//             {
//                 ChangeAnimationState(PLAYER_IDLE);
//             }
//             if (evolution == 2)
//             {
//                 ChangeAnimationState(PLAYER_IDLE2);
//             }

//         }

//         // Update last position for the next frame
//         lastPosition = transform.position;
//     }

//     void ChangeAnimationState(string newAnimation)
//     {
//         if (currentAnimaton == newAnimation) return;

//         animator.Play(newAnimation);
//         currentAnimaton = newAnimation;
//     }

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Evolution1")) 
//         {
//             evolution = 2 ;
//             Debug.Log("Variable chang�e � 1");
//         }
//     }
// }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;

    private string currentAnimaton;
    const string PLAYER_IDLE = "idle";
    const string PLAYER_IDLE2 = "idle2";
    const string PLAYER_IDLE3 = "idle3";
    const string PLAYER_RUN = "RUN";
    const string PLAYER_RUN2 = "RUN2";
    const string PLAYER_RUN3 = "RUN3";
    const string PLAYER_JUMP1 = "JUMP1";
    const string PLAYER_JUMP2 = "JUMP2";
    const string PLAYER_JUMP3 = "JUMP3";
    const string PLAYER_FALL = "FALL";
    const string PLAYER_GRAP = "GRAP";
    const string PLAYER_GRAP2 = "GRAP2";
    float _xAxis;

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
        _xAxis = Input.GetAxisRaw("Horizontal");

        float movement = Vector3.Distance(transform.position, lastPosition);

        if (Input.GetKey(KeyCode.A))
        {

            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        
        else
        {
            

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
        if (other.CompareTag("Evolution2"))
        {
            evolution = 3;
            Debug.Log("Variable chang�e � 1");
        }
    }

    public void Jump()
    {
        
        if (evolution == 1)
        {
            ChangeAnimationState(PLAYER_JUMP1);
        }
        if (evolution == 2)
        {
            ChangeAnimationState(PLAYER_JUMP2);
        }
        if (evolution == 3)
        {
            ChangeAnimationState(PLAYER_JUMP3);
        }
    }
    public void RUN()
    {
        if (_xAxis != 0)
        {
            if (evolution == 1)
            {
                ChangeAnimationState(PLAYER_RUN);
            }
            if (evolution == 2)
            {
                ChangeAnimationState(PLAYER_RUN2);
            }
            if (evolution == 3)
            {
                ChangeAnimationState(PLAYER_RUN3);
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
            if (evolution == 3)
            {
                ChangeAnimationState(PLAYER_IDLE3);
            }
        }
    }

    public void Idle()
    {
        if (evolution == 1)
        {
            //ChangeAnimationState(PLAYER_IDLE);
        }
        if (evolution == 2)
        {
            //ChangeAnimationState(PLAYER_IDLE2);
        }
    }

    public void Fall()
    {
        //ChangeAnimationState(PLAYER_FALL);
        //animator.Play("FALL");
    }
    public void Grab()
    {
        if (evolution == 2)
        {
            ChangeAnimationState(PLAYER_GRAP);
        }
        if (evolution == 3)
        {
            ChangeAnimationState(PLAYER_GRAP2);
        }
    }
}