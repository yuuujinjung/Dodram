using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FarmingObject : MonoBehaviour
{
    public GameObject dropItem;
    
    public void Digging()
    {
        Instantiate(dropItem).transform.position = this.transform.position;
        
        Destroy(gameObject);
    }
}
