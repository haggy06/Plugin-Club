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

    [SerializeField] private LayerMask groundLayer; //  �� ���̾� ����

    // Start�� �� ���� �����
    void Start()
    {
        //�ڵ����� ������ ���� ����
        default_direction.x = Random.Range(-1.0f, 1.0f);
        default_direction.y = Random.Range(-1.0f, 1.0f);
        //���ӵ� ����
        accelaration = 0.1f;       // 1. ���� ������ �ƴ� ��� ������ ���� ������ �� �ʱ�ȭ ������ �־��ִ� �� ����.
        default_velocity = 0.1f;
    }

    // Update�� �����Ӹ��� �ݺ��ؼ� �����
    void Update()
    {
        // Player�� ���� ��ġ�� �޾ƿ��� ������Ʈ
        target = GameObject.Find("Player").transform; // 2 - 1. GameObject.Find()�� Hierachy �信 �ִ� ��� ������Ʈ�� �˻��ϴ� �Լ��� Update()������ ������ �ʴ� �� ������.
        //Player�� dinosaur ������ �Ÿ� ���                      2 - 2. Transform ���� �� �̷��� �޾ƿ��� �ش� Transform�� �ּҰ��� ����Ǳ� ������ �� ���� �����͵� �ǽð����� ���� ���ŵ�.
        float distance = Vector3.Distance(target.position, transform.position); // 3. ������ �Լ� ������ �����ϸ� �Լ��� �� �� ȣ��� ������ �������� �����Ǳ� ������ �ۿ� ������ ����� ���� ��õ��.

        if (distance <= 10.0f)
        {
            MoveToTarget();
            //���� �Ÿ��� ���� Player���� �ٰ���
        }
        else
        {
            velocity = 0.0f;
            this.transform.position = new Vector3(transform.position.x + (default_direction.x * default_velocity), // 4. this.transform�� transform�� ���� �ǹ��̹Ƿ� ��� ������.
                                               transform.position.y + (default_direction.y * default_velocity),
                                               transform.position.z); // 5. ������ Z���� 0���� �����̱� ������ Vector2�� (x, y)���� ������ �൵ ������(�ڵ����� z = 0 ��).
        }
    }
    public void MoveToTarget()
    {
        direction = (target.position - transform.position).normalized;
        //Player�� ��ġ�� dinosaur�� ��ġ�� ���� ���� ����ȭ ��
        velocity = (velocity + accelaration * Time.deltaTime);
        //�ʰ� �ƴ� �� ���������� ���ӵ� ����Ͽ� ���� ����ȭ ��
        this.transform.position = new Vector3(transform.position.x + (direction.x * velocity),
                                           transform.position.y + (direction.y * velocity),
                                              transform.position.z);
    }
}