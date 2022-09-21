using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PickUpScript : MonoBehaviour
{
    public Vector2 size;
    public LayerMask whatIsLayer;
    public GameObject Hand;

    public bool isHold;
    

    // Start is called before the first frame update
    void Start()
    {
        isHold = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        Interactive(); //상호작용
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }

    void Interactive()
    {
        Collider2D[] hit = Physics2D.OverlapBoxAll(transform.position, size, 0, whatIsLayer);
        
        if (hit != null)
        {
            for(int i = 0; i < hit.Length; ++i)
            {
                Debug.Log(hit[i].name);
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (isHold == true)
                { 
                    Hand.transform.DetachChildren();
                    isHold = false;
                    return;
                }
                
                for(int i = 0; i < hit.Length; ++i)
                {
                    if (hit[i].CompareTag("tool") || hit[i].CompareTag("item"))
                    {
                        hit[i].gameObject.transform.SetParent(Hand.transform);
                        hit[i].transform.localPosition = Vector2.zero;
                        isHold = true;
                    }
                    break;
                }
            }

            if (isHold == true)
            {
                if (Hand.transform.GetChild(0).name == "Axe")
                {
                    for (int i = 0; i < hit.Length; ++i)
                    {
                        if (hit[i].CompareTag("Tree"))
                        {
                            if (Input.GetKeyDown(KeyCode.LeftControl))
                            {
                                hit[i].GetComponent<FarmingObject>().Digging();
                            }
                            break;
                        }
                    }
                }
                if (Hand.transform.GetChild(0).name == "PickAxe")
                {
                    for (int i = 0; i < hit.Length; ++i)
                    {
                        if (hit[i].CompareTag("Stone"))
                        {
                            if (Input.GetKeyDown(KeyCode.LeftControl))
                            {
                                hit[i].GetComponent<FarmingObject>().Digging();
                            }
                            break;
                        }
                    }
                }
                if (Hand.transform.GetChild(0).name == "Scythe")
                {
                    for (int i = 0; i < hit.Length; ++i)
                    {
                        if (hit[i].CompareTag("Grass"))
                        {
                            if (Input.GetKeyDown(KeyCode.LeftControl))
                            {
                                hit[i].GetComponent<FarmingObject>().Digging();
                            }
                            break;
                        }
                    }
                }   
            }

            
            

        }
    }
}