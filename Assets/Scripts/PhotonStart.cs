using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PhotonStart : MonoBehaviourPunCallbacks
{
    bool isConnecting;

    [SerializeField] GameObject playButton;
    [SerializeField] GameObject joiningRoomText;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void JoinGame()
    {
        if (!PhotonNetwork.IsConnected)
            isConnecting = PhotonNetwork.ConnectUsingSettings();
        else
            PhotonNetwork.JoinRandomRoom();

        playButton.SetActive(false);
        joiningRoomText.SetActive(true);
    }

    public override void OnConnectedToMaster()
    {
        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
            isConnecting = false;
        }
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);
        PhotonNetwork.CreateRoom("Level1", new Photon.Realtime.RoomOptions { MaxPlayers = 3 });
    }

}
