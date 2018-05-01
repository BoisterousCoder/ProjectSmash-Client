using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Assets.Scripts.InputScripts
{
    public class CharacterInput : MonoBehaviour
    {

        public float runSpeed;
        public float jumpSpeed = 7;
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
                return animator.GetBool("IsSpecialAttack");
            }
            set
            {
                if (IsWalking != value)
                    animator.SetBool("IsSpecialAttack", value);
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

            bool isAttacking = Input.GetKey(attackKey) || Input.GetKey(specialKey);
            if (isAttacking)
            {
                if (Input.GetKey(rightKey) || Input.GetKey(leftKey))
                    AttackDir = AttackDirection.Side;
                else if (Input.GetKey(upKey))
                    AttackDir = AttackDirection.Up;
                else if (Input.GetKey(downKey))
                    AttackDir = AttackDirection.Down;
                else
                    AttackDir = AttackDirection.Neutral;
            }
            else if (Input.GetKey(rightKey))
            {
                charecterMovement.x = runSpeed;
            }
            else if (Input.GetKey(leftKey))
            {
                charecterMovement.x = -runSpeed;
            }else if(Input.GetKey(upKey) && IsGrounded)
            {
                rigidBody.AddForce(new Vector2(0, 7), ForceMode2D.Impulse);
            }

            if (!isAttacking) AttackDir = AttackDirection.Neutral;
            if(Input.GetKey(rightKey)) transform.localRotation = Quaternion.Euler(0, 0, 0);
            else if (Input.GetKey(leftKey)) transform.localRotation = Quaternion.Euler(0, 180, 0);

            IsSpecialAttack = Input.GetKey(specialKey);
            IsGrounded = rigidBody.velocity.y == 0;
            IsWalking = !(charecterMovement.Equals(new Vector3()));
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
