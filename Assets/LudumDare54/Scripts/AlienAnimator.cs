using System;
using UnityEngine;
using System.Collections.Generic;

public class AlienAnimator : MonoBehaviour
{
  public AnimatorOverrideController animatorOverrideController;

  private Rigidbody2D rb;

  // private Alien alien;
  private AlienMovement alienMov;
  private AlienDragDrop alienDragDrop;
  private Animator animator;


  private void Awake()
  {
    rb = GetComponentInParent<Rigidbody2D>();

    // alien = GetComponentInParent<Alien>();

    alienMov = GetComponentInParent<AlienMovement>();

    alienDragDrop = GetComponentInParent<AlienDragDrop>();

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

  private void FlipCharacter()
  {
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

  private void UpdateAnimatorVars()
  {
    animator.SetFloat("vx", rb.velocity.x);
    animator.SetBool("floating", alienMov.isInSpace || alienDragDrop.isDragging);
  }

  public void SetEmotion(float statisfaction)
  {
    if (statisfaction > 0)
    {
      animator.SetBool("in_love", true);
      animator.SetBool("in_rage", false);
    }
    else if (statisfaction < 0)
    {
      animator.SetBool("in_love", false);
      animator.SetBool("in_rage", true);
    }
    else
    {
      animator.SetBool("in_love", false);
      animator.SetBool("in_rage", false);
    }
  }
}