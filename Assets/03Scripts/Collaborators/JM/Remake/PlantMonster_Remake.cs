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

    public Stack<GameObject> bulletStack = new Stack<GameObject>(); // �����ϰ� ���� ������Ʈ Ǯ
    private void Awake()
    {
        for (int i = 0; i < 20; i++) // ������Ʈ�� ����� bulletStack�� �׾� �δ� �Լ�(20�� ����).
        {
            bulletStack.Push(Instantiate(bullet, transform)); // bullet�� transform�� �ڽ����� ������ bulletStack�� ����
        }
    }

    private bool isCoolDowned = true; // ��Ÿ�� ���Ҵ��� üũ�ϴ� ����
    private GameObject obj; // �߻��� �Ѿ��� �ӽ÷� ������ �Լ�
    void FixedUpdate()
    {
        Debug.DrawLine(transform.position, transform.right * -1);
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right * -1, distance, isLayer);
        if (raycast.collider != null)
        {
            Debug.Log("�÷��̾� ����");

            if (Vector2.Distance(transform.position, raycast.collider.transform.position) < atkDistance)
            {
                if (isCoolDowned)
                {
                    if (bulletStack.TryPop(out obj)) // ���ÿ� ������ �� ���� ��� ������.
                    {
                        obj.transform.position = pos.position;
                        obj.transform.rotation = transform.rotation;

                        obj.SetActive(true); // ��Ȱ��ȭ �Ǿ� �־��� �Ѿ��� Ȱ��ȭ������.
                    }
                    else // ������ ������� ���
                    {
                        obj = Instantiate(bullet, transform); // �� �Ѿ��� ����

                        obj.transform.position = pos.position;
                        obj.transform.rotation = transform.rotation;

                        obj.SetActive(true); // ��Ȱ��ȭ �Ǿ� �־��� �Ѿ��� Ȱ��ȭ������.
                    }

                    StartCoroutine("CoolDown"); // " StartCoroutine(CoolDown()); "�� �۵���.
                }
            }
        }
    }

    private IEnumerator CoolDown()
    {
        isCoolDowned = false;

        yield return new WaitForSeconds(cooltime); // coolTime�� ��ٸ�.

        isCoolDowned = true;
    }
}
