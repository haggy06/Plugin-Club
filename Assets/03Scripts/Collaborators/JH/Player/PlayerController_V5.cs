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
    private GameObject bulletPrefab;
    [SerializeField]
    private float bulletSpeed = 15f;
    [SerializeField]
    private float bulletDamage = 5f;

    private bool coolDowned = true;

    private bool isJump = false;
    private bool jumpReady = false;

    private bool canMove = true;

    private GameObject obj;
    private void Awake()
    {
        movement = GetComponentInChildren<RigidMovement>();
        checker = GetComponentInChildren<GroundTypeChecker>();

        sprite = GetComponent<SpriteRenderer>();
        rigid2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        bulletPos = transform.GetChild(1);
    }

    /*
    [SerializeField, Range(-1f, 1f)] private float moveDirection = 0f;
    [SerializeField, Range(-1f, 1f)] private float lastMoveDirection = 0f;
     */
    private float moveDirection = 0f;
    private float lastMoveDirection = 5f;
    private void FixedUpdate()
    {/*
        if (canMove)
        {*/
            moveDirection = Input.GetAxisRaw("Horizontal");

            if (!Mathf.Approximately(lastMoveDirection, moveDirection))
            {
                if (Mathf.Approximately(moveDirection, -1f))
                {
                    movement.LeftMove();
                }
                else if (Mathf.Approximately(moveDirection, 1f))
                {
                    movement.RightMove();
                }
                else
                {
                    movement.Break();
                }

                lastMoveDirection = moveDirection;
            }/*
        }*/
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

    public void EnterPortal()
    {
        movement.Break();
        lastMoveDirection = 0f;

        GetComponent<PlayerHitCheck>().invincible = true;
        canMove = false;
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
                    Audio.Inst.PlaySfx(Audio.Sfx.Jump1);
                    StartCoroutine("Jumping");
                }
                else if (movement.WaterJump)
                {
                    movement.Swim();
                }
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                anim.SetTrigger("Attack");
                Audio.Inst.PlaySfx(Audio.Sfx.Shoot1);
            }
        }

        if (Input.GetKeyUp(KeyCode.Z))
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
        canMove = false;
    }
    public void BulletFire()
    {
        Debug.Log("발사");

        obj = Instantiate(bulletPrefab);

        obj.transform.position = bulletPos.position;
        obj.GetComponent<PlayerBullet>().InitInfo(Mathf.Approximately(Mathf.Abs(transform.eulerAngles.y), 180f) ? Vector2.left : Vector2.right, bulletSpeed, bulletDamage);
    }
    private IEnumerator Jumping()
    {
        yield return YieldInstructionCache.WaitForSeconds(0.25f);

        jumpReady = false;
    }

    public void PlayerDead()
    {
        Debug.Log("플레이어 사망");
        movement.StopAllCoroutines();
        StopAllCoroutines();

        rigid2D.velocity = Vector2.zero;

        sprite.color = Color.red;
        transform.Rotate(new Vector3(0f, 0f, 90f));
        Destroy(GetComponent<PlayerHitCheck>());
        enabled = false;
    }

    public void Clear()
    {
        gameObject.layer = 8;
    }
}
