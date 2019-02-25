using Photon.Realtime;
using System.Collections.Generic;
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
        Debug.Log("Connecting to photon...");
        StatusText.transform.parent.GetComponent<Button>().enabled = false;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        StatusText.text = "Connection Failed: " + cause.ToString();
        StatusText.gameObject.SetActive(true);
        MatchMakingPanel.SetActive(false);
        StatusText.transform.parent.GetComponent<Button>().enabled = true;
        MatchMakingPanel.SetActive(false);
        WaitingPanel.SetActive(false);
        StartButton.SetActive(false);
    }

    private void Start()
    {
        if (Photon.Pun.PhotonNetwork.IsConnectedAndReady)
        {
            StatusText.gameObject.SetActive(false);
            StartButton.SetActive(true);
        }
        else
        {
            connectToPhoton();
        }

        if (PhotonGameplay.wasGameplay)
        {
            StatusText.text = "Disconnected";
            PhotonGameplay.wasGameplay = false;
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        foreach (var room in roomList)
        {
            Debug.Log(room);
        }
    }

    public void JoinRandomRoom()
    {
        var roomProperty = new ExitGames.Client.Photon.Hashtable();
        roomProperty.Add("gameEnded", false);
        Photon.Pun.PhotonNetwork.JoinRandomRoom(roomProperty, 2);

        MatchMakingPanel.SetActive(true);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        StatusText.gameObject.SetActive(false);
        StartButton.SetActive(true);
        Debug.Log("Connected to Master");
    }
    
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);

        Debug.Log("Join random failed");

        RoomOptions roomOptions = new RoomOptions();
        var roomProperty = new ExitGames.Client.Photon.Hashtable();
        roomProperty.Add("gameEnded", false);
        roomOptions.CustomRoomProperties = roomProperty;
        roomOptions.CustomRoomPropertiesForLobby = new string[] { "gameEnded" };
        roomOptions.MaxPlayers = 2;
        Photon.Pun.PhotonNetwork.CreateRoom(null, roomOptions, TypedLobby.Default, null);
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
