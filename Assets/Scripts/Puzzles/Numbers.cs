using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Numbers : MonoBehaviour
{
    public Image moduleBackground;
    public Sprite CorrectImage;

    [SerializeField]
    private Image[] images;

    [SerializeField]
    private int[] image_Number;

    [SerializeField]
    private List<Text> m_NumberIndexes;

    [SerializeField]
    List<string> leftTextList = new List<string>();//Ah Lian, Atas, Ayam right
    [SerializeField]
    List<string> middleTextList = new List<string>();//Kena, Kiasu, Kaypoh
    [SerializeField]
    List<string> rightTextList = new List<string>();//Sibeh, Sian, Siam

    [SerializeField]
    private Text leftText;

    [SerializeField]
    private Text middleText;

    [SerializeField]
    private Text rightText;

    int leftTextIndex;
    int middleTextIndex;
    int rightTextIndex;

    int leftAnswer;
    int middleAnswer;
    int rightAnswer;

    int playerLeftAnswer;
    int playerMiddleAnswer;
    int playerRightAnswer;

    // Start is called before the first frame update
    void Start()
    {
        leftTextIndex = Random.Range(0, 2);
        middleTextIndex = Random.Range(0, 2);
        rightTextIndex = Random.Range(0, 2);

        leftText.text = leftTextList[leftTextIndex];
        middleText.text = middleTextList[middleTextIndex];
        rightText.text = rightTextList[rightTextIndex];

        CalculateAnswer();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncreaseNumber(int numberIndex)
    {
        var newValue = Mathf.Repeat(int.Parse(m_NumberIndexes[numberIndex].text) + 1, 10);
        m_NumberIndexes[numberIndex].text = newValue.ToString();

        if(numberIndex == 0)
        playerLeftAnswer = (int)newValue;

        else if(numberIndex == 1)
            playerMiddleAnswer = (int)newValue;

        else if (numberIndex == 2)
            playerRightAnswer = (int)newValue;
    }

    public void CalculateAnswer()
    {
        if(leftTextIndex == 0)
        {
            leftAnswer = 1;
        }
        if (leftTextIndex == 1)
        {
            leftAnswer = 2;
        }
        if (leftTextIndex == 2)
        {
            leftAnswer = 3;
        }

        if (middleTextIndex == 0)
        {
            middleAnswer = 1;
        }
        if (middleTextIndex == 1)
        {
            middleAnswer = 2;
        }
        if (middleTextIndex == 2)
        {
            middleAnswer = 3;
        }

        if (rightTextIndex == 0)
        {
            rightAnswer = 1;
        }
        if (rightTextIndex == 1)
        {
            rightAnswer = 2;
        }
        if (rightTextIndex == 2)
        {
            rightAnswer = 3;
        }
    }

    public void ConfirmAnswer()
    {
        if (playerLeftAnswer == leftAnswer && playerMiddleAnswer == middleAnswer && playerRightAnswer == rightAnswer)
        {
            Debug.Log("Answer is correct");
            moduleBackground.sprite = CorrectImage;
        }
        else
        {
            Debug.Log("Answer is wrong");
            //Debug.Log(leftAnswer);
            //Debug.Log(middleAnswer);
            //Debug.Log(rightAnswer);
        }
    }
}
