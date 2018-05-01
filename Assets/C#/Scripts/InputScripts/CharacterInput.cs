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
        private bool isCrouching = false;
        public KeyCode leftKey = KeyCode.A;
        public KeyCode upKey = KeyCode.W;
        public KeyCode downKey = KeyCode.S;
        public KeyCode rightKey = KeyCode.D;
        public KeyCode specialKey = KeyCode.LeftControl;
        public KeyCode attackKey = KeyCode.LeftShift;
        public bool IsSpecialAttack {
            get {
                return animator.GetBool("isSpecialAttack");
            }
            set
            {
                if (IsWalking != value)
                    animator.SetBool("isSpecialAttack", value);
            }
        }
        public AttackDirection AttackDir {
            get
            {
                return (AttackDirection) animator.GetInteger("AttackDir");
            }
            set
            {
                if (AttackDir != value)
                    animator.SetInteger("AttackDir", (int)value);
            }
        }
        public bool IsWalking {
            get {
                return animator.GetBool("IsWalking");
            }
            set
            {
                if (IsWalking != value)
                    animator.SetBool("IsWalking", value);
            }
        }
        public bool IsGrounded
        {
            get
            {
                return animator.GetBool("IsGrounded");
            }
            set
            {
                if (IsGrounded != value)
                    animator.SetBool("IsGrounded", value);
            }
        }

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
            IsWalking = false;
            AttackDir = AttackDirection.Neutral;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 charecterMovement = new Vector3();
            if (Input.GetKey(rightKey) && !Input.GetKey(attackKey))
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                charecterMovement.x = runSpeed;
                IsWalking = true;
            }
            else if (Input.GetKey(leftKey) && !Input.GetKey(attackKey))
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                charecterMovement.x = -runSpeed;
                IsWalking = true;
            }
            else IsWalking = false;

            if(Input.GetKey(upKey))
            {
                if (IsGrounded)
                {
                    rigidBody.AddForce(new Vector2(0, 7), ForceMode2D.Impulse);
                    animator.SetBool("IsGrounded", false);
                }
            }
            IsGrounded = rigidBody.velocity.y == 0;
            transform.localPosition += charecterMovement;
        }
    }
    public enum AttackDirection : int
    {
        Neutral = 0,
        Up = 1,
        Side = 2,
        Down = 3
    }
}
