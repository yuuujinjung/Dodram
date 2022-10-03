using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Serialization;

public class PickUpScript : MonoBehaviour
{
    public Vector2 size;
    public Vector3 boxTransform;
    public LayerMask whatIsLayer;
    public GameObject Hand;
    GameObject changeHold;


    public bool isHold;

    [FormerlySerializedAs("DiggingPer")] public float GaugePer;

    public GameObject prfGaugeBar;
    public GameObject canvas;

    private RectTransform gaugeBar;
    public float height = 0.0f;
    private Image nowGaugebar;

    public float diggingSpd = 20.0f;

    [SerializeField] private GameObject targetObject;
    [SerializeField] private GameObject targetEndObject;

    [SerializeField] private Material whiteMaterial;
    [SerializeField] private Material originalMaterial;

    // Start is called before the first frame update
    void Start()
    {
        isHold = false;
        GaugePer = 0.0f;
        gaugeBar = Instantiate(prfGaugeBar, canvas.transform).GetComponent<RectTransform>();
        nowGaugebar = gaugeBar.transform.GetChild(0).GetComponent<Image>();
    }
    
    // Update is called once per frame
    void Update()
    {
        Interactive(); //상호작용

        Vector3 _gaugeBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        gaugeBar.position = _gaugeBarPos;

        if (GaugePer <= 0)
        {
            gaugeBar.gameObject.SetActive(false);
        }
        else
        {
            gaugeBar.gameObject.SetActive(true);
        }

        nowGaugebar.fillAmount = GaugePer / 100.0f;
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + boxTransform, size);
    }

    void Interactive()
    {
        Collider2D hit = Physics2D.OverlapBox(transform.position+ boxTransform, size, 0, whatIsLayer);

        //d
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
            GaugePer = 0.0f;
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
            GaugePer = 0.0f;
        }
        
        
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (hit != null)
            {   
                if (isHold == true)
                {
                    //if (Hand.transform.GetChild(0).name == "Axe") //나무 캐기
                    //{
                    //    if (hit.CompareTag("Tree"))
                    //    {
                    //        GaugePer += diggingSpd * Time.deltaTime;
                    //        if (GaugePer >= 100)
                    //        {
                    //            hit.GetComponent<FarmingObject>().Digging();
                    //            GaugePer = 0.0f;
                    //        }
                    //    }
                    //}
                    //else if (Hand.transform.GetChild(0).name == "PickAxe") //돌 캐기
                    //{
                    //    if (hit.CompareTag("Stone"))
                    //    {
                    //        GaugePer += diggingSpd * Time.deltaTime;
                    //        if (GaugePer >= 100)
                    //        {
                    //            hit.GetComponent<FarmingObject>().Digging();
                    //            GaugePer = 0.0f;
                    //        }
                    //    }
                    //}
                    //else if (Hand.transform.GetChild(0).name == "Scythe")   //풀 베기
                    //{
                    //    if (hit.CompareTag("Grass"))
                    //    {
                    //        GaugePer += diggingSpd * Time.deltaTime;
                    //        if (GaugePer >= 100)
                    //        {
                    //            hit.GetComponent<FarmingObject>().Digging();
                    //            GaugePer = 0.0f;
                    //        }
                    //    }
                    //}
                    if (Hand.transform.GetChild(0).name == "wood")  //나무 기계에 넣기
                    {
                        if (hit.gameObject.name == "Sawmill")
                        {
                            hit.GetComponent<MachineScript>().SubCount();
                            isHold = false;
                        }

                    }
                    else if (Hand.transform.GetChild(0).name == "cobblestone")  //돌 기계에 넣기
                    {
                        if (hit.gameObject.name == "Stonecutter")
                        {
                            hit.GetComponent<MachineScript>().SubCount();
                            isHold = false;
                        }
                    }
                    else if (Hand.transform.GetChild(0).name == "chip")     //풀 기계에 넣기
                    {
                        if (hit.gameObject.name == "Mill")
                        {
                            hit.GetComponent<MachineScript>().SubCount();
                            isHold = false;
                        }
                    }
                }
                else
                {

                }
            }
            else
            {
                if (isHold == true)
                {

                }
                else
                {

                }
            }
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            if (hit != null)
            {
                if (isHold == true)
                {
                    if (Hand.transform.GetChild(0).name == "Axe") //나무 캐기
                    {
                        if (hit.CompareTag("Tree"))
                        {
                            GaugePer += diggingSpd * Time.deltaTime;
                            if (GaugePer >= 100)
                            {
                                hit.GetComponent<FarmingObject>().Digging();
                                GaugePer = 0.0f;
                            }
                        }
                    }
                    else if (Hand.transform.GetChild(0).name == "PickAxe") //돌 캐기
                    {
                        if (hit.CompareTag("Stone"))
                        {
                            GaugePer += diggingSpd * Time.deltaTime;
                            if (GaugePer >= 100)
                            {
                                hit.GetComponent<FarmingObject>().Digging();
                                GaugePer = 0.0f;
                            }
                        }
                    }
                    else if (Hand.transform.GetChild(0).name == "Scythe")   //풀 베기
                    {
                        if (hit.CompareTag("Grass"))
                        {
                            GaugePer += diggingSpd * Time.deltaTime;
                            if (GaugePer >= 100)
                            {
                                hit.GetComponent<FarmingObject>().Digging();
                                GaugePer = 0.0f;
                            }
                        }
                    }
                }
            }
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            GaugePer = 0.0f;
        }
        else if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (hit != null)
            {
                if (isHold == true)
                {
                    if (hit.CompareTag("tool") || hit.CompareTag("item"))
                    {
                        //바꾸기
                        changeHold = hit.gameObject;
                        Hand.transform.GetChild(0).gameObject.layer = 6;
                        Hand.transform.DetachChildren();
                        changeHold.transform.SetParent(Hand.transform);
                        changeHold.transform.localPosition = Vector2.zero;
                        changeHold.layer = 0;
                    }
                }
                else
                {
                    if (hit.CompareTag("tool") || hit.CompareTag("item"))
                    {
                        //들기
                        hit.gameObject.transform.SetParent(Hand.transform);
                        hit.transform.localPosition = Vector2.zero;
                        hit.gameObject.layer = 0; //Default
                        isHold = true;
                    }
                }
            }
            else
            {
                if (isHold == true)
                {
                    //놓기
                    Hand.transform.GetChild(0).gameObject.layer = 6;
                    Hand.transform.DetachChildren();
                    isHold = false;
                    return;
                }
                else
                {

                }
            }
        }
    }
}