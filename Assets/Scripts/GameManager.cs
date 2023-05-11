using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;
    public GameObject gameOverScreen;
    [SerializeField] GameObject wall;
    [SerializeField] Canvas canvas;



    private void Awake()
    {
        instance = this;
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
    }
    // Start is called before the first frame update
    void Start()
    {
        if(Player.localPlayerInstance == null)
        PhotonNetwork.Instantiate("Player", new Vector3(-3, 0.5f, -1), Quaternion.identity);
    }

    public void GameOver()
    {
        //wall.SetActive(false);
        //gameOverScreen.SetActive(true);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log("Player joined room");
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel("Room" + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel(PhotonNetwork.CurrentRoom.PlayerCount);
    }

}
