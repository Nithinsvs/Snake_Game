using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject gameOverScreen;


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

}
