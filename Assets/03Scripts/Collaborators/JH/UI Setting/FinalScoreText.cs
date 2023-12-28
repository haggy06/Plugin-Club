using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class FinalScoreText : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<TextMeshProUGUI>().text = "Final Score : " + GameManager.Inst.PlayerScore;
    }
}
