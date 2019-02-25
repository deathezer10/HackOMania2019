using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchmakingText : MonoBehaviour
{
    private float swapTime = 0.5f;
    public Text matchmakingtext;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(loadingText(swapTime));
    }

    private IEnumerator loadingText(float waitTime)
    {
        while (true)
        {
            matchmakingtext.text += ".";
            yield return new WaitForSeconds(waitTime);
            matchmakingtext.text += ".";
            yield return new WaitForSeconds(waitTime);
            matchmakingtext.text += ".";
            yield return new WaitForSeconds(waitTime);
            matchmakingtext.text = matchmakingtext.text.Remove(matchmakingtext.text.Length - 3);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
