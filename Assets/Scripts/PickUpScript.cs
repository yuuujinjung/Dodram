using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PickUpScript : MonoBehaviour
{
    public Vector2 size;
    public Vector3 boxTransform;
    public LayerMask whatIsLayer;
    public GameObject Hand;

    public bool isHold;

    [SerializeField] private GameObject targetObject;
    [SerializeField] private GameObject targetEndObject;

    [SerializeField] private Material whiteMaterial;
    [SerializeField] private Material originalMaterial;

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
        Gizmos.DrawWireCube(transform.position + boxTransform, size);
    }

    void Interactive()
    {
        Collider2D hit = Physics2D.OverlapBox(transform.position+ boxTransform, size, 0, whatIsLayer);

        if (Physics2D.OverlapBox(transform.position+ boxTransform, size, 0, whatIsLayer) == null)
        {
            if(targetObject != null) 
            {
                targetObject.GetComponent<SpriteRenderer>().material = originalMaterial;
                targetObject = null;
            }
            if (targetEndObject != null)
            {
                targetEndObject.GetComponent<SpriteRenderer>().material = originalMaterial;
                targetEndObject = null;
            }
        }
        else if (targetObject != hit.gameObject)
        {
            targetEndObject = targetObject;
            targetObject = hit.gameObject;
            
            if (targetObject != null)
            {
                targetObject.GetComponent<SpriteRenderer>().material = whiteMaterial;
            }

            if (targetEndObject != null)
            {
                targetEndObject.GetComponent<SpriteRenderer>().material = originalMaterial;
            }
        }
        
        

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (isHold == true)
            {
                Hand.transform.GetChild(0).gameObject.layer = 6;
                Hand.transform.DetachChildren();
                isHold = false;
                return;
            }
        }

        if (hit != null)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (hit.CompareTag("tool") || hit.CompareTag("item"))
                {
                    hit.gameObject.transform.SetParent(Hand.transform);
                    hit.transform.localPosition = Vector2.zero;
                    hit.gameObject.layer = 0; //Default
                    isHold = true;
                }
            }
            
            if (isHold == true)
            {
                if (Hand.transform.GetChild(0).name == "Axe")
                {
                    if (hit.CompareTag("Tree"))
                    {
                        if (Input.GetKeyDown(KeyCode.LeftControl))
                        {
                            hit.GetComponent<FarmingObject>().Digging();
                        }
                    }
                }
                
                if (Hand.transform.GetChild(0).name == "PickAxe")
                {
                    if (hit.CompareTag("Stone"))
                    {
                        if (Input.GetKeyDown(KeyCode.LeftControl))
                        {
                            hit.GetComponent<FarmingObject>().Digging();
                        }
                    }

                }
                if (Hand.transform.GetChild(0).name == "Scythe")
                {
                    if (hit.CompareTag("Grass"))
                    {
                        if (Input.GetKeyDown(KeyCode.LeftControl))
                        {
                            hit.GetComponent<FarmingObject>().Digging();
                        }
                    }

                }   
            }

        }
    }
}