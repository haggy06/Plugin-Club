using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using static UnityEngine.GraphicsBuffer;

public class Egg : MonoBehaviour
{
    Rigidbody2D rigid;
    CapsuleCollider2D capsule;
    SpriteRenderer spriteRenderer;

    [SerializeField]
    private GameObject trackingFeather;

    public float HP = 5f;
    Transform target;
    public float speed = 10f;
    public Sprite feath;
    
    bool a;

    void Awake()
    {
        capsule = GetComponent<CapsuleCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        //speed = 10;
        //HP = 5;
        Invoke("Hatch", 3);
    
        //feath = Resources.Load<Sprite>("Feather_3");
    }

    void FixedUpdate()
    {
        if (a)
        {
            Vector3 direction = target.position - transform.position;
            direction.Normalize();

            transform.position += direction * speed * Time.deltaTime;

            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 270, Vector3.forward);
        }

    }

    IEnumerator Movement()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
    void Hatch()
    {
        rigid.gravityScale = 0;

        Instantiate(trackingFeather, new Vector2(transform.position.x, transform.position.y + 1.5f), Quaternion.identity);
        Destroy(gameObject);
        /*
        spriteRenderer.sprite = feath;
        a = true;
        StartCoroutine(Movement());
         * */

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
