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
    BoxCollider2D box;   // TIP : Circle Collider2D, Box Collider 2D 등 모든 Collider2D는 Collider2D로 참조 할 수 있음
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
    public Transform parent;   // 5. 자기 자신의 Transform은 참조할 이유 X
    bool Emergence;




    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        Bosshp = 30f;   // 1. 초기값 설정은 Awake에서 하는 것 보다 변수 선언과 동시에 하는 게 더 깔끔할 것 같음(어차피 상수 넣을 거라면)
        spriteRenderer.flipX = true;

        
        Invoke("Ready", 3);
        
    }
    void FixedUpdate()
    {
        

        rigid.velocity = new Vector2(nextmove, rigid.velocity.y);   // 2. 함수 내부에서 변수 선언 또는 new 값을 만들 경우
        spriteRenderer.flipX = target.position.x < rigid.position.x; //    변수가 돌아갈 때마다 가비지가 쌓이게 되므로 최적화에 안 좋음

    }

    private void LateUpdate()   // 6. nextmove 가 업데이트 될 때만 체크하면 쓸데없는 연산량을 줄일 수 있음 
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

        if (ptrn == 1f)   // 3. else if 문이나 switch 문으로 쓰는 게 더 좋을 것 같음
        {
            Invoke("Ptrn1", 1);
        }

        if (ptrn == 2f)   // 4. int 값 비교를 굳이 float으로 할 필요 없음
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
        for(int i = 0; i < 6 ; i++)    // TIP : 이런 탄막 개수 지정은 상수로 넣지 말고 따로 변수로 빼 두는 게 관리하기 좋음(아니면 스크립트 고치고 플레이모드 껐다 켜야 하기 때문)
        { 
        GameObject bullet = Instantiate(Feather);   // TIP : 오브젝트 생성할 때 바로 Transform, rotation 지정 가능함
        bullet.transform.position = parent.position;
        }
            
        Invoke("Think", Random.Range(3f, 4f));
    }

    void Ptrn2()
    {
        // 빠르게 이동하며 아래로 알 투하-> 알 부화 후 발사체 발사
        for (int i = 0; i < 1 ; i++)
        {
            // 알 생성 
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


