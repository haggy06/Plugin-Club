using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class HPBarScript : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Inst.SetHPBar(GetComponent<Image>());
    }
}
