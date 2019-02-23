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

        yield return new WaitForSeconds(waitTime);
        matchmakingtext.text = "Matchmaking in progress.";
        StartCoroutine(loadingText2(swapTime));
    }

    private IEnumerator loadingText2(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        matchmakingtext.text = "Matchmaking in progress..";
        StartCoroutine(loadingText3(swapTime));
    }

    private IEnumerator loadingText3(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        matchmakingtext.text = "Matchmaking in progress...";
        StartCoroutine(loadingText4(swapTime));
    }

    private IEnumerator loadingText4(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        matchmakingtext.text = "Matchmaking in progress";
        StartCoroutine(loadingText(swapTime));
    }
}
