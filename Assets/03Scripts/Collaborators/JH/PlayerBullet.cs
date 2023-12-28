using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private LayerMask enemyLayer;

    private float damage;

    private Rigidbody2D rigid2D;
    private PlayerController_V5 player;
    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player_Version 5").GetComponent<PlayerController_V5>();
    }

    private void OnEnable()
    {
        StartCoroutine("AutoDestroy");
    }
    public void InitInfo(Vector2 fireDir, float bulletSpeed, float myDamage)
    {
        rigid2D.velocity = fireDir * bulletSpeed;

        damage = myDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("���� �ǰ�");

        if (collision.gameObject.layer == 3)
        {
            Debug.Log("�� �ǰ�");

            DestroyThis();
        }
        else if (collision.gameObject.layer == 7)
        {
            Debug.Log("�� �ǰ�");

            if (collision.gameObject.TryGetComponent<EnemyBase>(out EnemyBase enemy))
            {
                enemy.GetDamage(damage);
            }

            DestroyThis();
        }
    }

    private void DestroyThis()
    {
        StopAllCoroutines();

        player.ReturnBullet(gameObject);
    }
    private IEnumerator AutoDestroy()
    {
        yield return YieldInstructionCache.WaitForSeconds(3f);

        DestroyThis();
    }
}
