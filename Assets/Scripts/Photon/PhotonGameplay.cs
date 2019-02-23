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
    public int playerReadyCount = 0;
    private int m_AudioCount = 0;
    public Text readyText;

    public PlayerRole player_Role = PlayerRole.None;
    public Text roleText;
    public GameObject loadingPage;
    public GameObject startPage;
    public GameObject supportPage;
    public GameObject diffuserPage;
    public GameObject btnLoading;
    public GameObject walkieTalkieContent;
    public GameObject audioPanelPrefab;


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

    public void ReadyToPlay()
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent((byte)EventCodes.PlayerReady, null, raiseEventOptions, SendOptions.SendReliable);

    }

    public void ChangeReadyText()
    {
        if (readyText)
            readyText.text = "Waiting for other players...";
    }

    void ShuffleRole()
    {
        player_Role = (PlayerRole)Random.Range(1, 3);
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

        switch ((EventCodes)caseSwitch)
        {
            case EventCodes.SetRoles:

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
            case EventCodes.PlayerReady:
                playerReadyCount++;
                if (PhotonNetwork.IsMasterClient && playerReadyCount == 2)
                {
                    RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                    PhotonNetwork.RaiseEvent((byte)EventCodes.StartGame, null, raiseEventOptions, SendOptions.SendReliable);
                }
                break;
            case EventCodes.StartGame:
                //Start Game
                Debug.Log("Game Has Started");
                startPage.SetActive(false);
                break;

            case EventCodes.AudioDiffuseToSupport:
            case EventCodes.AudioSupportToDiffuse:
                {
                    byte[] audioArray = (byte[])photonEvent.CustomData;
                    AudioClip clip = WavUtility.ToAudioClip(audioArray);
                    GameObject audioPanel = Instantiate(audioPanelPrefab, walkieTalkieContent.transform);
                    audioPanel.GetComponentInChildren<AudioPanelPlayButton>().AssignClip(clip);
                    audioPanel.GetComponentInChildren<Text>().text = "AUDIO " + ++m_AudioCount;
                    PhotonNetwork.RaiseEvent((byte)EventCodes.AudioDataSent, null, RaiseEventOptions.Default, SendOptions.SendReliable);
                }
                break;

            case EventCodes.AudioDataSent:
                btnLoading.SetActive(false);
                btnLoading.transform.parent.GetComponent<MicrophoneButton>().ResetSprite();
                break;

        }

    }

}
