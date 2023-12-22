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

    private void OnEnable() // 이 스크립트가 활성화될 때마다 실행됨.
    {
        StartCoroutine(AutoDestroyBullet(autoDestroyTIme)); // 자동 삭제 코루틴 실행

        GetComponent<Rigidbody2D>().velocity = transform.right * -1f * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("당했다!");

            StopAllCoroutines(); // 자동삭제 코루틴 중지
            DestroyBullet();
        }
        else if(collision.CompareTag("Ground"))
        {
            StopAllCoroutines(); // 자동삭제 코루틴 중지
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        GetComponentInParent<PlantMonster_Remake>().bulletStack.Push(gameObject);

        gameObject.SetActive(false);
    }

    private IEnumerator AutoDestroyBullet(float destroyTime) // 자동 삭제 코루틴
    {
        yield return new WaitForSeconds(destroyTime);

        DestroyBullet();
    }
}
