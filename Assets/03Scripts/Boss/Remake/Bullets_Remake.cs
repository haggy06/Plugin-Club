using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets_Remake : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 5f;
    private void Awake()
    {
        transform.eulerAngles = new Vector3(0, 0, Random.Range(120f, 240f));
    }

    private void Update()
    {
        transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
