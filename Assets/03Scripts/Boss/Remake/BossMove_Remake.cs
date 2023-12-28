using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove_Remake : EnemyBase
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;
    Collider2D col;

    private Transform target;

    public int nextmove = 3;

    [Space(5), SerializeField]
    private GameObject feather;
    [SerializeField]
    private int featherCount = 6;

    [Space(5), SerializeField]
    private GameObject trackingFeather;
    [SerializeField]
    private int trackingFeatherCount = 2;

    [Space(5), SerializeField]
    private GameObject egg;

    [SerializeField]
    private int eggCount = 1;
    // 5

    private new void Awake()
    {
        base.Awake();

        col = GetComponent<Collider2D>();
        col.enabled = false;
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        target = GameObject.FindWithTag("Player").transform;

        Invoke("Ready", 3);
    }

    private Vector2 newVel;
    private void NextmoveChange()
    {
        if (nextmove != 0)
        {
            newVel.x = nextmove;
            newVel.y = rigid.velocity.y;
            rigid.velocity = newVel;   // 2

            spriteRenderer.flipX = nextmove < 0;
        }
    }

    void Ready()
    {
        animator.SetBool("Emergence", true);

        col.enabled = true;
        transform.position = new Vector2(0f, 5f);

        Invoke("Think", 1);
    }
    void Think()
    {
        nextmove = Random.Range(-5, 5);
        NextmoveChange();

        switch (Random.Range(1, 4))   // 3, 4
        {
            case 1:
                Invoke("Ptrn1", 1);
                break;

            case 2:
                Invoke("Ptrn2", 1);
                break;

            case 3:
                Invoke("Ptrn3", 1);
                break;
        }
    }

    int i;
    GameObject bullet;
    void Ptrn1()
    {
        for (i = 0; i < featherCount; i++)    // TIP
        {
            bullet = Instantiate(feather, transform.position, Quaternion.Euler(0, 0, 0));
        }

        Invoke("Think", Random.Range(2f, 3f));
    }

    void Ptrn2()
    {
        for (i = 0; i < eggCount; i++)
        {
            GameObject bullet = Instantiate(egg, transform.position, Quaternion.Euler(0, 0, 0));
        }

        Invoke("Think", Random.Range(2f, 3f));
    }

    void Ptrn3()
    {
        for (i = 0; i < trackingFeatherCount; i++)
        {
            GameObject bullet = Instantiate(trackingFeather, transform.position, Quaternion.Euler(0, 0, 0));
        }

        Invoke("Think", Random.Range(2f, 3f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            nextmove = -nextmove;

            NextmoveChange();
        }
    }

    protected override void EnemyDead()
    {
        sprite.flipY = true;
        sprite.color = Color.gray;
        gameObject.layer = 8;

        GameManager.Inst.Kill_Boss();

        rigid.velocity = Vector2.down;
        Destroy(this);
    }
}