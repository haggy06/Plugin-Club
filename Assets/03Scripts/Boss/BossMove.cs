using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class BossMove : MonoBehaviour
{
    Rigidbody2D rigid;
    BoxCollider2D box;
    SpriteRenderer spriteRenderer;
    Collision2D collision;
    Animator animator;
    public int ptrn;
    public float Bosshp;
    public int nextmove;
    public GameObject Feather;
    public GameObject UdoFeather;
    public GameObject Egg;
    public Transform target;
    public Transform parent;
    bool Emergence;




    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();    
        Bosshp = 30f;
        spriteRenderer.flipX = true;

        
        Invoke("Ready", 3);
        
    }
    void FixedUpdate()
    {
        

        rigid.velocity = new Vector2(nextmove, rigid.velocity.y);
        spriteRenderer.flipX = target.position.x < rigid.position.x;

    }

    private void LateUpdate()
    {
        if (nextmove != 0)
        {
            spriteRenderer.flipX = nextmove < 0;
        }
    }

    void Ready()
    {
        animator.SetBool("Emergence", true);
        transform.position = new Vector2(transform.position.x, transform.position.y + 2);
        Invoke("Think", 1);
    }
    void Think()
    {
        
        nextmove = Random.Range(-5 , 5);
        ptrn = Random.Range(1, 4);

        if (ptrn == 1f)
        {
            Invoke("Ptrn1", 1);
        }

        if (ptrn == 2f)
        {
                Invoke("Ptrn2", 1);
        }

        if (ptrn == 3f)
        {
            Invoke("Ptrn3", 1);
        }
    }

    void Ptrn1()
    {
        //깃털 발사
        for(int i = 0; i < 6 ; i++) 
        { 
        GameObject bullet = Instantiate(Feather);
        bullet.transform.position = parent.position;
        }
            
        Invoke("Think", Random.Range(3f, 4f));
    }

    void Ptrn2()
    {
        // 빠르게 이동하며 아래로 알 투하-> 알 부화 후 발사체 발사
        for (int i = 0; i < 1 ; i++)
        {
            GameObject bullet = Instantiate(Egg);
            bullet.transform.position = parent.position;
        }
        Invoke("Think", Random.Range(3f, 4f));
    }

    void Ptrn3()
    {
        //유도탄
        for (int i = 0; i < 2; i++)
        {

            GameObject bullet = Instantiate(UdoFeather);
            bullet.transform.position = parent.position;
            
        }
        Invoke("Think", Random.Range(3f, 4f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        nextmove = -nextmove;
        
    }

}


