using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Assets.Scripts.InputScripts
{
    public class CharacterInput : MonoBehaviour
    {

        public float runSpeed;
        public float jumpSpeed;
        private float currentJumpPosition;
        private float jumpFrames = 50;
        private float distToGround;
        private bool isCrouching;

        private float previousHorizontal;

        private Animator animator;
        private Rigidbody2D rigidBody;
        private Collider collider;

        // Use this for initialization
        void Start()
        {
            animator = GetComponent<Animator>();
            rigidBody = GetComponent<Rigidbody2D>();
            collider = GetComponent<Collider>();

            distToGround = collider.bounds.extents.y;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 newVector = transform.localPosition;
            if (Input.GetAxis("Horizontal") > 0)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                if (Input.GetAxis("Horizontal") >= previousHorizontal)
                {
                    newVector.x += runSpeed;
                }
            }

            if (Input.GetAxis("Horizontal") < 0)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                if (Input.GetAxis("Horizontal") <= previousHorizontal)
                {
                    newVector.x -= runSpeed;
                }
            }

            if(Input.GetAxis("Vertical") > 0)
            {
                if (IsGrounded())
                {
                    rigidBody.AddForce(new Vector2(0, 7), ForceMode2D.Impulse);
                    animator.SetBool("IsGrounded", false);
                }
            }
            if (IsGrounded())
            {
                animator.SetBool("IsGrounded", true);
            }
            previousHorizontal = Input.GetAxis("Horizontal");

            transform.localPosition = newVector;
        }

        public bool IsGrounded()
        {
            return rigidBody.velocity.y == 0;
        }
    }
}
