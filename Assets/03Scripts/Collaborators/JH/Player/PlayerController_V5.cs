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

    private Transform bulletPos;
    #endregion

    [SerializeField]
    private float fireCoolTime = 0.5f;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float bulletSpeed = 15f;
    [SerializeField]
    private float bulletDamage = 5f;

    private bool coolDowned = true;

    private bool isJump = false;
    private bool jumpReady = false;

    private bool canMove = true;

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

        bulletPos = transform.GetChild(1);

        for (int i = 0; i < 20; i++)
        {
            bulletStack.Push(obj = Instantiate(bulletPrefab));
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
        if (canMove)
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

        if (canMove)
        {
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

            if (Input.GetKeyDown(KeyCode.X) && coolDowned)
            {
                anim.SetTrigger("Attack");

                coolDowned = false;
                Invoke("CoolDown", fireCoolTime);
            }
        }

        if (jumpReady)
        {
            movement.Jump(1f);

            isJump = true;

            jumpReady = false;
        }
    }

    private void CoolDown()
    {
        coolDowned = true;
    }

    public void Controll_ON()
    {
        Debug.Log("발사 완료");
        canMove = true;
    }
    public void Controll_OFF()
    {
        Debug.Log("발사 준비");
        movement.Break();
        canMove = false;
    }
    public void BulletFire()
    {
        Debug.Log("발사");
        if (bulletStack.TryPop(out obj))
        {
            obj.SetActive(true);

            obj.transform.position = bulletPos.position;
            obj.GetComponent<PlayerBullet>().InitInfo(Mathf.Approximately(Mathf.Abs(transform.eulerAngles.y), 180f) ? Vector2.left : Vector2.right, bulletSpeed, bulletDamage);
        }
        else
        {
            Debug.Log("총알 없음");
        }
    }
    private IEnumerator Jumping()
    {
        yield return YieldInstructionCache.WaitForSeconds(0.25f);

        jumpReady = false;
    }
}
