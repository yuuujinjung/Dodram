using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FarmingObject : MonoBehaviour
{
    public GameObject dropItem;
    private GameObject prefab_obj;
    
    public void Digging()
    {
        prefab_obj = Instantiate(dropItem);
        prefab_obj.transform.position = this.transform.position;
        prefab_obj.name = dropItem.name;
        
        Destroy(gameObject);
    }
}
