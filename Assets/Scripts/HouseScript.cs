using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HouseScript : MonoBehaviour
{
    public int needPartsNum;
    
    public GameObject[] buildingParts;

    public GameObject endCanvas;
    
    private float countNum;
    private float changeValue;
    public int checkIndex;
    
    void Start()
    {
        countNum = 0;

        changeValue = (float)needPartsNum / buildingParts.Length;
        checkIndex = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
        if (buildingParts.Length > checkIndex)
        {
            while (countNum-changeValue>=0)
            {
                countNum -= changeValue;

                var go = Instantiate(buildingParts[checkIndex]);
                go.transform.SetParent(this.transform);
                go.transform.localPosition = new Vector3(0, 0,go.transform.position.z);

                checkIndex += 1;
            }
        }
        else 
        {
            //Time.timeScale = 0f;
            endCanvas.SetActive(true);

        }


    }

    public void Building(GameObject hand)
    {
        GameObject playerItem;
        playerItem = hand.transform.GetChild(0).gameObject;

        if (playerItem.name == "Last_Parts" && buildingParts.Length > checkIndex)
        {
            playerItem.transform.SetParent(this.transform);
            //playerItem.SetActive(false);
            Destroy(playerItem);
            countNum += 1;
        }
    }
}
