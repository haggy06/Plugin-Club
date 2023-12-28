using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlantMonster_Original : MonoBehaviour
{
    public float distance;         // 1. �⺻�� ������ �� ���ָ� �ٸ� ����� �޾��� �� ���� ��� �ʱ�ȭ�ǹǷ� ������ �� ����..
    public float atkDistance;    // 2. �ʹ� public�� �����ϸ� �ڵ尡 ���� �� ����. " [SerializeField] private ... " ����� ������.
    public LayerMask isLayer;  // 3. �������� ���������� ���� ���� " [Tooltip("����")] " �̳� �ּ� ������ ������ �� �ָ� ����(������ �������� �� best).

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
                    GameObject bulletcopy = Instantiate(bullet, pos.position, transform.rotation); // 4 - 1.  ������Ʈ ���� ���� �߰� �۾��� �����Ƿ� " GameObject bulletcopy " �� ������ ���� X
                    currenttime = cooltime;                                                                               // 4 - 2. �̷��� ������ �����ؾ� �ϴ� ������Ʈ�� ������Ʈ Ǯ���� �ϴ� �� ����.
                }

            }
            currenttime -= Time.deltaTime; // 5 - 1. FixedUpdate()�� �������� �ֱ�(�⺻ 0.02��)�� ����Ǵ� �Լ��� ������ Time.deltaTime(�ʴ� ������ ��)�� ���� ����.
                                                           // 5 - 2. ��Ÿ�� ������ �ڷ�ƾ���� �ϴ� �� �� ���� �� ����.
        }


    }
}