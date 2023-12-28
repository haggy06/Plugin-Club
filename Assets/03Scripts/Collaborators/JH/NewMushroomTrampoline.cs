using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMushroomTrampoline : MonoBehaviour
{
    [Range(1, 20), SerializeField] private float bouncingPower = 7.5f;
    private Animator animator;
    private void Awake()
    {
        animator = gameObject.GetComponentInParent<Animator>();
    }

    private Vector2 bouncingVec = Vector2.zero;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigid2D))
        {
            bouncingVec.x = rigid2D.velocity.x;
            bouncingVec.y = Mathf.Clamp(bouncingPower * rigid2D.gravityScale, 3f, float.PositiveInfinity);

            rigid2D.velocity = bouncingVec;

            animator.SetTrigger("Bounce");
        }
    }
}
