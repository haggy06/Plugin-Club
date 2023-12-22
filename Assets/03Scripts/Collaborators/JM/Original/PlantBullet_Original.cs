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

        RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right * -1, distance, isLayer); // 1. 총알에 맞았을 때를 구하고 싶은 거니까 OnTriggerEnter2D()가 더 나을 것 같음.
        if (raycast.collider != null)
        {
            if (raycast.collider.tag == "Player")
            {
                Debug.Log("당했다!");
            }
            DestroyBullet();
        }

        transform.Translate(transform.right * -1f * speed * Time.deltaTime); // 2. Rigidbody2D의 velocity를 수정해서 날리는 것도 좋은 방법임.

    }

    void DestroyBullet()
    {
        Debug.Log("총알 삭제");
        Destroy(gameObject); // 3. 오브젝트 풀링을 할 경우 변경해야 할 코드임.
    }
}