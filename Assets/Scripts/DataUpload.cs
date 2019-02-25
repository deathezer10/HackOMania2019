using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class DataUpload : MonoBehaviour
{
    public GameObject uploadPanel, uploadScrollView, uploadScrollViewContent, audioPanelprefab;
    public Text uploadText, uploadHeaderText;
    public bool skipDataCollection = false;

    public void UploadVideo()
    {
        uploadPanel.SetActive(true);
        uploadScrollView.SetActive(false);
        uploadText.gameObject.SetActive(true);
        uploadHeaderText.gameObject.SetActive(true);

        StartCoroutine(Upload());
    }

    IEnumerator Upload()
    {
        var micButton = FindObjectOfType<MicrophoneButton>();

        int totalVoiceData = micButton.m_VoiceList.Count;

        int currentCount = 0;

        foreach (var data in micButton.m_VoiceList)
        {
            currentCount++;

            if (!skipDataCollection)
            {
                string url = "https://westus.api.cognitive.microsoft.com/sts/v1.0?language=en-US";

                var webForm = new WWWForm();
                webForm.AddBinaryData("", data.Value);
                var request = UnityWebRequest.Post(url, webForm);
                request.SetRequestHeader("Accept", "application/json");
                request.SetRequestHeader("Content-Type", "audio/wav");

                yield return request.SendWebRequest();

                long responseCode = request.responseCode;

                if ((request.isHttpError || request.isNetworkError || responseCode != 200))
                {
                    Destroy(uploadHeaderText.GetComponent<MatchmakingText>());
                    uploadText.text = "Error Code: " + responseCode;
                    uploadHeaderText.text = "Upload Failed";
                    Debug.LogWarning("WWW data upload request failed: " + request.error);
                    foreach (var errors in request.GetResponseHeaders())
                    {
                        Debug.LogWarningFormat("Header: {0} Body: {1}", errors.Key, errors.Value);
                    }
                    yield break;
                }
                else
                {
                    var temp = request.GetResponseHeaders();

                    foreach (var item in temp)
                    {
                        Debug.LogFormat("Key: '{0}' Value: '{1}'", item.Key, item.Value);
                    }

                    var panel = Instantiate(audioPanelprefab, uploadScrollViewContent.transform);
                    panel.GetComponentInChildren<AudioPanelPlayButton>().AssignClip(data.Key);
                    panel.GetComponentInChildren<Text>().text = "Audio " + currentCount;

                    var currentPercent = 100 * (currentCount / totalVoiceData);

                    string color = "";
                    if (currentPercent < 50)
                        color = "orange";
                    else
                        color = "green";

                    uploadText.text = string.Format("Status: <color={0}>{1}%</color>", color, currentPercent);
                }
            }
            else
            {
                var panel = Instantiate(audioPanelprefab, uploadScrollViewContent.transform);
                panel.GetComponentInChildren<AudioPanelPlayButton>().AssignClip(data.Key);
                panel.GetComponentInChildren<Text>().text = "Audio " + currentCount;

                var currentPercent = 100 * (currentCount / totalVoiceData);

                string color = "";
                if (currentPercent < 50)
                    color = "orange";
                else
                    color = "green";

                uploadText.text = string.Format("Status: <color={0}>{1}%</color>", color, currentPercent);
            }
        }

        Destroy(uploadHeaderText.GetComponent<MatchmakingText>());
        uploadHeaderText.text = "<color=green>Upload complete!</color>";
        uploadText.gameObject.SetActive(false);
        uploadScrollView.SetActive(true);
    }

    void CreateText()
    {
        ////Path of the file
        //string path = Application.dataPath + "/Log.txt";
        ////Create File if it doesn't exist
        //if (!File.Exists(path))
        //{
        //    File.WriteAllText(path, "Login log \n\n");
        //}
        ////Content of the file
        //string content = "Time: " + System.DateTime.Now + "," + data[0] + "," + data[1] + "," + data[2];
        ////Add some to text to it
        //File.AppendAllText(path, content);
        //Debug.Log("wrote to file");
    }

    public void QuitGame()
    {
        StopAllCoroutines();
        PhotonGameplay.wasGameplay = false;
        Photon.Pun.PhotonNetwork.AutomaticallySyncScene = false;
        Photon.Pun.PhotonNetwork.LeaveRoom();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
        Photon.Pun.PhotonNetwork.AutomaticallySyncScene = true;
    }

}
