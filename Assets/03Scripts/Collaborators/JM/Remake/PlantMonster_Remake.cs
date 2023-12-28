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

    private bool isCoolDowned = true; // 쿨타임 돌았는지 체크하는 변수
    private GameObject obj; // 발사한 총알을 임시로 저장할 함수
    void FixedUpdate()
    {
        Debug.DrawLine(transform.position, transform.right * -1);
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right * -1, distance, isLayer);
        if (raycast.collider != null)
        {
            Debug.Log("플레이어 감지");

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
        obj = Instantiate(bullet); // 새 총알을 생성

        obj.transform.position = spitPos.position;
        obj.transform.rotation = transform.rotation;
        obj.GetComponent<PlantBullet_Remake>().SetDir(plantDir);
    }


    private IEnumerator CoolDown()
    {
        isCoolDowned = false;

        yield return new WaitForSeconds(cooltime); // coolTime초 기다림.

        isCoolDowned = true;
    }
}
