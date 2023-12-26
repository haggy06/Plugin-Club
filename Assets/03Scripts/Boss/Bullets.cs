using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullets : MonoBehaviour
{
    Rigidbody2D rigid;
    BoxCollider2D collision;
    private Vector2 newPos;
    public Rigidbody2D target;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();   // 1. 어차피 한 번 밖에 안 쓸 콤포넌트는 굳이 참조해 올 필요가 없음(메모리 낭비)
        newPos = new Vector2(Random.Range(-8f, 8f), -3f);
        GetComponent<Rigidbody2D>().velocity = newPos * 2f; // ┳ 2. 뭘 의도하려던 건진 모르겠지만 하나만 써도 될 듯 함(위쪽 코드 추천)
        rigid.velocity = new Vector2(newPos.x, newPos.y) * 2;    // ┛
        Vector2 direction = transform.forward;
        var quaternion = Quaternion.Euler(0, 1, 0);
        Vector2 newDirection = quaternion * direction; // 3. 


        float angle = Mathf.Atan2(-newPos.x, -newPos.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);

    }

    public void OnCollisionEnter2D(UnityEngine.Collision2D collision) // 2. 이런 총알 류의 오브젝트는 닿아서 밀려나면 안 되니까(의도했다면 모르지만) isTrigger를 켜 주고 OnTriggerEnter2D 를 쓰자
    {
        if (collision.collider.gameObject.CompareTag("Tiles") || collision.collider.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

    
}
