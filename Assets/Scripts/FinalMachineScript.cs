using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.UI;

public class FinalMachineScript : MonoBehaviour
{
    public GameObject recipcheck;
    public GameObject mushcheck;
    public GameObject rockcheck;
    public GameObject treecheck;

    public float craftTime;
    public float destroyTime;
    public float workTime;

    public enum MachineState
    {
        None,
        Working,
        Destroying
    }
    
    public MachineState state;

    public GameObject production;

    public GameObject recipes;
    
    
    public GameObject prfGaugeBar;
    public GameObject canvas;
    private RectTransform gaugeBar;
    public float height = 0.0f;
    private Image nowGaugebar;
    

    void Start()
    {
        gaugeBar = Instantiate(prfGaugeBar, canvas.transform).GetComponent<RectTransform>();
        nowGaugebar = gaugeBar.transform.GetChild(0).GetComponent<Image>();
        state = MachineState.None;
    }
    
    void Update()
    {
        GaugeBar();
        if (state != MachineState.None)
        {
            workTime += Time.deltaTime;
        }
        if (state == MachineState.Destroying)
        {
            if (workTime >= destroyTime)
            {
                ChildDestroy();
                state = MachineState.None;
                workTime = 0;
            }
        }
    }
    
    void GaugeBar()
    {
        Vector3 _gaugeBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        gaugeBar.position = _gaugeBarPos;

        if (state == MachineState.None)
        {
            gaugeBar.gameObject.SetActive(false);
        }
        else
        {
            gaugeBar.gameObject.SetActive(true);
        }

        if (state == MachineState.Working)
        {
            nowGaugebar.fillAmount = workTime / craftTime;   
            nowGaugebar.color= Color.white;
        }
        else if (state == MachineState.Destroying)
        {
            nowGaugebar.fillAmount = 1 - (workTime / destroyTime);
            nowGaugebar.color= Color.red;
        }
    }
    
    public void SubCount(GameObject hand)      //????????? ?????? 
    {
        GameObject playerItem;
        playerItem = hand.transform.GetChild(0).gameObject;
        
        if (this.transform.childCount < recipes.GetComponent<RecipeScript>().needNum && state == MachineState.None) //????????? ????????? ????????? ????????? ???????????? ??????????
        {
            for (int i = 0; i < recipes.GetComponent<RecipeScript>().nowRecipe.Count; i++) //?????? ???????????? ??????(????????????) ??? ?????? ????????? ??????
            {
                if (playerItem.name == recipes.GetComponent<RecipeScript>().nowRecipe[i].name) //????????? ???????????? ??????????????? ????????? ?????? ???????????? ?????? ????????? ????????? ????????? ??????????
                {
                    for (int j = 0; j < this.transform.childCount; j++)
                    {
                        if (this.transform.GetChild(j).name == playerItem.name) // ????????? ?????? ?????? ??? ????????? ???????????? ??? ???????????? return ??????.
                        {
                            return;
                        }
                    }
                    playerItem.transform.SetParent(this.transform); //???????????? ??? ???????????? ????????? ?????????.
                    playerItem.SetActive(false);   
                }
            }
        }
        
    }

    public void CraftOn()   //?????? ??????
    {
        if (recipes.GetComponent<RecipeScript>().needNum == this.transform.childCount)
        {
            if (state == MachineState.None)
            {
                Invoke("Crafting", craftTime);
                state = MachineState.Working;
            }   
        }
    }

    public void PickUp(GameObject hand) //?????????
    {
        if (state == MachineState.Destroying)
        {
            CreateDone(hand);
            recipcheck.GetComponent<RecipeScript>().recipeOrder++;
            mushcheck.GetComponent<RecipeDawnCheck>().checkInit();
            rockcheck.GetComponent<RecipeDawnCheck>().checkInit();
            treecheck.GetComponent<RecipeDawnCheck>().checkInit();
        }
    }

    public void Crafting()      //???????????? ??? ????????? ????????? ??????
    {
        state = MachineState.Destroying;
        workTime = 0;
    }
    

    public void CreateDone(GameObject hand)    //????????? ??????
    {
        var go =Instantiate(production, Vector2.zero, quaternion.identity);
        
        int index = go.name.IndexOf("(Clone)"); //???????????? ?????? ????????? ??????
        if (index > 0)
        {
            go.name = go.name.Substring(0, index);
        }
        
        go.transform.SetParent(hand.transform);
        go.transform.localPosition = Vector2.zero;
        go.layer = 0;
        state = MachineState.None;
        workTime = 0;
        ChildDestroy();
    }
    
    public void ChildDestroy() //?????? ?????? and ????????? ?????????
    {
        for (int i = 0; i < this.transform.childCount; i++)
        { 
            Destroy(this.transform.GetChild(i).gameObject);
        }
        recipes.GetComponent<RecipeScript>().RecipeSetting();
    } 
    
}
