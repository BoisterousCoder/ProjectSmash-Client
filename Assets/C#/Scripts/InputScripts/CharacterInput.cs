using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Assets.Scripts.InputScripts
{
    public class CharacterInput : MonoBehaviour
    {

        private float runSpeed = (float) 0.1;
        private int jumpSpeed = 3;
        private float currentJumpPosition;
        private float jumpFrames = 50;
        private bool isCrouching = false;
        private float groundedTolerance = (float)0.1;
        public KeyCode leftKey = KeyCode.A;
        public KeyCode upKey = KeyCode.W;
        public KeyCode downKey = KeyCode.S;
        public KeyCode rightKey = KeyCode.D;
        public KeyCode specialKey = KeyCode.LeftControl;
        public KeyCode attackKey = KeyCode.LeftShift;
        public bool IsSpecialAttack
        {
            get
            {
                return animator.GetBool("IsSpecialAttack");
            }
            set
            {
                if (IsWalking != value)
                    animator.SetBool("IsSpecialAttack", value);
            }
        }
        public AttackDirection AttackDir
        {
            get
            {
                return (AttackDirection)animator.GetInteger("AttackDir");
            }
            set
            {
                if (AttackDir != value)
                    animator.SetInteger("AttackDir", (int)value);
            }
        }
        public bool IsWalking
        {
            get
            {
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
        private Collider2D collider2D;

        // Use this for initialization
        void Start()
        {
            animator = GetComponent<Animator>();
            rigidBody = GetComponent<Rigidbody2D>();
            collider = GetComponent<Collider>();
            collider2D = GetComponent<Collider2D>();

            IsWalking = false;
            IsGrounded = true;
            AttackDir = AttackDirection.Neutral;
        }

        // Update is called once per frame
        void Update()
        {
            bool isWalkingInUpdate = false;
            bool isAttacking = Input.GetKey(attackKey) || Input.GetKey(specialKey);
            bool isPressingWalk = Input.GetKey(rightKey) || Input.GetKey(leftKey);
            MoveDirection direction = MoveDirection.None;
            direction = (Input.GetKey(leftKey)) ? MoveDirection.Left : direction;
            direction = Input.GetKey(rightKey) ? MoveDirection.Right : direction;

            if (isAttacking)
                SetAttackDir();
            else if (isPressingWalk)
                Move(direction, out isWalkingInUpdate);
            else if (Input.GetKey(upKey) && IsGrounded)
                Jump();

            if (!isAttacking) AttackDir = AttackDirection.Neutral;
            int rotation = 90 - (int)direction * 90;
            if(direction != MoveDirection.None) transform.localRotation = Quaternion.Euler(0, rotation, 0);

            IsSpecialAttack = Input.GetKey(specialKey);
            IsWalking = isWalkingInUpdate;
            IsGrounded = IsOnFloor();
        }
        void Move(MoveDirection direction)
        {
            transform.localPosition += new Vector3(runSpeed * (int)direction, 0, 0);
        }
        void Move(MoveDirection direction, out bool isWalkingInUpdate)
        {
            Move(direction);
            isWalkingInUpdate = true;
        }
        void Jump()
        {
            Debug.Log("JUMP");
            rigidBody.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
        }
        void SetAttackDir()
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
        bool IsOnFloor()
        {
            var position = transform.position;
            var floors = GameObject.FindGameObjectsWithTag("floor");
            foreach (var floor in floors)
            {
                float closestDistance = floor.GetComponent<Collider2D>().Distance(collider2D).distance;
                Debug.Log(closestDistance);
                if (closestDistance < groundedTolerance) return true;
            }
                
            return false;
        }
    }
    public enum AttackDirection : int
    {
        Neutral = 0,
        Up = 1,
        Side = 2,
        Down = 3
    }
    enum MoveDirection : int
    {
        None = 0,
        Left = -1,
        Right = 1
    }
}