using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class PhotonGameplay : MonoBehaviour, IOnEventCallback
{
    int player_Role = -1;
    public Text roleText;
    public GameObject loadingPage;
    public GameObject startPage;
    public GameObject supportPage;
    public GameObject diffuserPage;

    public bool skipInitialConnection = false;
    public int skipRole = 0;

    public void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        if (PhotonNetwork.IsMasterClient)
        {
            ShuffleRole();
        }
    }
    
    void ShuffleRole()
    {
        player_Role = Random.Range(0, 2);
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
        PhotonNetwork.RaiseEvent((byte)EventCodes.SetRoles, player_Role, raiseEventOptions, SendOptions.SendReliable);

        startPage.SetActive(true);
        loadingPage.SetActive(false);

        if (player_Role == 1)
        {
            roleText.text = "SUPPORT";
            supportPage.SetActive(true);
        }
        else
        {
            roleText.text = "DIFFUSER";
            diffuserPage.SetActive(true);
        }
    }
    public void OnEvent(EventData photonEvent)
    {
        byte caseSwitch = photonEvent.Code;
        Debug.Log((int)caseSwitch);

        switch (caseSwitch)
        {
            case (byte)EventCodes.SetRoles:

                loadingPage.SetActive(false);
                startPage.SetActive(true);

                if ((int)photonEvent.CustomData == 0)
                {
                    player_Role = 1;
                    roleText.text = "SUPPORT";
                    supportPage.SetActive(true);
                }
                else
                {
                    player_Role = 0;
                    roleText.text = "DIFFUSER";
                    diffuserPage.SetActive(true);
                }
                break;
        }

    }

}
