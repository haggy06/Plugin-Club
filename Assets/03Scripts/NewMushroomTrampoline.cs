using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMushroomTrampoline : MonoBehaviour
{
    [Range(1, 20), SerializeField] private float bouncingPower = 20.5f;
    //private Animator animator;
    [SerializeField] private char mushroomDirection = 'U';
    private void Awake()
    {

        //animator = gameObject.GetComponentInParent<Animator>();

        switch (transform.GetComponentInParent<Transform>().eulerAngles.z)
        {
            case 0:
                mushroomDirection = 'U';
                break;
            case 90:
                mushroomDirection = 'L';
                break;
            case 180:
                mushroomDirection = 'D';
                break;
            case 270:
                mushroomDirection = 'R';
                break;
            default:
                break;
        }
    }

    private Vector2 bouncingVec = Vector2.zero;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigid2D))
        {
            switch (mushroomDirection)
            {
                case 'U':
                    if (rigid2D.velocity.y < 0)
                    {
                        bouncingVec.x = rigid2D.velocity.x;
                        bouncingVec.y = Mathf.Clamp(bouncingPower * rigid2D.gravityScale, 3f, float.PositiveInfinity);

                        rigid2D.velocity = bouncingVec;

                        //animator.SetTrigger("Bounce");
                    }
                    break;
                case 'L':
                    if (rigid2D.velocity.x > 0)
                    {
                        bouncingVec.x = -Mathf.Clamp(bouncingPower * rigid2D.gravityScale, 5f, float.PositiveInfinity);
                        bouncingVec.y = rigid2D.velocity.y;

                        rigid2D.velocity = bouncingVec;

                        //animator.SetTrigger("Bounce");
                    }
                    break;
                case 'D':
                    if (rigid2D.velocity.y > 0)
                    {
                        bouncingVec.x = rigid2D.velocity.x;
                        bouncingVec.y = -Mathf.Clamp(bouncingPower * rigid2D.gravityScale, 3f, float.PositiveInfinity);

                        rigid2D.velocity = bouncingVec;

                        //animator.SetTrigger("Bounce");
                    }
                    break;
                case 'R':
                    if (rigid2D.velocity.x < 0)
                    {
                        bouncingVec.x = Mathf.Clamp(bouncingPower * rigid2D.gravityScale, 5f, float.PositiveInfinity);
                        bouncingVec.y = rigid2D.velocity.y;

                        rigid2D.velocity = bouncingVec;

                        //animator.SetTrigger("Bounce");
                    }
                    break;

            }
        }
    }
}
