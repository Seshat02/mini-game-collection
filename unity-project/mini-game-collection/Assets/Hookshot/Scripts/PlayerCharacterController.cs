/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour {

    private CharacterController characterController;
    
    private float characterVelocityY;
    private Vector3 characterVelocityMomentum;
   
    private State state;
   

    private enum State {
        Normal,
        HookshotThrown,
        HookshotFlyingPlayer,
    }

    private void Awake() {
        characterController = GetComponent<CharacterController>();
       
    }

    private void Update() {
        switch (state) {
        default:
        case State.Normal:
            HandleCharacterLook();
            HandleCharacterMovement();
           
            break;
        case State.HookshotThrown:
           
            HandleCharacterLook();
            HandleCharacterMovement();
            break;
        case State.HookshotFlyingPlayer:
            HandleCharacterLook();
            
            break;
        }
    }

    private void HandleCharacterLook() {
        float lookX = Input.GetAxisRaw("Mouse X");
        float lookY = Input.GetAxisRaw("Mouse Y");

        //// Rotate the transform with the input speed around its local Y axis
        //transform.Rotate(new Vector3(0f, lookX, 0f), Space.Self);

        //// Add vertical inputs to the camera's vertical angle
        //cameraVerticalAngle -= lookY * mouseSensitivity;

        //// Limit the camera's vertical angle to min/max
        //cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -89f, 89f);

        
    }

    private void HandleCharacterMovement() {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        float moveSpeed = 20f;

        Vector3 characterVelocity = transform.right * moveX * moveSpeed + transform.forward * moveZ * moveSpeed;

        if (characterController.isGrounded) {
            characterVelocityY = 0f;
            // Jump
            if (TestInputJump()) {
                float jumpSpeed = 30f;
                characterVelocityY = jumpSpeed;
            }
        }

        // Apply gravity to the velocity
        float gravityDownForce = -60f;
        characterVelocityY += gravityDownForce * Time.deltaTime;


        // Apply Y velocity to move vector
        characterVelocity.y = characterVelocityY;

        // Apply momentum
        characterVelocity += characterVelocityMomentum;

        // Move Character Controller
        characterController.Move(characterVelocity * Time.deltaTime);

        // Dampen momentum
        if (characterVelocityMomentum.magnitude > 0f) {
            float momentumDrag = 3f;
            characterVelocityMomentum -= characterVelocityMomentum * momentumDrag * Time.deltaTime;
            if (characterVelocityMomentum.magnitude < .0f) {
                characterVelocityMomentum = Vector3.zero;
            }
        }
    }

    private void ResetGravityEffect() {
        characterVelocityY = 0f;
    }

    
    private bool TestInputJump() {
        return Input.GetKeyDown(KeyCode.Space);
    }


}
