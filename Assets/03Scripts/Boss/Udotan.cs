using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Udotan : MonoBehaviour
{
    CapsuleCollider2D capsule;
    Rigidbody2D rigid;
    GameObject target;
    SpriteRenderer spriter;
    public float speed;
    bool isLive;
    public float GameTime;
    private void Update()
    {
        //+= 계속 더하기
        GameTime += Time.deltaTime;

        if (GameTime > 3)
        {
            Destroy(this.gameObject);
        }
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player");
        capsule = GetComponent<CapsuleCollider2D>();
        speed = UnityEngine.Random.Range(2f, 8f);
    }
    void FixedUpdate()
    {
        
        Vector2 dircV = target.transform.position - this.transform.position;
        Vector2 NextV = dircV.normalized * speed * Time.fixedDeltaTime;
        transform.Translate(NextV);
        rigid.velocity = Vector2.zero;
    }

        public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Tiles") || collision.collider.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
        
    }
}
