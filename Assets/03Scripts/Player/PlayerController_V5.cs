using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_V5 : MonoBehaviour
{
    #region Imported Things
    private RigidMovement movement;
    private GroundTypeChecker checker;

    private Rigidbody2D rigid2D;
    #endregion

    private bool isJump = false;
    private bool jumpReady = false;

    private void Awake()
    {
        movement = gameObject.GetComponentInChildren<RigidMovement>();
        checker = gameObject.GetComponentInChildren<GroundTypeChecker>();

        rigid2D = gameObject.GetComponent<Rigidbody2D>();
    }

    /*
    [SerializeField, Range(-1f, 1f)] private float moveDirection = 0f;
    [SerializeField, Range(-1f, 1f)] private float lastMoveDirection = 0f;
     */
    private float moveDirection = 0f;
    private void FixedUpdate()
    {
        moveDirection = Input.GetAxisRaw("Horizontal");

        if (moveDirection == -1)
        {
            movement.LeftMove();
        }
        else if (moveDirection == 1)
        {
            movement.RightMove();
        }
        else
        {
            movement.Break();
        }
        /*
        moveDirection = Input.GetAxis("Horizontal");
        
        if (Mathf.Abs(lastMoveDirection - moveDirection) > 0.2f)
        {
            Debug.Log("Before : " + moveDirection + ", " + lastMoveDirection);
            
            moveDirection += lastMoveDirection;

            Debug.Log("After : " + moveDirection + ", " + lastMoveDirection);
        }

        lastMoveDirection = moveDirection;

        playerMove.Xmove(moveDirection);
        */
    }

    private void Update()
    {
        if (isJump && rigid2D.velocity.y <= movement.JumpCanclePower)
        {
            isJump = false;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (checker.IsGrounded)
            {
                jumpReady = true;
                
                StartCoroutine("Jumping");
            }
            else if (movement.WaterJump)
            {
                movement.Swim();
            }
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            if (isJump && rigid2D.velocity.y > movement.JumpCanclePower)
            {
                movement.JumpCancle();

                jumpReady = false;

                isJump = false;
            }
        }

        if (jumpReady)
        {
            movement.Jump(1f);

            isJump = true;

            jumpReady = false;
        }
    }

    private WaitForSeconds wfs = new WaitForSeconds(0.25f);
    private IEnumerator Jumping()
    {
        yield return wfs;

        jumpReady = false;
    }
}
