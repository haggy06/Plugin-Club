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
    Transform target;
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
        target = GameObject.FindWithTag("Player").transform;
        capsule = GetComponent<CapsuleCollider2D>();
        speed = UnityEngine.Random.Range(2f, 8f);
    }
    void FixedUpdate()
    {

        Vector3 direction = target.position - transform.position;
        direction.Normalize();

        transform.position += direction * speed * Time.deltaTime;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

        public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Tiles") || collision.collider.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
        
    }
}
