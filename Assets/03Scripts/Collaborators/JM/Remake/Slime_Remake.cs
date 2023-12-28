using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Remake : MonoBehaviour
{
    private Transform target;

    private float velocity;

    private Vector3 direction;
    private Vector3 default_direction;

    [SerializeField]
    private float default_velocity = 0.1f;
    [SerializeField]
    private float accelaration = 0.1f;

    [SerializeField]
    private LayerMask groundLayer; //  땅 레이어 지정

    // Start는 한 번만 실행됨
    void Start()
    {
        //자동으로 움직일 방향 벡터
        default_direction.x = Random.Range(-1.0f, 1.0f);
        default_direction.y = Random.Range(-1.0f, 1.0f);

        // Player의 현재 위치를 받아오는 오브젝트
        target = GameObject.Find("Player").transform;
    }

    // Update는 프레임마다 반복해서 실행됨
    void Update()
    {
        //Player과 dinosaur 사이의 거리 계산
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= 10.0f)
        {
            MoveToTarget();
            //일정 거리에 있음 Player에게 다가옴
        }
        else
        {
            velocity = 0.0f;
            transform.position = new Vector2(transform.position.x + default_direction.x, transform.position.y + default_direction.y) * default_velocity;
        }
    }
    public void MoveToTarget()
    {
        //Player의 위치와 dinosaur의 위치를 빼고 단위 백터화 함
        direction = (target.position - transform.position).normalized;

        //초가 아닌 한 츠레임으로 가속도 계산하여 단위 벡터화 함
        velocity = (velocity + accelaration * Time.deltaTime);

        transform.position = new Vector2(transform.position.x + direction.x, transform.position.y + direction.y) * velocity;
    }
}
