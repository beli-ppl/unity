using System.Collections;
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

        if (Input.GetButtonDown("Crouch") && controller.m_Grounded)
        {
            isCrouching = true;
            animator.SetBool("isCrouching", true);
        } else if (Input.GetButtonUp("Crouch"))
        {
            isCrouching = false;
            animator.SetBool("isCrouching", false);
        }

        if (Input.GetAxisRaw("Horizontal") < 0 && controller.m_FacingRight)
        {
            Debug.Log("AWU");
            controller.setFacing();
        }

        if (Input.GetAxisRaw("Horizontal") > 0 && !controller.m_FacingRight)
        {
            controller.setFacing();
        }
    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }

    void FixedUpdate()
    {
        if (!isCrouching)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, isCrouching, isJumping);
        } else
        {
            controller.Move(0, isCrouching, isJumping);
        }
        isJumping = false;
    }
}
