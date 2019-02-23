using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlipImageBombModuleScript : MonoBehaviour
{
    List<string> MainTextList = new List<string>();//Ah Lian, Atas, Ayam right
    List<string> TopTextList = new List<string>();//Kena, Kiasu, Kaypoh
    List<string> BottomTextList = new List<string>();//Sibeh, Sian, Siam

    public Text MainText;
    public Image TopText;
    public Image BottomText;

    int maintext;
    int toptext;
    int bottomtext;

    float TopRotateAngle;
    float BottomRotateAngle;
    // Start is called before the first frame update
    void Start()
    {
        MainTextList.Add("Ah Lian");
        MainTextList.Add("Atas");
        MainTextList.Add("Ayam");
        TopTextList.Add("Kena");
        TopTextList.Add("Kiasu");
        TopTextList.Add("Kaypoh");
        BottomTextList.Add("Sibeh");
        BottomTextList.Add("Sian");
        BottomTextList.Add("Siam");

        maintext = Random.Range(0, 2);
        toptext = Random.Range(0, 2);
        bottomtext = Random.Range(0, 2);

        MainText.text = MainTextList[maintext];
        TopText.transform.GetChild(0).GetComponent<Text>().text = TopTextList[toptext];
        BottomText.transform.GetChild(0).GetComponent<Text>().text = BottomTextList[bottomtext];

        CalculateRotateAnswer();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RotateTopImageLeft()
    {
        TopText.transform.Rotate(0, 0, 90);
        //Debug.Log(TopText.transform.rotation.eulerAngles.z);
    }
    public void RotateTopImageRight()
    {
        TopText.transform.Rotate(0, 0, -90);
        //Debug.Log(TopText.transform.rotation.eulerAngles.z);
    }
    public void RotateBottomImageLeft()
    {
        BottomText.transform.Rotate(0, 0, 90);
        //Debug.Log(BottomText.transform.rotation.eulerAngles.z);
    }
    public void RotateBottomImageRight()
    {
        BottomText.transform.Rotate(0, 0, -90);
        //Debug.Log(BottomText.transform.rotation.eulerAngles.z);
    }

    void CalculateRotateAnswer()
    {
        int top = 0;
        int bottom = 0;
        if(maintext == 0)
        {
            top = toptext - 2 + 1;
            bottom = bottomtext - 1 + 1;
        }
        else if(maintext == 1)
        {
            top = toptext - 1 + 1;
            bottom = bottomtext - 2 + 1;
        }

        if (top < 0)
            TopRotateAngle = 360 + (top * 90);
        else
            TopRotateAngle = top * 90;

        if (bottom < 0)
            BottomRotateAngle = 360 + (bottom * 90);
        else
            BottomRotateAngle = bottom * 90;
    }

    public void ConfirmAnswer()
    {
        if (TopText.transform.rotation.eulerAngles.z == TopRotateAngle && BottomText.transform.rotation.eulerAngles.z == BottomRotateAngle)
            Debug.Log("Correct");
        else
        {
            Debug.Log(TopRotateAngle);
            Debug.Log(BottomRotateAngle);
        }
            
    }
}
