using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeIn_Out : Singleton<FadeIn_Out>
{
    [SerializeField]
    private float fadeIn_OutTime = 1f;
    public float FadeTime
    {
        set => value = fadeIn_OutTime;
        get => fadeIn_OutTime;
    }

    [SerializeField]
    private Image fadeImage;
    
    private void Start()
    {
        StartCoroutine("FadeOut");
    }
    private void OnLevelWasLoaded(int level)
    {
        Debug.Log(level);
        StartCoroutine("FadeOut");
    }
    #region Fade Coroutines
    private Color alpha;
    private float currentTime, percent;
    private IEnumerator FadeOut()
    {
        alpha = fadeImage.color;
        currentTime = 0f;
        percent = 0f;

        fadeImage.enabled = true;
        while (fadeImage.color.a > 0f)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeIn_OutTime;

            alpha.a = Mathf.Lerp(1f, 0f, percent);
            fadeImage.color = alpha;

            yield return null;
        }
        fadeImage.raycastTarget = false;
    }
    public IEnumerator FadeIn(string nextSceneName)
    {
        Debug.Log("페이드 실행");
        alpha = fadeImage.color;
        currentTime = 0f;
        percent = 0f;

        fadeImage.raycastTarget = true;
        while (fadeImage.color.a < 1f)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeIn_OutTime;

            alpha.a = Mathf.Lerp(0f, 1f, percent);
            fadeImage.color = alpha;

            yield return null;
        }

        
        SceneManager.LoadScene(SceneName.LoadingScene.ToString());
    }
    #endregion
}
