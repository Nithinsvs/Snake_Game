using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //SWIPE MOVEMENT
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered

    Rigidbody rb;
#if UNITY_EDITOR
    float moveSpeed = 5f;
#elif UNITY_ANDROID
float moveSpeed = 100f;
#endif


    //SNAKE PART
    [SerializeField] GameObject bodyPart;
    [SerializeField] int gap = 10;
    List<GameObject> bodyParts = new List<GameObject>();
    List<Transform> positions = new List<Transform>();



    void Start()
    {
        dragDistance = Screen.height * 5 / 100; //dragDistance is 5% height of the screen
        rb = GetComponent<Rigidbody>();
        positions.Insert(0, transform);
    }

    void Update()
    {

#if UNITY_EDITOR
        transform.position += transform.forward * Time.deltaTime * moveSpeed;

        float steerDirection = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * 180 * Time.deltaTime * steerDirection);
#endif

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list

                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {
                    //Drag confirmed

                    //checking if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lp.x > fp.x))  //If the movement was to the right)
                        {   //Right swipe
                            Debug.Log("Right Swipe");
                            rb.velocity = Vector3.right * Time.deltaTime * moveSpeed;
                        }
                        else
                        {   //Left swipe
                            Debug.Log("Left Swipe");
                            rb.velocity = Vector3.left * Time.deltaTime * moveSpeed;
                        }
                    }
                    else
                    {   //vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {
                            Debug.Log("Up Swipe");
                            rb.velocity = Vector3.forward * Time.deltaTime * moveSpeed;
                        }
                        else
                        {
                            Debug.Log("Down Swipe");
                            rb.velocity = Vector3.back * Time.deltaTime * moveSpeed;
                        }
                    }
                }
                else
                {
                    Debug.Log("Tap");
                }
            }
        }

        //MOVEMENT AND SPAWNING

        positions[positions.Count - 1].position = transform.position;
        Debug.Log("Moving");
        positions.Insert(0, positions[positions.Count - 1]);
        positions.RemoveAt(positions.Count - 1);

    }


    void GenerateTail()
    {
        GameObject body = Instantiate(bodyPart, transform.position, Quaternion.identity);
        bodyParts.Add(body);
        Debug.Log("Generating");
        positions.Insert(0, body.transform);

        FoodSpawner.instance.SpawnFood();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.food))
        {
            Debug.Log("Food consume");
            Destroy(other.gameObject);
            GenerateTail();
        }
        if (other.CompareTag(Constants.wall))
        {
            Debug.Log("Touched");
            GameManager.instance.GameOver();
        }

    }
}