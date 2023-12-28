using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBullet_Remake : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float autoDestroyTIme= 3f;

    void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable() // �� ��ũ��Ʈ�� Ȱ��ȭ�� ������ �����.
    {
        StartCoroutine(AutoDestroyBullet(autoDestroyTIme)); // �ڵ� ���� �ڷ�ƾ ����

        GetComponent<Rigidbody2D>().velocity = transform.right * -1f * speed;
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
        GetComponentInParent<PlantMonster_Remake>().bulletStack.Push(gameObject);

        gameObject.SetActive(false);
    }

    private IEnumerator AutoDestroyBullet(float destroyTime) // �ڵ� ���� �ڷ�ƾ
    {
        yield return new WaitForSeconds(destroyTime);

        DestroyBullet();
    }
}
