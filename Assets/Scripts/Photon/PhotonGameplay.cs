using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class PhotonGameplay : MonoBehaviour, IOnEventCallback
{ 
    int player_Role;
    public Text whoAmI;

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

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShuffleRole()
    {
        player_Role = Random.Range(0, 2);
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
        PhotonNetwork.RaiseEvent((byte)EventCodes.SetRoles, player_Role, raiseEventOptions, ExitGames.Client.Photon.SendOptions.SendReliable);
        if (player_Role == 0)
        {
            //the master is the diffuser role
            whoAmI.text = "You are the diffuser";
        }
        else
        {
            whoAmI.text = "You are the support role";
        }

    }
    public void OnEvent(EventData photonEvent)
    {
        byte caseSwitch = photonEvent.Code;
        Debug.Log((int)caseSwitch);

        switch (caseSwitch)
        {
            case (byte)EventCodes.SetRoles:
                if ((int)photonEvent.CustomData == 0)
                {
                    player_Role = 1;
                    whoAmI.text = "You are the support role";
                }
                else
                {
                    player_Role = 0;
                    whoAmI.text = "You are the diffuser";
                }
                break;
        }

    }
}
