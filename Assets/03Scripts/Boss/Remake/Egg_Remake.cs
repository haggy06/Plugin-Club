using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg_Remake : EnemyBase
{
    Transform target;

    [SerializeField]
    private GameObject spawnObj;

    [SerializeField]
    private float spawnObjSpeed = 10f;

    [SerializeField]
    private float spawnObjLifeTime = 1.5f;



    new void Awake()
    {
        base.Awake();

        target = GameObject.FindWithTag("Player").transform;

        Invoke("Hatch", 3);
    }
    void Hatch()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().isTrigger = true;

        TrackingBullet_Remake obj = Instantiate(spawnObj, new Vector2(transform.position.x, transform.position.y + 0.55f), Quaternion.identity).GetComponent<TrackingBullet_Remake>();
        obj.speed = spawnObjSpeed;
        obj.AutoDestroyTime = spawnObjLifeTime;

        Destroy(gameObject);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}