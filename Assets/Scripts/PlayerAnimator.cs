using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    public PlayerController playerController;

    public Animator animator;
    public float moveThreshold = 0.1f;

    public float groundCheckDistance = 0.3f;
    public LayerMask groundLayer;

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        float moveAmount = new Vector2(moveX, moveZ).magnitude;
        animator.SetFloat("Blend", moveAmount);
    }
}
