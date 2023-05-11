using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FoodSpawner : MonoBehaviourPunCallbacks
{
    public GameObject[] foodItems;
    public static FoodSpawner instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
            SpawnFood();
    }
    public void SpawnFood()
    {
        PhotonNetwork.Instantiate(foodItems[Random.Range(0, foodItems.Length)].name, new Vector3(Random.Range(-4f, 4f), 0f, Random.Range(-4f, 4f)), Quaternion.identity);
    }
}
