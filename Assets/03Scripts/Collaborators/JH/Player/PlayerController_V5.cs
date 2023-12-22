using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_V5 : MonoBehaviour
{
    #region Imported Things
    private RigidMovement movement;
    private GroundTypeChecker checker;

    private SpriteRenderer sprite;
    private Rigidbody2D rigid2D;
    private Animator anim;
    #endregion

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float bulletSpeed = 15f;
    [SerializeField]
    private float bulletDamage = 5f;

    private bool isJump = false;
    private bool jumpReady = false;

    private Stack<GameObject> bulletStack = new Stack<GameObject>();
    public void ReturnBullet(GameObject returnedBullet)
    {
        returnedBullet.SetActive(false);
        bulletStack.Push(returnedBullet);
    }

    private GameObject obj;
    private void Awake()
    {
        movement = GetComponentInChildren<RigidMovement>();
        checker = GetComponentInChildren<GroundTypeChecker>();

        sprite = GetComponent<SpriteRenderer>();
        rigid2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        for (int i = 0; i < 20; i++)
        {
            bulletStack.Push(obj = Instantiate(bulletPrefab, transform));
            obj.SetActive(false);
        }
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

        if (Input.GetKeyDown(KeyCode.X))
        {
            anim.SetTrigger("Attack");

            if (bulletStack.TryPop(out obj))
            {
                obj.SetActive(true);

                obj.transform.position = transform.position;
                obj.GetComponent<PlayerBullet>().InitInfo(sprite.flipX ? Vector2.left : Vector2.right, bulletSpeed, bulletDamage);
            }
            else
            {
                Debug.Log("ÃÑ¾Ë ¾øÀ½");
            }
        }

        if (jumpReady)
        {
            movement.Jump(1f);

            isJump = true;

            jumpReady = false;
        }
    }

    private IEnumerator Jumping()
    {
        yield return YieldInstructionCache.WaitForSeconds(0.25f);

        jumpReady = false;
    }
}
