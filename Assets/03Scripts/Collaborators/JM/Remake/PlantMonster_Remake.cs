using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantMonster_Remake : EnemyBase
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
    private Transform spitPos;

    [SerializeField]
    private float cooltime = 2f;

    private Animator anim;
    private new void Awake()
    {
        base.Awake();

        anim = GetComponent<Animator>();
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

            if (isCoolDowned)
            {
                anim.SetTrigger("Attack");

                StartCoroutine("CoolDown");
            }
        }
    }

    [SerializeField]
    private float plantDir = -1f;
    public void Spit()
    {
        obj = Instantiate(bullet); // �� �Ѿ��� ����

        obj.transform.position = spitPos.position;
        obj.transform.rotation = transform.rotation;
        obj.GetComponent<PlantBullet_Remake>().SetDir(plantDir);
    }


    private IEnumerator CoolDown()
    {
        isCoolDowned = false;

        yield return new WaitForSeconds(cooltime); // coolTime�� ��ٸ�.

        isCoolDowned = true;
    }
}
