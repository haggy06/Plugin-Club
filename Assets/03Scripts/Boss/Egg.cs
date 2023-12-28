using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Egg : MonoBehaviour
{
    Rigidbody2D rigid;
    CapsuleCollider2D capsule;
    SpriteRenderer spriteRenderer;
    public float HP;
    Transform target;
    public float speed;
    bool a;

    void Awake()
    {
        capsule = GetComponent<CapsuleCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        speed = 10;
        HP = 5;
        Invoke("Hatch", 3);
    }

    void FixedUpdate()
    {
        if (a)
        {
            Vector3 direction = target.position - transform.position;
            direction.Normalize();

            transform.position += direction * speed * Time.deltaTime;

            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
        
    }

    IEnumerator Movement()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
    void Hatch()
    {
        
        a = true;
        StartCoroutine(Movement());
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
