using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ImageBombModule : MonoBehaviour
{
    public PlayerControl player;
    public Button leftButton;
    public Button middleButton;
    public Button rightButton;

    public Sprite Rojak;
    public Sprite Laksa;
    public Sprite MBS;
    public Image actualImage;

    List<string> LeftTextList = new List<string>();//Abuden, Alamak, Bo Jio
    List<string> MiddleTextList = new List<string>();//Bodoh, Encik, Chim
    List<string> RightTextList = new List<string>();//Chope, Liao, Rabak

    int displayLeft;
    int displayMiddle;
    int displayRight;
    int answer;//decides image number

    List<Sprite> ImageList = new List<Sprite>();
    public Sprite correct;
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

        ImageList.Add(Rojak);
        ImageList.Add(Laksa);
        ImageList.Add(MBS);

        InitDisplays();

        leftButton.transform.GetChild(0).GetComponent<Text>().text = LeftTextList[displayLeft];
        middleButton.transform.GetChild(0).GetComponent<Text>().text = MiddleTextList[displayMiddle];
        rightButton.transform.GetChild(0).GetComponent<Text>().text = RightTextList[displayRight];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitDisplays()
    {
        List<int> Numbers = new List<int>() { 0, 1, 2 };
        displayLeft = Random.Range(0, Numbers.Count);
        answer = Random.Range(0, Numbers.Count);
        actualImage.sprite = ImageList[answer];
        Numbers.Remove(displayLeft);
        int rand = Random.Range(0, Numbers.Count);
        displayMiddle = Numbers[rand];
        Numbers.Remove(displayMiddle);
        displayRight = Numbers[0];
    }

    public void ClickLeftButton()
    {
        if (displayLeft == answer && this.GetComponent<Image>().sprite != correct)
        {
            this.GetComponent<Image>().sprite = correct;
            player.AddCompleted();
        }
        else if (this.GetComponent<Image>().sprite != correct)
            player.AddMistake();
    }
    public void ClickMiddleButton()
    {
        if (displayMiddle == answer && this.GetComponent<Image>().sprite != correct)
        {
            this.GetComponent<Image>().sprite = correct;
            player.AddCompleted();
        }
        else if (this.GetComponent<Image>().sprite != correct)
            player.AddMistake();
    }
    public void ClickRightButton()
    {
        if (displayRight == answer && this.GetComponent<Image>().sprite != correct)
        {
            this.GetComponent<Image>().sprite = correct;
            player.AddCompleted();
        }
        else if(this.GetComponent<Image>().sprite != correct)
            player.AddMistake();
    }
}
