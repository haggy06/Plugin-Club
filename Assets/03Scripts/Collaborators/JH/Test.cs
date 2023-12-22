using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(LayerNumber.Default);
        Debug.Log(LayerNumber.TransparentFX);
        Debug.Log(LayerNumber.Ignore_Raycast);
        Debug.Log(LayerNumber.Ground);
        Debug.Log(LayerNumber.Water);
        Debug.Log(LayerNumber.UI);
    }
}
