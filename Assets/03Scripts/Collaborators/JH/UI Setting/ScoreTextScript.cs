using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class ScoreTextScript : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Inst.SetScoreText(GetComponent<TextMeshProUGUI>());
    }
}
