using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FarmingObject : MonoBehaviour
{
    public GameObject dropItem;
    GameObject Prefab_obj;
    
    public void Digging()
    {
        Prefab_obj = Instantiate(dropItem);
        Prefab_obj.transform.position = this.transform.position;
        Prefab_obj.name = dropItem.name;

        
        Destroy(gameObject);
    }
}