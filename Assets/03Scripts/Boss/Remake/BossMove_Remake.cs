using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove_Remake : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;

    private Transform target;

    public float Bosshp = 30f;   // 1.
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

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        target = GameObject.FindWithTag("Player").transform;

        NextmoveChange();

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
        transform.position += Vector3.up * 2f;

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
        //깃털 발사
        for (i = 0; i < featherCount; i++)    // TIP
        {
            bullet = Instantiate(feather, transform.position, Quaternion.Euler(0, 0, 0));
            Audio.instance.PlaySfx(Audio.Sfx.ATK1);
        }

        Invoke("Think", Random.Range(3f, 4f));
    }

    void Ptrn2()
    {
        // 빠르게 이동하며 아래로 알 투하-> 알 부화 후 발사체 발사
        for (i = 0; i < eggCount; i++)
        {
            GameObject bullet = Instantiate(egg, transform.position, Quaternion.Euler(0, 0, 0));
            Audio.instance.PlaySfx(Audio.Sfx.ATK1);
        }

        Invoke("Think", Random.Range(3f, 4f));
    }

    void Ptrn3()
    {
        //유도탄
        for (i = 0; i < trackingFeatherCount; i++)
        {
            GameObject bullet = Instantiate(trackingFeather, transform.position, Quaternion.Euler(0, 0, 0));
            Audio.instance.PlaySfx(Audio.Sfx.ATK1);
        }

        Invoke("Think", Random.Range(3f, 4f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        nextmove = -nextmove;

        NextmoveChange();
    }
}
