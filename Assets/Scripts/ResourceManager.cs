using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Resource Manager 사용법

    Managers.Resource.Instantiate("Tank");
    Managers.Resource.Instantiate("Prefabs/Tank");
    
    Managers.Resource.Destroy(tank);
*/

public class ResourceManager 
{
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        if (prefab == null)
        {
            Debug.Log($"Filed to load prefab : {path}");
            return null;
        }

        return Object.Instantiate(prefab, parent);
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        Object.Destroy(go);
    }

}
