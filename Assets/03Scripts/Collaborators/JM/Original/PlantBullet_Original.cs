using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBullet_Original : MonoBehaviour
{
    public float speed;
    public float distance;
    public LayerMask isLayer;

    void Start()
    {
        Invoke("DestroyBullet", 2f);
    }


    void Update()
    {

        RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right * -1, distance, isLayer); // 1. �Ѿ˿� �¾��� ���� ���ϰ� ���� �Ŵϱ� OnTriggerEnter2D()�� �� ���� �� ����.
        if (raycast.collider != null)
        {
            if (raycast.collider.tag == "Player")
            {
                Debug.Log("���ߴ�!");
            }
            DestroyBullet();
        }

        transform.Translate(transform.right * -1f * speed * Time.deltaTime); // 2. Rigidbody2D�� velocity�� �����ؼ� ������ �͵� ���� �����.

    }

    void DestroyBullet()
    {
        Debug.Log("�Ѿ� ����");
        Destroy(gameObject); // 3. ������Ʈ Ǯ���� �� ��� �����ؾ� �� �ڵ���.
    }
}