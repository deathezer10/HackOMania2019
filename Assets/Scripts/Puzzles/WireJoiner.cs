using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WireJoiner : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public PlayerControl player;
    public GameObject m_LinePrefab;
    WireBombModule m_BombModule;
    GameObject m_Line;
    bool m_validWire;
    bool m_doNotDraw;
    int m_currWire;

    public void OnDrag(PointerEventData eventData)
    {
        if (m_Line != null && !m_doNotDraw)
        {
            var cursorWorlPos = Input.mousePosition;
            cursorWorlPos.z = 0;

            SetLineToPos(cursorWorlPos);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        m_doNotDraw = false;
        for (int a = 0; a < m_BombModule.GetImageList().Count; a++)
        {
            if (transform.position == m_BombModule.GetImageList()[a].transform.position)
            {
                m_currWire = a;
                if (m_BombModule.GetCheckImageList()[a])
                {
                    m_doNotDraw = true;
                    return;
                }
            }
        }
        m_Line = Instantiate(m_LinePrefab, transform);
        m_Line.transform.localPosition = Vector2.zero;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (m_Line != null && !m_doNotDraw)
        {
            float distanceCheck = ((Vector3)eventData.position - transform.position).magnitude;
            if (distanceCheck > 20)
            {
                for (int a = 0; a < m_BombModule.GetImageList().Count; a++)
                {
                    float distance = ((Vector3)eventData.position - m_BombModule.GetImageList()[a].transform.position).magnitude;
                    if (distance < 20)
                    {//might use a number instead of localscale if wrong
                        if (m_currWire == 0 && a == 3 || m_currWire == 1 && a == 4 || m_currWire == 2 && a == 5 || m_currWire == 3 && a == 0 || m_currWire == 4 && a == 1 || m_currWire == 5 && a == 2)
                        {
                            m_BombModule.GetCheckImageList()[a] = true;
                            m_BombModule.GetCheckImageList()[m_currWire] = true;
                            SetLineToPos(m_BombModule.GetImageList()[a].transform.position);
                            Debug.Log("Connected");
                            m_validWire = true;
                            m_BombModule.CheckWireAnswer();
                            break;
                        }
                        else if (!m_BombModule.CheckImageCorrect())
                            player.AddMistake();
                    }
                }
            }
            if (!m_validWire)
            {
                Destroy(m_Line.gameObject);
                m_validWire = false;
            }
            else
            {
                for (int a = 0; a < m_BombModule.GetImageList().Count; a++)
                {
                    if (m_BombModule.GetImageList()[a].transform.position == transform.position)
                    {
                        m_BombModule.GetCheckImageList()[a] = true;
                        break;
                    }
                }
            }
        }
    }

    void SetLineToPos(Vector3 pos)
    {
        float distance = (pos - m_Line.transform.position).magnitude;
        Vector3 currentScale = m_Line.transform.localScale;
        m_Line.transform.right = pos - m_Line.transform.position;

        currentScale.x = distance / FindObjectOfType<Canvas>().scaleFactor;
        m_Line.transform.localScale = currentScale;
    }

    private void Start()
    {
        m_validWire = false;
        m_doNotDraw = false;
        m_BombModule = FindObjectOfType<WireBombModule>();
    }

}
