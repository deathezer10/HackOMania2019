using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class DataUpload : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Upload());
    }

    IEnumerator Upload()
    {
        string url = "http://52.163.240.180/client/dynamic/recognize";
        Hashtable headers = new Hashtable();
        headers.Add("content-type", "audio/x-wav");
        WWW www = new WWW(url, new byte[] { (byte)0 }, headers);
        yield return www;
        Debug.Log(www.text);

        string temp = www.text;
        temp = temp.Replace("\"", "");
        temp = temp.Replace("}", "");
        temp = temp.Replace("{", "");
        string[] data = temp.Split(',');
        Debug.Log(data[0]);
        Debug.Log(data[1]);
        Debug.Log(data[2]);
    }
}
