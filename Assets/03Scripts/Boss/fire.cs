using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFiring : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 20f;
    public float fireRate = 0.5f;
    private float nextFire;

    void Update()
    {
        // 0.5초 간격으로 총알을 발사 할 수 있음
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Fire();
        }
    }

    void Fire()
    {
        // 총알 프리팹 생성
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        // 총알 발사
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

        // 2초 뒤에 파괴
        Destroy(bullet, 2.0f);
    }
}
