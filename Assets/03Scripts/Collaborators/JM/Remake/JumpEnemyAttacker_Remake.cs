using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEnemyAttacker_Remake : MonoBehaviour
{
    [Header("For petrolling")]
    [SerializeField] float moveSpeed;
    private float moveDirection = 1;
    private bool facingRight = true; //슬라임 고개돌림
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Transform wallCheckPoint;
    [SerializeField] float circleRadius; //슬라임 패트롤 범위 설정을 위해(원모양)
    [SerializeField] LayerMask groundLayer;
    private bool checkingGround;
    private bool checkingWall;

    [Header("For JumpAttacking")] //점프해서 공격
    [SerializeField] float jumpHeight;
    [SerializeField] Transform Player;
    [SerializeField] Transform groundCheck;
    [SerializeField] Vector2 boxSize; // (사각형모양)
    private bool isGrounded;

    [Header("For SeeingPlayer")] // 플레이어 볼 때
    [SerializeField] Vector2 lineOfSite;
    [SerializeField] LayerMask PlayerLayer;
    private bool canSeePlayer;

    [Header("other")]
    private Rigidbody2D enemyRB;
    // private Animator (이름); //애니메이션 넣을 때 하기 !! - 만약 애니메이션을 넣는 더 좋은 방법을 아신다면, 그것을 따르세요.

    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        //(이름) = GetComponent<Animator>(); //애니메이션!!

    }

    void FixedUpdate() //패트롤을 위해 필요
    {
        checkingGround = Physics2D.OverlapCircle(groundCheckPoint.position, circleRadius, groundLayer);  //땅
        checkingWall = Physics2D.OverlapCircle(wallCheckPoint.position, circleRadius, groundLayer);  // 벽
        isGrounded = Physics2D.OverlapBox(groundCheck.position, boxSize, 0, groundLayer); //점프공격
        canSeePlayer = Physics2D.OverlapBox(transform.position, lineOfSite, 0, PlayerLayer); //플레이어 볼 때
                                                                                             // AnimationController(); // 애니메이션!!
        if (!canSeePlayer && isGrounded) //플레이어 안 볼 때
        {
            Petrolling();
        }
    }

    void Petrolling() //패트롤하는 슬라임
    {
        if (!checkingGround || checkingWall)
        {
            if (facingRight)
            {
                Flip(); //Sprite Renderer에서 Flip X와 Y를 체크하면 좌우, 상하로 이미지를 반전시킬 수 있다. 이것을 코드로 옮김. 포토샵 같은 프로그램으로 이미지를 돌려서 저장할 필요 없이, 유니티 내에서 코드 한 줄로 이미지를 뒤집을 수 있음. 슬라임이 플레이어쪽으로 고개 돌릴 때 적용.
            }
            else if (!facingRight)
            {
                Flip();
            }
        }
        enemyRB.velocity = new Vector2(moveSpeed * moveDirection, enemyRB.velocity.y);
    }

    void JumpAttack() //점프공격하는 슬라임
    {
        float distanceFromPlayer = Player.position.x - transform.position.x;

        if (isGrounded)
        {
            enemyRB.AddForce(new Vector2(distanceFromPlayer, jumpHeight), ForceMode2D.Impulse);

        }
    }

    void FlipTowardsPlayer() //플레이어 향해 고개 돌림
    {
        float PlayerPosition = Player.position.x - transform.position.x;
        if (PlayerPosition < 0 && facingRight)
        {
            Flip();
        }
        else if (PlayerPosition > 0 && !facingRight)
        {
            Flip();
        }
    }

    void Flip() // 고개돌리는 슬라임
    {
        moveDirection *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0); //Rotation Y가 180 -> 고개 반대로 돌아감
    }

    /*void AnimationController() // 애니메이션!!
    {
        (이름).SetBool("canSeePlayer", canSeePlayer);
        (이름).SetBool("isGrounded", isGrounded);
    }*/

    private void OnDrawGizmosSelected() //(Gizmos)기즈모 설정
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheckPoint.position, circleRadius);
        Gizmos.DrawWireSphere(wallCheckPoint.position, circleRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawCube(groundCheck.position, boxSize);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, lineOfSite); //플레이어 볼 때
    }
}
