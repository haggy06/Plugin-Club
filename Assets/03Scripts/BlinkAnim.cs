using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkAnim : MonoBehaviour
{
    private SpriteRenderer sprite;

    [SerializeField]
    private bool SetRandomTerm = true;

    [Space(5)]

    [SerializeField]
    private Color blinkColor = new Color(1f, 1f, 1f, 0.5f);
    [SerializeField]
    private float blinkSpeed = 1f;

    private Color startColor;
    private Color targetColor;
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        startColor = sprite.color;

        if (SetRandomTerm)
        {
            Invoke("BlinkStart", Random.Range(0f, 0.5f));
        }
        else
        {
            StartCoroutine("BlinkObj");
        }
    }
    private void BlinkStart()
    {

        StartCoroutine("BlinkObj");
    }

    private Color newColor;
    private float curTime = Mathf.PI;
    private IEnumerator BlinkObj()
    {
        Debug.Log("Move");
        while (true)
        {
            curTime += (blinkSpeed * Time.deltaTime) * Mathf.PI;
            targetColor = blinkColor / 2f;

            newColor.r = startColor.r - (Mathf.Sin(curTime) * targetColor.r + targetColor.r);
            newColor.g = startColor.g - (Mathf.Sin(curTime) * targetColor.g + targetColor.g);
            newColor.b = startColor.b - (Mathf.Sin(curTime) * targetColor.b + targetColor.b);
            newColor.a = startColor.a - (Mathf.Sin(curTime) * targetColor.a + targetColor.a);

            sprite.color = newColor;

            if (Mathf.Approximately(curTime, 2f))
            {
                curTime = 0f;
            }

            yield return null;
        }
    }
}
