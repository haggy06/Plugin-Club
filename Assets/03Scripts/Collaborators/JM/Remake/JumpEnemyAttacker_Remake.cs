using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEnemyAttacker_Remake : MonoBehaviour
{
    [Header("For petrolling")]
    [SerializeField] float moveSpeed;
    private float moveDirection = 1;
    private bool facingRight = true; //������ ������
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Transform wallCheckPoint;
    [SerializeField] float circleRadius; //������ ��Ʈ�� ���� ������ ����(�����)
    [SerializeField] LayerMask groundLayer;
    private bool checkingGround;
    private bool checkingWall;

    [Header("For JumpAttacking")] //�����ؼ� ����
    [SerializeField] float jumpHeight;
    [SerializeField] Transform Player;
    [SerializeField] Transform groundCheck;
    [SerializeField] Vector2 boxSize; // (�簢�����)
    private bool isGrounded;

    [Header("For SeeingPlayer")] // �÷��̾� �� ��
    [SerializeField] Vector2 lineOfSite;
    [SerializeField] LayerMask PlayerLayer;
    private bool canSeePlayer;

    [Header("other")]
    private Rigidbody2D enemyRB;
    // private Animator (�̸�); //�ִϸ��̼� ���� �� �ϱ� !! - ���� �ִϸ��̼��� �ִ� �� ���� ����� �ƽŴٸ�, �װ��� ��������.

    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        //(�̸�) = GetComponent<Animator>(); //�ִϸ��̼�!!

    }

    void FixedUpdate() //��Ʈ���� ���� �ʿ�
    {
        checkingGround = Physics2D.OverlapCircle(groundCheckPoint.position, circleRadius, groundLayer);  //��
        checkingWall = Physics2D.OverlapCircle(wallCheckPoint.position, circleRadius, groundLayer);  // ��
        isGrounded = Physics2D.OverlapBox(groundCheck.position, boxSize, 0, groundLayer); //��������
        canSeePlayer = Physics2D.OverlapBox(transform.position, lineOfSite, 0, PlayerLayer); //�÷��̾� �� ��
                                                                                             // AnimationController(); // �ִϸ��̼�!!
        if (!canSeePlayer && isGrounded) //�÷��̾� �� �� ��
        {
            Petrolling();
        }
    }

    void Petrolling() //��Ʈ���ϴ� ������
    {
        if (!checkingGround || checkingWall)
        {
            if (facingRight)
            {
                Flip(); //Sprite Renderer���� Flip X�� Y�� üũ�ϸ� �¿�, ���Ϸ� �̹����� ������ų �� �ִ�. �̰��� �ڵ�� �ű�. ���伥 ���� ���α׷����� �̹����� ������ ������ �ʿ� ����, ����Ƽ ������ �ڵ� �� �ٷ� �̹����� ������ �� ����. �������� �÷��̾������� �� ���� �� ����.
            }
            else if (!facingRight)
            {
                Flip();
            }
        }
        enemyRB.velocity = new Vector2(moveSpeed * moveDirection, enemyRB.velocity.y);
    }

    void JumpAttack() //���������ϴ� ������
    {
        float distanceFromPlayer = Player.position.x - transform.position.x;

        if (isGrounded)
        {
            enemyRB.AddForce(new Vector2(distanceFromPlayer, jumpHeight), ForceMode2D.Impulse);

        }
    }

    void FlipTowardsPlayer() //�÷��̾� ���� �� ����
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

    void Flip() // �������� ������
    {
        moveDirection *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0); //Rotation Y�� 180 -> �� �ݴ�� ���ư�
    }

    /*void AnimationController() // �ִϸ��̼�!!
    {
        (�̸�).SetBool("canSeePlayer", canSeePlayer);
        (�̸�).SetBool("isGrounded", isGrounded);
    }*/

    private void OnDrawGizmosSelected() //(Gizmos)����� ����
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheckPoint.position, circleRadius);
        Gizmos.DrawWireSphere(wallCheckPoint.position, circleRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawCube(groundCheck.position, boxSize);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, lineOfSite); //�÷��̾� �� ��
    }
}
