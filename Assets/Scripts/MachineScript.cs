using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineScript : MonoBehaviour
{
    PickUpScript pickUpScript;
    //public GameObject Hand;
    public GameObject small;
    public GameObject mideum;
    public GameObject large;
    GameObject itemCount;

    public float timeLimit = 1;
    bool timeStart;

    private void Awake()
    {
        pickUpScript = GameObject.Find("Player").GetComponent<PickUpScript>();
        timeLimit = 60;
    }

    private void Update()
    {
        CountDown();
        ItemDestroy();
    }

    public void SubCount()
    {
        if(this.transform.childCount == 0)
        {
            timeStart = true;
        }
        itemCount = pickUpScript.Hand.transform.GetChild(0).gameObject;
        itemCount.transform.SetParent(this.transform);
        itemCount.SetActive(false);
        pickUpScript.isHold = false;
    }

    public void Crafting()
    {
        if (this.gameObject.transform.childCount == 1)
        {
            Instantiate(small).transform.position = this.transform.position;
        }
        else if (this.gameObject.transform.childCount == 2)
        {
            Instantiate(mideum).transform.position = this.transform.position;
        }
        else if (this.gameObject.transform.childCount == 3)
        {
            Instantiate(large).transform.position = this.transform.position;
        }
    }

    public void ItemDestroy()
    {
        if(timeLimit <= 0)
        {
            for (int a = 0; a < this.transform.childCount; a++)
            {
                Destroy(this.transform.GetChild(a).gameObject);
            }
            this.transform.DetachChildren();
            timeStart = false;
            timeLimit = 20;
        }
    }

    public void CountDown()
    {
        if (timeStart == true)
        {
            timeLimit -= Time.deltaTime;
        }
    }
}