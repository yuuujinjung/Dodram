using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmingObject : MonoBehaviour
{
    bool isDig;
    public GameObject ammo;

    void Start()
    {
        //ammo = Managers.Resource.Instantiate("stone");
    }

    // Update is called once per frame
    void Update()
    {
        if(isDig && Input.GetKeyDown(KeyCode.Space))
        {
            Digging();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            isDig = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            isDig = false;
        }
    }

    void Digging()
    {
        Destroy(gameObject);
        Managers.Resource.Instantiate("stone");
    }
}
