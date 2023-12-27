using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZLook : MonoBehaviour
{
    void LateUpdate()
    {
        if (transform.position.z < 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
        }
    }
}
