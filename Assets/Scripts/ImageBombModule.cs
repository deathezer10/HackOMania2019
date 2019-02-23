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
    List<int> Numbers = new List<int>();

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
        Numbers.Add(0);
        Numbers.Add(1);
        Numbers.Add(2);

        displayLeft = Random.Range(0, 2);
        RemoveFromNumberList(displayLeft);
        displayMiddle = 
        leftButton.transform.GetChild(0).GetComponent<Text>().text = LeftTextList[displayLeft];
        middleButton.transform.GetChild(0).GetComponent<Text>().text = MiddleTextList[displayMiddle];
        rightButton.transform.GetChild(0).GetComponent<Text>().text = RightTextList[displayRight];
    }

    void RemoveFromNumberList(int number)
    {
        for(int a = 0; a < Numbers.Count; a++)
        {
            if (number == Numbers[a])
            {
                Numbers.RemoveAt(a);
                break;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
