using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelArrow : MonoBehaviour
{

    [SerializeField]
    private Button m_OtherArrow;

    int GetCurrentPanel()
    {
        for (int i = 0; i < 4; ++i)
        {
            if (transform.parent.GetChild(i).gameObject.activeSelf)
            {
                return i;
            }
        }
        return -1;
    }

    public void ChangePanel(bool goRight)
    {
        var curr = GetCurrentPanel();

        var newCurr = (int)Mathf.Repeat(curr + ((goRight) ? 1 : -1), 4);
        
        if (goRight)
        {
            if (newCurr == 3)
            {
                GetComponent<Button>().image.enabled = false;
            } else
            {
                m_OtherArrow.image.enabled = true;
            }
        }
        else
        {
            if (newCurr == 0)
            {
                GetComponent<Button>().image.enabled = false;
            }
            else
            {
                m_OtherArrow.image.enabled = true;
            }
        }

        transform.parent.GetChild(curr).gameObject.SetActive(false);
        transform.parent.GetChild(newCurr).gameObject.SetActive(true);
    }

}
