using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private float playerMaxHP = 30f;
    [SerializeField]
    private float playerCurHP = 0f;
    [Space(5), SerializeField]
    private float playerCurScore;
    public float PlayerScore => playerCurScore;

    private string gameResult = "None";
    public string GameResult => gameResult;

    private new void Awake()
    {
        base.Awake();

        playerCurHP = playerMaxHP;
    }

    private Image hpBar;
    public void SetHPBar(Image newHpBar)
    {
        hpBar = newHpBar;
        hpBar.fillAmount = playerCurHP / playerMaxHP;
    }

    private TextMeshProUGUI hpText;
    public void SetHPText(TextMeshProUGUI newText)
    {
        hpText = newText;
        hpText.text = playerCurHP.ToString();
    }

    private TextMeshProUGUI scoreText;
    public void SetScoreText(TextMeshProUGUI newText)
    {
        scoreText = newText;
        scoreText.text = playerCurScore.ToString();
    }

    private GameObject gameEndPopup;
    public void SetGameEndPopup(GameObject obj)
    {
        gameEndPopup = obj;
        gameEndPopup.SetActive(false);
    }

    public void GetDamaged(float damage)
    {
        playerCurHP -= damage;

        Mathf.Round(playerCurHP);

        hpBar.fillAmount = playerCurHP / playerMaxHP;

        if (playerCurHP <= 0f)
        {
            hpText.text = gameResult = "Game Over";

            GameObject.FindWithTag("Player").GetComponent<PlayerController_V5>().PlayerDead();

            gameEndPopup.SetActive(true);
        }
        else
        {
            hpText.text = playerCurHP.ToString();
        }
    }

    public void Kill_Boss()
    {
        gameResult = "Game Clear";

        gameEndPopup.SetActive(true);
    }

    public void GetScored(float score)
    {
        playerCurScore += score;

        Mathf.Round(playerCurScore);

        scoreText.text = playerCurScore.ToString();
    }
}
