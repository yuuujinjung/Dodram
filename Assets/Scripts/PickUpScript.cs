using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpScript : MonoBehaviour
{
    GameObject obj;
    GameObject holdObj;
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
        playerHoldingPoint = GameObject.FindGameObjectWithTag("HoldPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isPlayerEnter)
        {
            if (isHold == false)
            {
                holdObj = obj;
                holdObj.transform.SetParent(playerHoldingPoint.transform);
                holdObj.transform.localPosition = Vector2.zero;
                holdObj.transform.rotation = new Quaternion(0, 0, 0, 0);
                //this.gameObject.GetComponent<BoxCollider2D>().enabled = false;

                Debug.Log("들기");
                isHold = true;
                isPlayerEnter = false;
            }
            else
            {
                playerHoldingPoint.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
                playerHoldingPoint.transform.DetachChildren();
                holdObj = obj;
                holdObj.transform.SetParent(playerHoldingPoint.transform);
                holdObj.transform.localPosition = Vector2.zero;
                holdObj.transform.rotation = new Quaternion(0, 0, 0, 0);
                holdObj.gameObject.GetComponent<BoxCollider2D>().enabled = false;

                Debug.Log("바꾸기");
                isHold = true;
                isPlayerEnter = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && !isPlayerEnter)
        {
            if (isHold == true)
            {
                playerHoldingPoint.transform.DetachChildren();
                holdObj.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                holdObj = null;
                isHold = false;
                Debug.Log("놓기");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("tool"))
        {
            //obj = GameObject.FindGameObjectWithTag("tool");
            obj = collision.gameObject;
            isPlayerEnter = true;
            Debug.Log("도구 충돌");
        }
        else if (collision.CompareTag("item"))
        {
            obj = collision.gameObject;
            isPlayerEnter = true;
            Debug.Log("아이템 충돌");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("tool"))
        {
            obj = null;
            isPlayerEnter = false;
            Debug.Log("도구 충돌 해제");
        }
        else if (collision.CompareTag("item"))
        {
            obj = null;
            isPlayerEnter = false;
            Debug.Log("아이템 충돌 해제");
        }
    }
}