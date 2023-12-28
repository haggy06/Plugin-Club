using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMovePortal : MonoBehaviour
{
    [SerializeField]
    private string toMoveSceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(FadeIn_Out.Inst.FadeIn(toMoveSceneName));
            Audio.Inst.PlaySfx(Audio.Sfx.Portal);

            collision.GetComponent<PlayerController_V5>().EnterPortal();
        }
    }
}
