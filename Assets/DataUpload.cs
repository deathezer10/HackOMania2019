using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class DataUpload : MonoBehaviour
{
    string temp;
    string[] data;
    
    public void uploadVideo(byte[] audio)
    {
        StartCoroutine(Upload(audio));
    }


    IEnumerator Upload(byte[] audio)
    {
        string url = "http://52.163.240.180/client/dynamic/recognize";
        Hashtable headers = new Hashtable();
        headers.Add("content-type", "audio/x-wav");
        WWW www = new WWW(url, audio, headers);
        yield return www;
        Debug.Log(www.text);

        temp = www.text;
        temp = temp.Replace("\"", "");
        temp = temp.Replace("}", "");
        temp = temp.Replace("{", "");
        data = temp.Split(',');
        Debug.Log(data[0]);
        Debug.Log(data[1]);
        Debug.Log(data[2]);
        CreateText();
    }

    void CreateText()
    {
        //Path of the file
        string path = Application.dataPath + "/Log.txt";
        //Create File if it doesn't exist
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "Login log \n\n");
        }
        //Content of the file
        string content = "Time: " + System.DateTime.Now + "," + data[0] + "," + data[1] + "," + data[2];
        //Add some to text to it
        File.AppendAllText(path, content);
        Debug.Log("wrote to file");
    }
}
