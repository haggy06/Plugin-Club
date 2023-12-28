using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Inst.StatReset();
    }
}
