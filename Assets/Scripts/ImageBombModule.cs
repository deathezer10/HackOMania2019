using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageBombModule : MonoBehaviour
{
    public Button leftButton;
    public Button middleButton;
    public Button rightButton;

    List<string> LeftTextList = new List<string>();//Abuden, Alamak, Bo Jio
    List<string> MiddleTextList = new List<string>();//Bodoh, Encik, Chim
    List<string> RightTextList = new List<string>();//Chope, Liao, Rabak

    int displayLeft;
    int displayMiddle;
    int displayRight;
    int answer;
    // Start is called before the first frame update
    void Start()
    {
        LeftTextList.Add("Abuden");
        LeftTextList.Add("Alamak");
        LeftTextList.Add("Bo Jio");
        MiddleTextList.Add("Bodoh");
        MiddleTextList.Add("Encik");
        MiddleTextList.Add("Chim");
        RightTextList.Add("Chope");
        RightTextList.Add("Liao");
        RightTextList.Add("Rabak");

        displayLeft = displayMiddle = displayRight = answer = Random.Range(0, 2);
        while (displayMiddle == displayLeft)
            displayMiddle = Random.Range(0, 2);
        while (displayRight == displayLeft || displayRight == displayMiddle)
            displayRight = Random.Range(0, 2);
        leftButton.transform.GetChild(0).GetComponent<Text>().text = LeftTextList[displayLeft];
        middleButton.transform.GetChild(0).GetComponent<Text>().text = MiddleTextList[displayMiddle];
        rightButton.transform.GetChild(0).GetComponent<Text>().text = RightTextList[displayRight];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickLeftButton()
    {
        if (displayLeft == answer)
            Debug.Log("true");
        else
            Debug.Log("false");
    }
    public void ClickMiddleButton()
    {
        if (displayMiddle == answer)
            Debug.Log("true");
        else
            Debug.Log("false");
    }
    public void ClickRightButton()
    {
        if (displayRight == answer)
            Debug.Log("true");
        else
            Debug.Log("false");
    }
}
