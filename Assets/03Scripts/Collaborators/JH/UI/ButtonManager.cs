using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public void SceneMove(string sceneName)
    {
        StartCoroutine(FadeIn_Out.Inst.FadeIn(sceneName));
        FadeIn_Out.Inst.FadeTime = 1f;
        Audio.Inst.PlaySfx(Audio.Sfx.Start);
    }
}
