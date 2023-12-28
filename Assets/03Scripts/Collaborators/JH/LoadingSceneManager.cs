using System.Collections;
using UnityEngine;

using UnityEngine.SceneManagement;
public class LoadingSceneManager : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("·Îµù ¾À ÁøÀÔ");

        FadeIn_Out.Inst.FadeImgHide();
        StartCoroutine("LoadSceneAsync");
        //Invoke("LoadNextScene", 1f);
    }

    private IEnumerator LoadSceneAsync()
    {
        yield return YieldInstructionCache.WaitForSeconds(0.1f);

        AsyncOperation asyncScene = SceneManager.LoadSceneAsync(PlayerPrefs.GetString(TempData.LoadSceneName.ToString()));
        asyncScene.allowSceneActivation = false;

        yield return YieldInstructionCache.WaitForSeconds(1.5f);
        asyncScene.allowSceneActivation = true;
    }
}
