using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitCheck : MonoBehaviour
{
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public bool invincible = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!invincible && collision.CompareTag("Enemy"))
        {
            GameManager.Inst.GetDamaged(collision.GetComponent<EnemyBase>().Damage);
            Audio.Inst.PlaySfx(Audio.Sfx.Crack);

            StartCoroutine("Hit_CoolTime");
        }
    }

    private IEnumerator Hit_CoolTime()
    {
        invincible = true;
        StartCoroutine("Hit_Blink");

        yield return YieldInstructionCache.WaitForSeconds(0.75f);

        invincible = false;
    }

    private IEnumerator Hit_Blink()
    {
        while (invincible)
        {
            sprite.color = Color.gray;

            yield return YieldInstructionCache.WaitForSeconds(0.125f);

            sprite.color = Color.white;

            yield return YieldInstructionCache.WaitForSeconds(0.125f);
        }
    }
}
