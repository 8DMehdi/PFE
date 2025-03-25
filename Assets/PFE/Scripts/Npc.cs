using UnityEngine;

public class NPC : Interactable
{
    public bool isLastNPC;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.Play("Idle"); 
        }
    }
}
