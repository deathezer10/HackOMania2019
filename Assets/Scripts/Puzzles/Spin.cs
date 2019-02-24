using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spin : MonoBehaviour
{
    public PlayerControl player;
    public Image moduleBackground;
    public Sprite CorrectImage;

    [SerializeField]
    private Button[] SpinObject;

    [SerializeField]
    List<string> top_LeftTextList = new List<string>();//Ah Lian, Atas, Ayam right
    [SerializeField]
    List<string> top_RightTextList = new List<string>();//Kena, Kiasu, Kaypoh
    [SerializeField]
    List<string> bottom_LeftTextList = new List<string>();//Sibeh, Sian, Siam
    [SerializeField]
    List<string> bottom_RightTextList = new List<string>();//Abit, Act blur, Abuden

    [SerializeField]
    private Text top_LeftText;

    [SerializeField]
    private Text top_RightText;

    [SerializeField]
    private Text bottom_LeftText;

    [SerializeField]
    private Text bottom_RightText;

    int topLeftTextIndex;
    int topRightTextIndex;
    int bottomLeftTextIndex;
    int bottomRightTextIndex;

    float topLeftRotateAngle;
    float topRightRotateAngle;
    float bottomLeftRotateAngle;
    float bottomRightRotateAngle;

    int topLeftAnswer;
    int TopRightAnswer;
    int bottomLeftAnswer;
    int bottomRightAnswer;

    float playerTopLeftAnswer;
    float playerTopRightAnswer;
    float playerBottomleftAnswer;
    float playerBottomRightAnswer;

    // Start is called before the first frame update
    void Start()
    {
        topLeftTextIndex = Random.Range(0, 2);
        topRightTextIndex = Random.Range(0, 2);
        bottomLeftTextIndex = Random.Range(0, 2);
        bottomRightTextIndex = Random.Range(0, 2);

        top_LeftText.text = top_LeftTextList[topLeftTextIndex];
        top_RightText.text = top_RightTextList[topRightTextIndex];
        bottom_LeftText.text = bottom_LeftTextList[bottomLeftTextIndex];
        bottom_RightText.text = bottom_RightTextList[bottomRightTextIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click(int buttonIndex)
    {
            SpinObject[buttonIndex].transform.Rotate(0,0,90);
            Debug.Log("button is clicked");
    }

    public void CalculateAnswer()
    {
        topLeftRotateAngle = 90 * (topLeftTextIndex + 1);
        topRightRotateAngle = 90 * (topRightTextIndex + 1);
        bottomLeftRotateAngle = 90 * (bottomLeftTextIndex + 1);
        bottomRightRotateAngle = 90 * (bottomRightTextIndex + 1);

        if (SpinObject[0].transform.eulerAngles.z == topLeftRotateAngle &&
            SpinObject[1].transform.eulerAngles.z == topRightRotateAngle &&
            SpinObject[2].transform.eulerAngles.z == bottomLeftRotateAngle &&
            SpinObject[3].transform.eulerAngles.z == bottomRightRotateAngle &&
            moduleBackground.sprite != CorrectImage)
        {
            Debug.Log("Correct");
            moduleBackground.sprite = CorrectImage;
            player.AddCompleted();
        }
        else if(moduleBackground.sprite != CorrectImage)
        {
            player.AddMistake();
        }
    }
}
