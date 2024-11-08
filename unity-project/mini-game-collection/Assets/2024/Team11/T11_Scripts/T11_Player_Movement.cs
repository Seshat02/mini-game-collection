using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniGameCollection;
using UnityEngine.InputSystem.XR;
using UnityEngine.Windows;

namespace MiniGameCollection.Games2024.Team11
{
    public class T11_Player_Movement : MonoBehaviour
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

        //Respawning        
        [SerializeField] private Transform respawnPoint;

        //Spawn_Attack variables
        public GameObject spherePrefab;
        public Transform spawningPos;
        public float sphereSpeed = 10f;

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

            Motion();

            if (isAttacking)
            {
                //Instantiate(sphere, spawningPos.transform.position, Quaternion.identity);
                var sphere = Instantiate(spherePrefab, spawningPos.position, Quaternion.identity);
                sphere.GetComponent<Rigidbody>().velocity = spawningPos.forward * sphereSpeed;
            }
                                 

        }

        private void Motion()
        {
            if (isGrounded)
            {
                movement = new Vector3(0, 0f, runningSpeed * 5);
                movement = transform.TransformDirection(movement);
            }
            else
            {
                movement *= 0.70f;                                      // Dampen the movement vector while mid-air
            }
                        
            rb.AddForce(movement * moveSpeed);   // Movement Force


            if ((isJumping) && isGrounded)
            {
                Debug.Log(this.ToString() + " isJumping = " + isJumping);
                animator.SetBool("isJumping", true);
                rb.AddForce(Vector3.up * 150);
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
                animator.SetBool("isJumping", false);
                isGrounded = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collider"))
            {
                this.transform.position = respawnPoint.transform.position;
                Physics.SyncTransforms();
            }
        }



    }
}