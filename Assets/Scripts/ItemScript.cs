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

    // Start is called before the first frame update
    void Start()
    {

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

            isPlayerEnter = false;
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
