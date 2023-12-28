using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBullet_Remake : EnemyBase
{
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float autoDestroyTIme= 3f;

    public  void SetDir(float dir) // �� ��ũ��Ʈ�� Ȱ��ȭ�� ������ �����.
    {
        StartCoroutine(AutoDestroyBullet(autoDestroyTIme)); // �ڵ� ���� �ڷ�ƾ ����

        GetComponent<Rigidbody2D>().velocity = new Vector2(speed * dir, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("���ߴ�!");

            StopAllCoroutines(); // �ڵ����� �ڷ�ƾ ����
            DestroyBullet();
        }
        else if(collision.CompareTag("Ground"))
        {
            StopAllCoroutines(); // �ڵ����� �ڷ�ƾ ����
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private IEnumerator AutoDestroyBullet(float destroyTime) // �ڵ� ���� �ڷ�ƾ
    {
        yield return new WaitForSeconds(destroyTime);

        DestroyBullet();
    }
}
