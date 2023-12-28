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
        set => fadeIn_OutTime = value;
        get => fadeIn_OutTime;
    }

    [SerializeField]
    private Image fadeImage;

    public void FadeImgHide()
    {
        fadeImage.color = Color.clear;
        Audio.instance.PlayBgm(true);
    }
    
    private new void Awake()
    {
        base.Awake();
        StartCoroutine("FadeOut");
    }
    private void OnLevelWasLoaded(int level)
    {
        Debug.Log(level);
        if (level != (int)SceneName.LoadingScene)
        {
            fadeImage.color = Color.black;
            StartCoroutine("FadeOut");
        }
    }
    #region Fade Coroutines
    private Color alpha;
    private float currentTime, percent;
    private IEnumerator FadeOut()
    {
        Debug.Log("Fadeout");
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
        Debug.Log("Fadein");
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

        PlayerPrefs.SetString(TempData.LoadSceneName.ToString(), nextSceneName);
        SceneManager.LoadScene(SceneName.LoadingScene.ToString());
    }
    #endregion
}
