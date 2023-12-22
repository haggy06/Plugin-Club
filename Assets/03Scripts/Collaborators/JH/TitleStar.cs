using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleStar : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        if (!TryGetComponent<Animator>(out anim))
        {
            Debug.LogError("TitleStar.cs - Awake() - anim 참조 실패");
        }
        Invoke("Glow", Random.Range(0f, 1f));
    }
    private void OnMouseEnter()
    {
        anim.SetTrigger("Twinkle");
    }

    private void Glow()
    {
        anim.SetTrigger("GlowStart");
    }
}
