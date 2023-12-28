using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndPopupScript : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Inst.SetGameEndPopup(gameObject);
    }
}
