using UnityEngine;

using TMPro;

public class HPTextScript : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Inst.SetHPText(GetComponent<TextMeshProUGUI>());
    }
}