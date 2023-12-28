using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    Rigidbody2D rigid;
    BoxCollider2D collision;
    private Vector2 newPos;
    public Rigidbody2D target;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        newPos = new Vector2(UnityEngine.Random.Range(-8f, 8f), -3f);
        GetComponent<Rigidbody2D>().velocity = newPos * 2f;
        rigid.velocity = new Vector2(newPos.x, newPos.y) * 2;
        Vector2 direction = transform.forward;
        var quaternion = Quaternion.Euler(0, 1, 0);
        Vector2 newDirection = quaternion * direction;


        float angle = Mathf.Atan2(-newPos.x, -newPos.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(-angle - 180, Vector3.forward);

    }

    public void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Tiles") || collision.collider.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }


}
