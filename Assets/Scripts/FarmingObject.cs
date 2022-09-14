using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmingObject : MonoBehaviour
{
    bool isDig;
    private GameObject obj;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isDig && Input.GetKeyDown(KeyCode.LeftControl))
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
        if(this.gameObject.name == "tree")
        {
            obj = Managers.Resource.Instantiate("chip");
            obj.transform.position = this.transform.position;
        }
        else if (this.gameObject.name == "stone")
        {
            obj = Managers.Resource.Instantiate("shingle");
            obj.transform.position = this.transform.position;
        }
        else if (this.gameObject.name == "grass")
        {
            obj = Managers.Resource.Instantiate("weed");
            obj.transform.position = this.transform.position;
        }

        Destroy(gameObject);

    }
}
