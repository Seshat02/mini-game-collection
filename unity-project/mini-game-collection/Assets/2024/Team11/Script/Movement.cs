/*  
 *      Third Person Player Movement Script v1.1 by Ian McCambridge
 *      :: Free to use always <3 2020 ::
 *  
*       This script pairs with my "Third Person Camera Script" which can be found here:
*       https://gist.github.com/kidchemical/b1542ea489c8f2abae3fbd09798dedd4
 *   FEATURE OUTLINE:
     *   -Rigidbody required. 
     *      -Plane or Ground must have Tag property set to new tag named "Ground"
     *      -Freeze X and Z Rotation for player Rigidbody
     *      -Uses 'force' for movement, but 'transform ' for rotation.
     *   -WASD Movement, Spacebar Jump, Controller support
     *   -No Strafe, horizontal axis of input turns (rotates) player (feels like driving controls...)
 *   
 *
 *   TO ADD:
     *   -Loose Camera "Look At"
     *   -Camera snaps behind player when moving / running?
     *   -Fix turning mechanism to feel more natural
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniGameCollection;
using UnityEngine.InputSystem.XR;
using UnityEngine.Windows;

public class Movement : MonoBehaviour
{
    [field: SerializeField, Range(1, 2)]
    private int PlayerID { get; set; } = 1;
    public int ID => PlayerID - 1;

    public float moveSpeed = 2;
    public float rotationSpeed = 4;
    float runningSpeed;
    float vaxis, haxis;
    public bool isJumping, isGrounded, isAttacking = false;
    Vector3 movement;

    private Animator animator; 
    private Rigidbody rb;
    private bool attacked;


    void Start()
    {
        Debug.Log("Initialized: (" + this.name + ")");
        animator = GetComponent<Animator>();        
        rb = GetComponent<Rigidbody>();
        
    }


    void FixedUpdate()
    {
        /*  Controller Mappings */
        vaxis = ArcadeInput.Players[ID].AxisY;
        //vaxis = Input.GetAxis("P1_AxisY");
        haxis = ArcadeInput.Players[ID].AxisX;
        //haxis = Input.GetAxis("P1_AxisX");
        isJumping = ArcadeInput.Players[ID].Action1.Down;
        isAttacking = ArcadeInput.Players[ID].Action2.Down;

        //Simplified...
        runningSpeed = vaxis;


        if (isGrounded)
        {
            movement = new Vector3(0, 0f, runningSpeed * 5);        // Multiplier of 8 seems to work well with Rigidbody Mass of 1.
            movement = transform.TransformDirection(movement);      // transform correction A.K.A. "Move the way we are facing"
        }
        else
        {
            movement *= 0.70f;                                      // Dampen the movement vector while mid-air
        }



        if (runningSpeed != 0 || moveSpeed != 0 && isGrounded)
        {
            animator.SetBool("isWalking", true);
        }

        rb.AddForce(movement * moveSpeed);   // Movement Force

        if (movement == Vector3.zero)
        {
            animator.SetFloat("Speed", 0);
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetFloat("Speed", 1);
            animator.SetBool("isWalking", true);
        }


        if ((isJumping) && isGrounded)
        {
            Debug.Log(this.ToString() + " isJumping = " + isJumping);
            animator.SetBool("isJumping", true);
            rb.AddForce(Vector3.up * 150);
        }

        //if(ArcadeInput.Player1.Action1.Down) animator.SetBool("isJumping", true);
        //if(ArcadeInput.Player1.Action1.Released) animator.SetBool("isJumping", false);

        if (isAttacking)
        {
            animator.SetBool("isAttacking", true);
            attacked = true;

            if(attacked)
            {
                attacked = false;
                animator.SetBool("isAttacking", false);
                
            }
        }



        if ((vaxis != 0f || haxis != 0f) && !isJumping && isGrounded)
        {
            if (vaxis >= 0)
                transform.Rotate(new Vector3(0, haxis * rotationSpeed, 0));
            else
                transform.Rotate(new Vector3(0, -haxis * rotationSpeed, 0));

        }

    }

    
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Entered");
        if (collision.gameObject.CompareTag("Arena"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        Debug.Log("Exited");
        if (collision.gameObject.CompareTag("Arena"))
        {
            animator.SetBool("isJumping",false);
            isGrounded = false;
        }
    }
        
}