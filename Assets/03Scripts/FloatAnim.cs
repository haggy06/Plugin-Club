using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatAnim : MonoBehaviour
{
    [SerializeField]
    private bool SetRandomTerm = true;

    [Space(5)]

    [SerializeField]
    private Vector2 moveGap = Vector2.up;
    [SerializeField]
    private float moveSpeed  = 1f;

    private Vector2 startVec;
    private void Awake()
    {
        startVec = transform.position;
        Debug.Log("Awake");
        if (SetRandomTerm)
        {
            Invoke("MoveStart", Random.Range(0f, 0.5f));
            Debug.Log("Move Start");
        }
        else
        {
            StartCoroutine("MoveObj");
        }
    }
    private void MoveStart()
    {

        StartCoroutine("MoveObj");
    }

    private Vector2 newPos;
    private float curTime = 0f;
    private IEnumerator MoveObj()
    {
        Debug.Log("Move");
        while (true)
        {
            

            curTime += (moveSpeed * Time.deltaTime) * Mathf.PI;

            newPos.x = startVec.x + (Mathf.Sin(curTime) * moveGap.x);
            newPos.y = startVec.y + (Mathf.Sin(curTime) * moveGap.y);

            transform.position = newPos;

            if (Mathf.Approximately(curTime, 2f))
            {
                curTime = 0f;
            }

            yield return null;
        }
    }
}
