using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantMonster_Remake : MonoBehaviour
{
    [SerializeField]
    private float distance = 5f;
    [SerializeField]
    private float atkDistance = 5f;

    [SerializeField]
    private LayerMask isLayer;

    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform pos;

    [SerializeField]
    private float cooltime = 2f;

    public Stack<GameObject> bulletStack = new Stack<GameObject>(); // 간단하게 만든 오브젝트 풀
    private void Awake()
    {
        for (int i = 0; i < 20; i++) // 오브젝트를 만들어 bulletStack에 쌓아 두는 함수(20개 생성).
        {
            bulletStack.Push(Instantiate(bullet, transform)); // bullet을 transform의 자식으로 생성해 bulletStack에 저장
        }
    }

    private bool isCoolDowned = true; // 쿨타임 돌았는지 체크하는 변수
    private GameObject obj; // 발사한 총알을 임시로 저장할 함수
    void FixedUpdate()
    {
        Debug.DrawLine(transform.position, transform.right * -1);
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right * -1, distance, isLayer);
        if (raycast.collider != null)
        {
            Debug.Log("플레이어 감지");

            if (Vector2.Distance(transform.position, raycast.collider.transform.position) < atkDistance)
            {
                if (isCoolDowned)
                {
                    if (bulletStack.TryPop(out obj)) // 스택에 가져올 게 있을 경우 가져옴.
                    {
                        obj.transform.position = pos.position;
                        obj.transform.rotation = transform.rotation;

                        obj.SetActive(true); // 비활성화 되어 있었을 총알을 활성화시켜줌.
                    }
                    else // 스택이 비어있을 경우
                    {
                        obj = Instantiate(bullet, transform); // 새 총알을 생성

                        obj.transform.position = pos.position;
                        obj.transform.rotation = transform.rotation;

                        obj.SetActive(true); // 비활성화 되어 있었을 총알을 활성화시켜줌.
                    }

                    StartCoroutine("CoolDown"); // " StartCoroutine(CoolDown()); "도 작동함.
                }
            }
        }
    }

    private IEnumerator CoolDown()
    {
        isCoolDowned = false;

        yield return new WaitForSeconds(cooltime); // coolTime초 기다림.

        isCoolDowned = true;
    }
}
