using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineScript : MonoBehaviour
{
    public GameObject Hand;
    public GameObject small;
    public GameObject mideum;
    public GameObject large;
    GameObject itemCount;

    private void Start()
    {

    }
    public void SubCount()
    {
        itemCount = Hand.transform.GetChild(0).gameObject;
        Destroy(Hand.transform.GetChild(0).gameObject);
        Hand.transform.DetachChildren();
        itemCount.transform.SetParent(this.gameObject.transform);
        itemCount.SetActive(false);
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

}