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
    private LayerMask groundLayer; //  �� ���̾� ����

    // Start�� �� ���� �����
    void Start()
    {
        //�ڵ����� ������ ���� ����
        default_direction.x = Random.Range(-1.0f, 1.0f);
        default_direction.y = Random.Range(-1.0f, 1.0f);

        // Player�� ���� ��ġ�� �޾ƿ��� ������Ʈ
        target = GameObject.Find("Player").transform;
    }

    // Update�� �����Ӹ��� �ݺ��ؼ� �����
    void Update()
    {
        //Player�� dinosaur ������ �Ÿ� ���
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= 10.0f)
        {
            MoveToTarget();
            //���� �Ÿ��� ���� Player���� �ٰ���
        }
        else
        {
            velocity = 0.0f;
            transform.position = new Vector2(transform.position.x + default_direction.x, transform.position.y + default_direction.y) * default_velocity;
        }
    }
    public void MoveToTarget()
    {
        //Player�� ��ġ�� dinosaur�� ��ġ�� ���� ���� ����ȭ ��
        direction = (target.position - transform.position).normalized;

        //�ʰ� �ƴ� �� ���������� ���ӵ� ����Ͽ� ���� ����ȭ ��
        velocity = (velocity + accelaration * Time.deltaTime);

        transform.position = new Vector2(transform.position.x + direction.x, transform.position.y + direction.y) * velocity;
    }
}
