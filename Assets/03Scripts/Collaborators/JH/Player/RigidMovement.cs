using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GroundTypeChecker))] // GroundTypeChecker 가 없을 경우 플레이 전 추가해 주는 함수
public class RigidMovement : MonoBehaviour
{
    #region Imported Things
    private GroundTypeChecker typeChecker;

    //private SpriteRenderer sprite;
    private Animator anim;
    #endregion

    #region Simple Settings
    [Header("Simple Settings"), Space(5)]
    [SerializeField, Tooltip("이 스크립트가 담당하는 게임오브젝트의 Rigidbody2D를 넣어주세요")]
    private Rigidbody2D rigid2D;

    [SerializeField]
    private bool autoWaterOut = true;
    #endregion

    #region Stat
    [Header("Stat"), Space(5)]

    [SerializeField]
    private char moveStatus = '0';

    [SerializeField]
    private bool watered = false;

    [SerializeField]
    private bool grounded = false;

    [Space(5)]

    [Range(0f, 500f), Tooltip("이동 속도 비율( 100이 기본속도 )")]
    public float moveSpeedRatio = 100f; // 속도 변화율(백분율)
    #region moveSpeedRatio 캡슐화
    public float MoveSpeedRatio
    {
        get => moveSpeedRatio;
        set => moveSpeedRatio = value;
    }
    #endregion

    [Range(0f, 500f), Tooltip("점프 파워 비율( 100이 기본속도 )")]
    public float jumpPowerRatio = 100f; // 속도 변화율(백분율)
    #region jumpPowerRatio 캡슐화
    public float JumpPowerRatio
    {
        get => jumpPowerRatio;
        set => jumpPowerRatio = value;
    }
    #endregion

    [Space(10)]

    [SerializeField, Range(0.05f, 5f)]
    private float boostLeadTime = 0.1f;
    #region boostLeadTime 캡슐화
    public float BoostLeadTime { get => boostLeadTime; }
    #endregion

    [SerializeField, Range(0f, 1f)]
    private float friction = 1f;
    #region friction 캡슐화
    public float Friction { get => friction; }
    #endregion

    [Space(10)]

    [SerializeField]
    private bool isMove = false;
    #region moveSpeed 캡슐화
    public bool IsMove { get => isMove; }
    #endregion

    [SerializeField]
    private float moveSpeed = 7f;
    #region moveSpeed 캡슐화
    public float MoveSpeed { get => moveSpeed; }
    #endregion

    [SerializeField]
    private float jumpPower = 16f;
    #region jumpPower 캡슐화
    public float JumpPower { get => jumpPower; }
    #endregion

    [SerializeField]
    private float jumpCanclePower = 5f;
    #region jumpCanclePower 캡슐화
    public float JumpCanclePower { get => jumpCanclePower; }
    #endregion

    [SerializeField]
    private float terminalVelocity = -15f;
    #region terminalVelocity 캡슐화
    public float TerminalVelocity { get => terminalVelocity; }
    #endregion
    #endregion

    #region Ground's Status
    [Space(5), Header("Ground's Status"), Space(5)]

    [SerializeField, Tooltip("{ 이동속도, 점프 파워, 점프 캔슬 파워, 종단속도, 중력 스케일 }")]
    private float[] commonGroundPhysics = { 7f, 16f, 5f, -15f, 3f };

    [Space(10)]

    [SerializeField, Tooltip("{ 가속도(0.05 ~ 5), 마찰력(0 ~ 1) }")]
    private float[] normalGround = { 0.1f, 1f };

    [SerializeField, Tooltip("{ 가속도(0.05 ~ 5), 마찰력(0 ~ 1) }")]
    private float[] iceGround = { 0.5f, 0.175f };
    #endregion

    #region Water's Status
    [Space(15), Header("Water's Status"), Space(5)]

    [SerializeField, Tooltip("{ 이동속도, 점프 파워, 점프 캔슬 파워, 종단속도, 중력 스케일 }")]
    private float[] normalWaterPhysics = { 5f, 6f, 6f, -5f, 1f };

    [SerializeField, Tooltip("{ 가속도(0.05 ~ 5), 마찰력(0 ~ 1) }")]
    private float[] normalWater = { 0.15f, 0.75f };

    [Space(10)]

    [SerializeField, Tooltip("{ 이동속도, 점프 파워, 점프 캔슬 파워, 종단속도, 중력 스케일 }")]
    private float[] swampWaterPhysics = { 2f, 2f, 2f, -2f, 0.5f };

    [SerializeField, Tooltip("{ 가속도(0.05 ~ 5), 마찰력(0 ~ 1) }")]
    private float[] swampWater = { 0.1f, 1f };
    #endregion

    #region Sundry Status
    [Space(15), Header("Sundry Status")]

    [SerializeField]
    private bool waterJump;
    #region waterJump 캡슐화
    public bool WaterJump { get => waterJump; }
    #endregion
    #endregion

    private void Awake()
    {
        typeChecker = GetComponent<GroundTypeChecker>();

        GameObject obj = transform.parent.gameObject;
        if (obj == null)
        {
            Debug.LogError("RIgidMovement가 자식으로 들어있지 않습니다 : " + gameObject.name);
        }
        else
        {
            //sprite = obj.GetComponent<SpriteRenderer>();
            
            if (!obj.TryGetComponent<Animator>(out anim))
            {
                Debug.Log("RigidMovement.cs - Awake() - anim 참조 실패. " + obj.name + "에 Animator가 있는지 확인해 주십시오.");
            }
        }
    }
    
    private void FixedUpdate()
    {
        #region Move Type Converting
        if (moveStatus != typeChecker.CurrentMoveType || watered != typeChecker.IsInWater || grounded != typeChecker.IsGrounded)
        {
            moveStatus = typeChecker.CurrentMoveType;
            watered = typeChecker.IsInWater;
            grounded = typeChecker.IsGrounded;

            if (watered)
            {
                StartCoroutine(Dive());

                switch (moveStatus)
                {
                    case 'U':
                        FrictionChange(normalWater);
                        PhysicsChange(normalWaterPhysics);
                        break;

                    case 'S':
                        FrictionChange(swampWater);
                        PhysicsChange(swampWaterPhysics);
                        break;

                    default:
                        Debug.LogError("RigidMovement.cs - 지정되지 않은 Water 형식입니다 : " + moveStatus);
                        break;
                }
            }

            else if (grounded)
            {
                PhysicsChange(commonGroundPhysics);

                switch (moveStatus)
                {
                    case 'G':
                        FrictionChange(normalGround);
                        break;

                    case 'I':
                        FrictionChange(iceGround);
                        break;

                    default:
                        Debug.LogError("RigidMovement.cs - 지정되지 않은 Ground 형식입니다 : " + typeChecker.CurrentMoveType);
                        break;
                }
            }
        }
        #endregion

        #region Water Out Jumping
        if (waterJump && !typeChecker.IsInWater)
        {
            if (autoWaterOut)
            {
                Jump(1.5f);
                waterJump = false;
            }
            rigid2D.gravityScale = commonGroundPhysics[4]; // 지상의 중력으로 변경
            terminalVelocity = commonGroundPhysics[3]; // 지상의 종단속도로 변경
        }
        #endregion
        
    }

    #region Move Type Converting
    private void FrictionChange(float[] inputArr)
    {
        boostLeadTime = inputArr[0];
        friction = inputArr[1];
    }
    private void PhysicsChange(float[] inputArr)
    {
        moveSpeed = inputArr[0];
        jumpPower = inputArr[1];
        jumpCanclePower = inputArr[2];
        terminalVelocity = inputArr[3];
        rigid2D.gravityScale = inputArr[4];
    }
    #endregion

    #region Move Script
    private Vector2 xMoveVec;
    private float fixedCycle = 0.02f; // 기본적으로 FixedUpdate는 0.02초에 한 번 실행됨
    public void LeftMove()
    {
        transform.parent.rotation = Quaternion.Euler(0f, 180f, 0f);
        isMove = true;

        if (rigid2D.velocity.x >= -moveSpeed * (moveSpeedRatio / 100))
        {
            xMoveVec.x = Mathf.Clamp(rigid2D.velocity.x - (moveSpeed / boostLeadTime * fixedCycle), -moveSpeed * (moveSpeedRatio / 100), float.PositiveInfinity);
        }
        else
        {
            xMoveVec.x = rigid2D.velocity.x + (moveSpeed / boostLeadTime * fixedCycle / 2f);
        }
        xMoveVec.y = rigid2D.velocity.y;

        rigid2D.velocity = xMoveVec;
    }
    public void RightMove()
    {
        transform.parent.rotation = Quaternion.Euler(0f, 0f, 0f);
        isMove = true;

        if (rigid2D.velocity.x <= moveSpeed * (moveSpeedRatio / 100))
        {
            xMoveVec.x = Mathf.Clamp(rigid2D.velocity.x + (moveSpeed / boostLeadTime * fixedCycle), float.NegativeInfinity, moveSpeed * (moveSpeedRatio / 100));
        }
        else
        {
            xMoveVec.x = rigid2D.velocity.x - (moveSpeed / boostLeadTime * fixedCycle / 2f);
        }
        xMoveVec.y = rigid2D.velocity.y;

        rigid2D.velocity = xMoveVec;
    }
    public void Break()
    {
        isMove = false;
        anim.SetBool("IsWalk", false);

        if (Mathf.Abs(rigid2D.velocity.x) <= 0.5f)
        {
            xMoveVec.x = 0f;
            xMoveVec.y = rigid2D.velocity.y;

            rigid2D.velocity = xMoveVec;
        }
        else
        {
            xMoveVec.x = rigid2D.velocity.x + (rigid2D.velocity.x > 0f ? -(moveSpeed * friction * 5f * fixedCycle) : (moveSpeed * friction * 5f * fixedCycle)); // 이동속도가 양수면 저 속도(moveSpeed * friction * 5f * fixedCycle)를 빼주고, 음수면 더해줌
            xMoveVec.y = rigid2D.velocity.y;

            rigid2D.velocity = xMoveVec;
        }
    }

    private Vector2 yMoveVec;
    public void Jump(float rate) // rate : 점프파워 배율
    {
        anim.SetTrigger("Jump");
        yMoveVec.x = rigid2D.velocity.x;
        yMoveVec.y = jumpPower * rate;

        rigid2D.velocity = yMoveVec;
        
        /*
        xMoveVec.x = 0;
        yMoveVec.y = jumpPower * rate;

        rigid2D.AddForce(yMoveVec, ForceMode2D.Force)
        */
    }
    public void JumpCancle()
    {
        yMoveVec.x = rigid2D.velocity.x;
        yMoveVec.y = jumpCanclePower;

        rigid2D.velocity = yMoveVec;
    }
    public void Swim()
    {
        if (rigid2D.velocity.y + jumpPower >= jumpPower)    //  속도 빨라졌을 때 최대속도 넘어갈 경우
        {
            yMoveVec.x = rigid2D.velocity.x;
            yMoveVec.y = Mathf.Clamp(rigid2D.velocity.y - jumpPower / 2f, jumpPower, float.PositiveInfinity);

            rigid2D.velocity = yMoveVec;
        }
        else // 최대속도 안 넘을 경우
        {
            yMoveVec.x = 0f;
            yMoveVec.y = jumpPower;

            //rigid2D.velocity += yMoveVec;
            rigid2D.AddForce(yMoveVec, ForceMode2D.Impulse);   //  점프 세기를 더해줌
        }
    }

    private void LateUpdate()
    {
        if (isMove) // 움직이고 있을 경우
        {
            if (Mathf.Approximately(rigid2D.velocity.x, 0))
            {
                anim.SetBool("IsWalk", false);
            }
            else
            {
                anim.SetBool("IsWalk", true);
            }
        }

        if (!grounded) // 공중에 있을 경우
        {
            if (rigid2D.velocity.y < 0)
            {
                anim.SetBool("IsFalling", true);

                if (rigid2D.velocity.y < terminalVelocity) // 종단속도 구현
                {
                    yMoveVec.x = rigid2D.velocity.x;
                    yMoveVec.y = terminalVelocity;

                    rigid2D.velocity = yMoveVec;
                }
            }
        }
        else
        {
            anim.SetBool("IsFalling", false);
        }
    }

    private IEnumerator Dive()
    {
        yield return YieldInstructionCache.WaitForSeconds(0.1f);

        if (typeChecker.IsInWater)
        {
            waterJump = true;
        }
    }
    #endregion
}