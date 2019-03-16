﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed;
    
    private float horizontalMove = 0f;
    private bool isJumping = false;
    private bool isCrouching = false;
    private int landCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            animator.SetBool("isJumping", true);
        }
        if (Input.GetButtonDown("Crouch") && !animator.GetBool("isJumping"))
        {
            isCrouching = true;
            animator.SetBool("isCrouching", true);
        } else if (Input.GetButtonUp("Crouch") && !animator.GetBool("isJumping"))
        {
            isCrouching = false;
            animator.SetBool("isCrouching", false);
        }
    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, isCrouching, isJumping);
        isJumping = false;
    }
}
