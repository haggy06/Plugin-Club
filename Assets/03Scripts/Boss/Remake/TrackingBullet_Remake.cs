using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingBullet_Remake : MonoBehaviour
{
     Transform target;

    [SerializeField]
    private float speed;

    private void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;

        speed = UnityEngine.Random.Range(2f, 8f);

        StartCoroutine("AutoDestroy");
    }

    private Vector3 dircV = Vector3.zero;
    void FixedUpdate()
    {
        dircV = (target.position - transform.position).normalized;
        transform.position += dircV * speed * Time.fixedDeltaTime;
        transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(dircV.x, dircV.y) * Mathf.Rad2Deg - 90, Vector3.forward);

        /*
        Vector2 dircV = target.transform.position - this.transform.position;
        Vector2 NextV = dircV.normalized * speed * Time.fixedDeltaTime;
        transform.Translate(NextV);
        */
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

    private IEnumerator AutoDestroy()
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
