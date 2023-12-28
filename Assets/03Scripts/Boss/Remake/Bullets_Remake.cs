using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets_Remake : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 5f;
    private void Awake()
    {
        transform.eulerAngles = new Vector3(0, 0, Random.Range(120f, 240f));

        StartCoroutine("Movement");
    }

    private void Update()
    {
        transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Movement()
    {
        yield return YieldInstructionCache.WaitForSeconds(3f);

        if (gameObject != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("오브젝트가 이미 파괴되었습니다");
        }
    }
}
