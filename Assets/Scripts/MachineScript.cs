using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineScript : MonoBehaviour
{
    //public GameObject Hand;
    public GameObject artefact;
    GameObject itemCount;

    public float DestroyTime = 1;
    public float CraftTime = 1;
    bool destroyStart;
    bool craftStart;

    private void Awake()
    {
        DestroyTime = 60;
        CraftTime = 10;
    }

    private void Update()
    {
        DestroyCountDown();
        CraftCountDown();
        Crafting();
        ItemDestroy();
    }

    public void SubCount(GameObject hand)      //기계에 넣기
    {
        if(this.transform.childCount == 0)
        {
            destroyStart = true;
        }
        itemCount = hand.transform.GetChild(0).gameObject;
        itemCount.transform.SetParent(this.transform);
        itemCount.SetActive(false);
    }

    public void CraftOn()
    {
        craftStart = true;
    }

    public void Crafting()      //제작
    {
        if(CraftTime <= 0)
        {
            if (this.gameObject.transform.childCount == 3)
            {
                Instantiate(artefact, new Vector3(
                            this.transform.position.x,
                            this.transform.position.y - 0.5f,
                            this.transform.position.z),
                            this.transform.rotation);
            }
        }
    }

    public void ItemDestroy()
    {
        if (DestroyTime <= 0)
        {
            for (int a = 0; a < this.transform.childCount; a++)
            {
                Destroy(this.transform.GetChild(a).gameObject);
            }
            this.transform.DetachChildren();
            destroyStart = false;
            DestroyTime = 60;
        }
    }

    public void DestroyCountDown()
    {
        if (destroyStart == true)
        {
            DestroyTime -= Time.deltaTime;
        }
    }

    public void CraftCountDown()
    {
        if (craftStart == true)
        {
            CraftTime -= Time.deltaTime;
        }
    }
}