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
    BoxCollider2D box;   // TIP : Circle Collider2D, Box Collider 2D �� ��� Collider2D�� Collider2D�� ���� �� �� ����
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
    public Transform parent;   // 5. �ڱ� �ڽ��� Transform�� ������ ���� X
    bool Emergence;




    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        Bosshp = 30f;   // 1. �ʱⰪ ������ Awake���� �ϴ� �� ���� ���� ����� ���ÿ� �ϴ� �� �� ����� �� ����(������ ��� ���� �Ŷ��)
        spriteRenderer.flipX = true;

        
        Invoke("Ready", 3);
        
    }
    void FixedUpdate()
    {
        

        rigid.velocity = new Vector2(nextmove, rigid.velocity.y);   // 2. �Լ� ���ο��� ���� ���� �Ǵ� new ���� ���� ���
        spriteRenderer.flipX = target.position.x < rigid.position.x; //    ������ ���ư� ������ �������� ���̰� �ǹǷ� ����ȭ�� �� ����

    }

    private void LateUpdate()   // 6. nextmove �� ������Ʈ �� ���� üũ�ϸ� �������� ���귮�� ���� �� ���� 
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

        if (ptrn == 1f)   // 3. else if ���̳� switch ������ ���� �� �� ���� �� ����
        {
            Invoke("Ptrn1", 1);
        }

        if (ptrn == 2f)   // 4. int �� �񱳸� ���� float���� �� �ʿ� ����
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
        //���� �߻�
        for(int i = 0; i < 6 ; i++)    // TIP : �̷� ź�� ���� ������ ����� ���� ���� ���� ������ �� �δ� �� �����ϱ� ����(�ƴϸ� ��ũ��Ʈ ��ġ�� �÷��̸�� ���� �Ѿ� �ϱ� ����)
        { 
        GameObject bullet = Instantiate(Feather);   // TIP : ������Ʈ ������ �� �ٷ� Transform, rotation ���� ������
        bullet.transform.position = parent.position;
        }
            
        Invoke("Think", Random.Range(3f, 4f));
    }

    void Ptrn2()
    {
        // ������ �̵��ϸ� �Ʒ��� �� ����-> �� ��ȭ �� �߻�ü �߻�
        for (int i = 0; i < 1 ; i++)
        {
            // �� ���� 
            GameObject bullet = Instantiate(Egg);
            bullet.transform.position = parent.position;
        }
        Invoke("Think", Random.Range(3f, 4f));
    }

    void Ptrn3()
    {
        //����ź
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


