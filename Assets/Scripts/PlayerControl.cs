using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public Sprite Cross;
    public Image First_Box;
    public Image Second_Box;

    int m_mistakes;
    int completed;
    // Start is called before the first frame update
    void Start()
    {
        m_mistakes = 0;
        First_Box.sprite = Cross;
        Second_Box.sprite = Cross;
        First_Box.enabled = false;
        Second_Box.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!First_Box.enabled && m_mistakes >= 1)
            First_Box.enabled = true;
        else if (!Second_Box.enabled && m_mistakes >= 2)
            Second_Box.enabled = true;
        else if(m_mistakes >= 3)
        {
            //die here
        }

        if(completed >= 4)
        {
            //call victory here
            Debug.Log("ALL COMPLETED");
        }

    }

    public void AddMistake()
    {
        m_mistakes++;
    }

    public void AddCompleted()
    {
        completed++;
    }
}
