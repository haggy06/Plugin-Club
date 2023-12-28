using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Original : MonoBehaviour
{
    public Transform target;
    public Vector3 direction;
    public float velocity;
    public float default_velocity;
    public float accelaration;
    public Vector3 default_direction;

    [SerializeField] private LayerMask groundLayer; //  땅 레이어 지정

    // Start는 한 번만 실행됨
    void Start()
    {
        //자동으로 움직일 방향 벡터
        default_direction.x = Random.Range(-1.0f, 1.0f);
        default_direction.y = Random.Range(-1.0f, 1.0f);
        //가속도 지정
        accelaration = 0.1f;       // 1. 난수 지정이 아닌 상수 지정은 변수 선언할 때 초기화 값으로 넣어주는 게 좋음.
        default_velocity = 0.1f;
    }

    // Update는 프레임마다 반복해서 실행됨
    void Update()
    {
        // Player의 현재 위치를 받아오는 오브젝트
        target = GameObject.Find("Player").transform; // 2 - 1. GameObject.Find()는 Hierachy 뷰에 있는 모든 오브젝트를 검사하는 함수라 Update()문에선 돌리지 않는 걸 권장함.
        //Player과 dinosaur 사이의 거리 계산                      2 - 2. Transform 같은 건 이렇게 받아오면 해당 Transform의 주소값이 복사되기 때문에 한 번만 가져와도 실시간으로 값이 갱신됨.
        float distance = Vector3.Distance(target.position, transform.position); // 3. 변수를 함수 내에서 생성하면 함수가 한 번 호출될 때마다 가비지가 생성되기 때문에 밖에 변수를 만드는 것을 추천함.

        if (distance <= 10.0f)
        {
            MoveToTarget();
            //일정 거리에 있음 Player에게 다가옴
        }
        else
        {
            velocity = 0.0f;
            this.transform.position = new Vector3(transform.position.x + (default_direction.x * default_velocity), // 4. this.transform과 transform은 같은 의미이므로 축약 가능함.
                                               transform.position.y + (default_direction.y * default_velocity),
                                               transform.position.z); // 5. 어차피 Z값은 0으로 고정이기 때문에 Vector2로 (x, y)값만 지정해 줘도 괜찮음(자동으로 z = 0 들어감).
        }
    }
    public void MoveToTarget()
    {
        direction = (target.position - transform.position).normalized;
        //Player의 위치와 dinosaur의 위치를 빼고 단위 백터화 함
        velocity = (velocity + accelaration * Time.deltaTime);
        //초가 아닌 한 츠레임으로 가속도 계산하여 단위 벡터화 함
        this.transform.position = new Vector3(transform.position.x + (direction.x * velocity),
                                           transform.position.y + (direction.y * velocity),
                                              transform.position.z);
    }
}