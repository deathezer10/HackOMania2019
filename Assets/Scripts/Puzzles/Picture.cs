using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Picture : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_ImageIndexes;

    int imageAnswer;

    [SerializeField]
    private Button[] SpinObject;

    int activatedPicture;

    int playerAnswer;

    // Start is called before the first frame update
    void Start()
    {
        activatedPicture = Random.Range(0, 2);

        m_ImageIndexes[activatedPicture].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CalculateAnswer()
    {
        //if(m_ImageIndexes[activatedPicture] == 0)
        //{

        //}
    }
    public void CheckAnswer(int buttonIndex)
    {
       // if(SpinObject[buttonIndex])
    }
}
