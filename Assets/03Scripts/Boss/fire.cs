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
        // 0.5�� �������� �Ѿ��� �߻� �� �� ����
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Fire();
        }
    }

    void Fire()
    {
        // �Ѿ� ������ ����
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        // �Ѿ� �߻�
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

        // 2�� �ڿ� �ı�
        Destroy(bullet, 2.0f);
    }
}
