using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Numbers : MonoBehaviour
{

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

    // Start is called before the first frame update
    void Start()
    {
        leftTextIndex = Random.Range(0, 2);
        middleTextIndex = Random.Range(0, 2);
        rightTextIndex = Random.Range(0, 2);

        leftText.text = leftTextList[leftTextIndex];
        middleText.text = middleTextList[middleTextIndex];
        rightText.text = rightTextList[rightTextIndex];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncreaseNumber(int numberIndex)
    {
        var newValue = Mathf.Repeat(int.Parse(m_NumberIndexes[numberIndex].text) + 1, 10);
        m_NumberIndexes[numberIndex].text = newValue.ToString();
    }

    public void CalculateAnswer()
    {

    }
}
