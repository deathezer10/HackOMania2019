using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class PhotonConnect : Photon.Pun.MonoBehaviourPunCallbacks
{
    public GameObject MatchMakingPanel;
    public GameObject WaitingPanel;
    public GameObject StartButton;
    public Text StatusText;

    public void connectToPhoton()
    {
        Photon.Pun.PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Connecting to photon");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        StatusText.text = "Connection Failed: " + cause.ToString();
        MatchMakingPanel.SetActive(false);
    }

    private void Start()
    {
        connectToPhoton();
    }

    //public override void OnConnected()
    //{
    //    base.OnConnected();
    //    Debug.Log("Connected!");
    //}

    public void JoinRandomRoom()
    {
        Photon.Pun.PhotonNetwork.JoinRandomRoom();

        MatchMakingPanel.SetActive(true);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        StatusText.gameObject.SetActive(false);
        StartButton.SetActive(true);
        Debug.Log("OnConnectedToMaster() was called by PUN.");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log("Join random failed");

        Photon.Realtime.RoomOptions roomOptions = new Photon.Realtime.RoomOptions();
        roomOptions.MaxPlayers = 2;

        Photon.Pun.PhotonNetwork.CreateRoom(null, roomOptions, null, null);
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        Debug.Log("Created Room");
        WaitingPanel.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Joined room!");
        Debug.Log("MAX PLAYERS: " + Photon.Pun.PhotonNetwork.CurrentRoom.MaxPlayers);
        //Photon.Pun.PhotonNetwork.GetCustomRoomList();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        if (Photon.Pun.PhotonNetwork.CurrentRoom.MaxPlayers == Photon.Pun.PhotonNetwork.CurrentRoom.PlayerCount)
        {
            Debug.Log("Let's start game!");
            //Photon.Pun.PhotonNetwork.automaticallySyncScene = true;
            Photon.Pun.PhotonNetwork.LoadLevel("Gameplay");
        }
    }
}
