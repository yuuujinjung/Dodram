using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemScript : MonoBehaviour
{
    GameObject player;
    GameObject playerHoldingPoint;
    Vector2 forceDirection;
    bool isPlayerEnter;
    bool isHold;

    // Start is called before the first frame update
    void Start()
    {
        isHold = false;
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHoldingPoint = GameObject.FindGameObjectWithTag("HoldPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && isPlayerEnter)
        {
            Debug.Log("들기");
            transform.SetParent(playerHoldingPoint.transform);
            transform.localPosition = Vector2.zero;
            transform.rotation = new Quaternion(0, 0, 0, 0);
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            isHold = true;
            isPlayerEnter = false;
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && !isPlayerEnter)
        {
            if(isHold == true)
            {
                playerHoldingPoint.transform.DetachChildren();
                this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                isHold = false;
                Debug.Log("놓기");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isPlayerEnter = true;
            Debug.Log("충돌");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerEnter = false;
            Debug.Log("충돌 해제");
        }
    }
}