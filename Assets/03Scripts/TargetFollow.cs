using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    void Update()
    {
        transform.position = target.position;
    }
}
