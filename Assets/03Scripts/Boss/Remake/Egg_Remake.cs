using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg_Remake : EnemyBase
{
    [Space(5), SerializeField, Space(5)]
    private GameObject spawningObj;

    [SerializeField]
    private float speed = 10f;

    new void Awake()
    {
        base.Awake();

        Invoke("Hatch", 3);
    }

    void Hatch()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().isTrigger = true;

        TrackingBullet_Remake obj = Instantiate(spawningObj, new Vector2(transform.position.x, transform.position.y + 0.55f), Quaternion.identity).GetComponent<TrackingBullet_Remake>();
        obj.speed = 10f;
        obj.autoDestroyTime = 1.5f;

        Destroy(gameObject);
    }
}