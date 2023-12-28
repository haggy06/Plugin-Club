using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    private float score = 5f;
    [SerializeField]
    protected float hp = 20f;
    [SerializeField]
    protected float damage = 1f;
    public float Damage => damage;

    protected SpriteRenderer sprite;
    protected void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void GetDamage(float damage)
    {
        hp -= damage;

        StartCoroutine("DamageAnim");

        if (hp <= 0)
        {
            Debug.Log(gameObject.name + " »ç¸Á");

            GameManager.Inst.GetScored(score);

            EnemyDead();
        }
    }

    protected virtual void EnemyDead()
    {
        Destroy(gameObject);
    }

    protected IEnumerator DamageAnim()
    {
        sprite.color = Color.red;

        yield return YieldInstructionCache.WaitForSeconds(0.05f);

        sprite.color = Color.white;
    }
}
