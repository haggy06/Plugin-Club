using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetStatus : MonoBehaviour
{
    [SerializeField]
    private Sprite clearImg;
    [SerializeField]
    private Sprite gameoverImg;

    private void Awake()
    {
        if (GameManager.Inst.GameResult == GameResult.Gameover)
        {
            GetComponent<SpriteRenderer>().sprite = gameoverImg;
        }
        else if (GameManager.Inst.GameResult == GameResult.Clear)
        {
            GetComponent<SpriteRenderer>().sprite = clearImg;
        }
    }
}
