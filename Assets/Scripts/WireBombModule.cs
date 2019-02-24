using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WireBombModule : MonoBehaviour
{
    List<Image> WireImageList = new List<Image>();
    public Image topLeftWire;
    public Image topMiddleWire;
    public Image topRightWire;
    public Image bottomLeftWire;
    public Image bottomMiddleWire;
    public Image bottomRightWire;

    public Text topLeftImage;
    public Text topMiddleImage;
    public Text topRightImage;
    public Text bottomLeftImage;
    public Text bottomMiddleImage;
    public Text bottomRightImage;

    List<List<string>> WireText = new List<List<string>>();
    List<int> WireTextChoice = new List<int>();
    List<bool> WireCheckList = new List<bool>();

    Image targetWire;

    bool m_pair1;
    bool m_pair2;
    bool m_pair3;
    // Start is called before the first frame update
    void Start()
    {
        WireImageList.Add(topLeftWire);
        WireCheckList.Add(false);
        WireImageList.Add(topMiddleWire);
        WireCheckList.Add(false);
        WireImageList.Add(topRightWire);
        WireCheckList.Add(false);
        WireImageList.Add(bottomLeftWire);
        WireCheckList.Add(false);
        WireImageList.Add(bottomMiddleWire);
        WireCheckList.Add(false);
        WireImageList.Add(bottomRightWire);
        WireCheckList.Add(false);

        WireText.Add(new List<string>());
        WireText[0].Add("Barang");
        //WireText[0].Add("Encik");
        WireText.Add(new List<string>());
        WireText[1].Add("Hosei");
        //WireText[1].Add("Kopi");
        WireText.Add(new List<string>());
        WireText[2].Add("Sabo");
        //WireText[2].Add("Shiok");
        WireText.Add(new List<string>());
        WireText[3].Add("Zai");
        //WireText[3].Add("Jialat");
        WireText.Add(new List<string>());
        WireText[4].Add("Chim");
        //WireText[4].Add("Macam");
        WireText.Add(new List<string>());
        WireText[5].Add("Mata");
        //WireText[5].Add("Taupok");

        for(int a = 0; a < WireText.Count; a++)
            WireTextChoice.Add(0);

        topLeftImage.text = WireText[0][WireTextChoice[0]];
        topMiddleImage.text = WireText[1][WireTextChoice[1]];
        topRightImage.text = WireText[2][WireTextChoice[2]];
        bottomLeftImage.text = WireText[3][WireTextChoice[3]];
        bottomMiddleImage.text = WireText[4][WireTextChoice[4]];
        bottomRightImage.text = WireText[5][WireTextChoice[5]];

        m_pair1 = false;
        m_pair2 = false;
        m_pair3 = false;
    }

    // Update is called once per frame
    void Update()
    { 

    }

    void CheckWireAnswer()
    {
        if(WireCheckList[0])
        {
            if (!WireCheckList[3])
            {

            }
            else
                m_pair1 = true;
        }

        if(WireCheckList[1])
        {
            if(!WireCheckList[4])
            {

            }
            else
                m_pair2 = true;
        }

        if(WireCheckList[2])
        {
            if(!WireCheckList[4])
            {

            }
            else
                m_pair3 = true;
        }

    }

    bool CheckIfPresentInImageList(Vector3 pos)
    {
        for(int a = 0; a < WireImageList.Count; a++)
        {

        }
        return false;
    }

    public List<Image> GetImageList()
    {
        return WireImageList;
    }

    public List<bool> GetCheckImageList()
    {
        return WireCheckList;
    }
}
