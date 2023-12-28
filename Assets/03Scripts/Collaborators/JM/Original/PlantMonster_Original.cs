using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlantMonster_Original : MonoBehaviour
{
    public float distance;         // 1. 기본값 설정을 안 해주면 다른 사람이 받았을 때 값이 모두 초기화되므로 불편할 수 있음..
    public float atkDistance;    // 2. 너무 public을 남발하면 코드가 꼬일 수 있음. " [SerializeField] private ... " 사용을 권장함.
    public LayerMask isLayer;  // 3. 변수명이 직관적이지 않을 때는 " [Tooltip("설명")] " 이나 주석 등으로 설명을 써 주면 좋음(하지만 직관적인 게 best).

    public GameObject bullet;
    public Transform pos;

    void Start()
    {

    }

    public float cooltime;
    private float currenttime;

    void FixedUpdate()
    {
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right * -1, distance, isLayer);
        if (raycast.collider != null)
        {


            if (Vector2.Distance(transform.position, raycast.collider.transform.position) < atkDistance)
            {
                if (currenttime <= 0)
                {
                    GameObject bulletcopy = Instantiate(bullet, pos.position, transform.rotation); // 4 - 1.  오브젝트 생성 이후 추가 작업이 없으므로 " GameObject bulletcopy " 에 저장할 이유 X
                    currenttime = cooltime;                                                                               // 4 - 2. 이렇게 꾸준히 생성해야 하는 오브젝트는 오브젝트 풀링을 하는 게 좋음.
                }

            }
            currenttime -= Time.deltaTime; // 5 - 1. FixedUpdate()는 고정적인 주기(기본 0.02초)로 실행되는 함수기 때문에 Time.deltaTime(초당 프레임 수)은 맞지 않음.
                                                           // 5 - 2. 쿨타임 구현은 코루틴으로 하는 게 더 좋을 것 같음.
        }


    }
}