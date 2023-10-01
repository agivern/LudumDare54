using System;
using UnityEngine;
using System.Collections.Generic;

public class AlienAnimator : MonoBehaviour
{
    public Rigidbody2D rb;
    public AnimatorOverrideController animatorOverrideController;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        var rtAnimator = GetComponent<RuntimeAnimatorController>();

        animator = GetComponent<Animator>();

        animator.runtimeAnimatorController = animatorOverrideController;

    }
    
    private void Update()
    {
    }

    private void FixedUpdate()
    {
        FlipCharacter();
        UpdateAnimatorVars();
    }

    private void FlipCharacter() {
        Vector3 velocity = rb.velocity;

        if (velocity.x < -0.1)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (velocity.x > 0.1)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void UpdateAnimatorVars() {
        var vx = rb.velocity.x;
        animator.SetFloat("vx", vx);
    }
}