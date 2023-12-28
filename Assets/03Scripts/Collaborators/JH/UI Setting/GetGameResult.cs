using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
public class GetGameResult : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<TextMeshProUGUI>().text = GameManager.Inst.GameResult.ToString();
    }
}
