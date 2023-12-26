using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg_Remake : EnemyBase
{
    Transform target;

    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private Sprite awakedSprite;
    
    private bool isAwake;

    new void Awake()
    {
        base.Awake();

        target = GameObject.FindWithTag("Player").transform;

        Invoke("Hatch", 3);
    }

    Vector3 dircV = Vector3.zero;
    void FixedUpdate()
    {
        if (isAwake)
        {
            dircV = (target.position - transform.position).normalized;
            transform.position += dircV * speed * Time.fixedDeltaTime;
            transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(dircV.x, dircV.y) * Mathf.Rad2Deg - 90, Vector3.forward);

        }
    }

    IEnumerator Movement()
    {
        yield return YieldInstructionCache.WaitForSeconds(1.5f);

        if (gameObject != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("오브젝트가 이미 파괴되었습니다");
        }
    }
    void Hatch()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().isTrigger = true;

        isAwake = true;
        StartCoroutine(Movement());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}