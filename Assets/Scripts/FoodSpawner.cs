using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
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
        SpawnFood();
    }
    public void SpawnFood()
    {
        Instantiate(foodItems[Random.Range(0, foodItems.Length)], new Vector3(Random.Range(-4f, 4f), 0f, Random.Range(-4f, 4f)), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
