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
        rigid = GetComponent<Rigidbody2D>();   // 1. ������ �� �� �ۿ� �� �� ������Ʈ�� ���� ������ �� �ʿ䰡 ����(�޸� ����)
        newPos = new Vector2(Random.Range(-8f, 8f), -3f);
        GetComponent<Rigidbody2D>().velocity = newPos * 2f; // �� 2. �� �ǵ��Ϸ��� ���� �𸣰����� �ϳ��� �ᵵ �� �� ��(���� �ڵ� ��õ)
        rigid.velocity = new Vector2(newPos.x, newPos.y) * 2;    // ��
        Vector2 direction = transform.forward;
        var quaternion = Quaternion.Euler(0, 1, 0);
        Vector2 newDirection = quaternion * direction; // 3. 


        float angle = Mathf.Atan2(-newPos.x, -newPos.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);

    }

    public void OnCollisionEnter2D(UnityEngine.Collision2D collision) // 2. �̷� �Ѿ� ���� ������Ʈ�� ��Ƽ� �з����� �� �Ǵϱ�(�ǵ��ߴٸ� ������) isTrigger�� �� �ְ� OnTriggerEnter2D �� ����
    {
        if (collision.collider.gameObject.CompareTag("Tiles") || collision.collider.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

    
}
