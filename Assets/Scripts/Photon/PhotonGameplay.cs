using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class PhotonGameplay : MonoBehaviour, IOnEventCallback
{
    public enum PlayerRole { None, Support, Diffuser }

    private PlayerRole player_Role = PlayerRole.None;
    public Text roleText;
    public GameObject loadingPage;
    public GameObject startPage;
    public GameObject supportPage;
    public GameObject diffuserPage;

    public bool skipInitialConnection = false;
    public PlayerRole skipRoleToUse = PlayerRole.Support;

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
        if (PhotonNetwork.IsConnected)
            Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);

        if (PhotonNetwork.IsMasterClient && !skipInitialConnection)
        {
            ShuffleRole();
        }

        if (skipInitialConnection)
        {
            startPage.SetActive(true);
            loadingPage.SetActive(false);
            player_Role = skipRoleToUse;
            if (player_Role == PlayerRole.Support)
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

    }

    void ShuffleRole()
    {
        player_Role = (PlayerRole)Random.Range(0, 2);
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
        PhotonNetwork.RaiseEvent((byte)EventCodes.SetRoles, player_Role, raiseEventOptions, SendOptions.SendReliable);

        startPage.SetActive(true);
        loadingPage.SetActive(false);

        if (player_Role == PlayerRole.Support)
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

                if ((PlayerRole)photonEvent.CustomData == PlayerRole.Diffuser)
                {
                    player_Role = PlayerRole.Support;
                    roleText.text = "SUPPORT";
                    supportPage.SetActive(true);
                }
                else
                {
                    player_Role = PlayerRole.Diffuser;
                    roleText.text = "DIFFUSER";
                    diffuserPage.SetActive(true);
                }
                break;
        }

    }

}
