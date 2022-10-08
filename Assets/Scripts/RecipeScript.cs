using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RecipeScript : MonoBehaviour
{
    private int[] randArray;
    
    public GameObject[] ingredientArray;

    public string[] ingredientStringArray;
    
    public int needNum;
    public List<GameObject> nowRecipe = new List<GameObject>();
    public TextMeshProUGUI recipeText;
    
    
    private void Start()
    {
        RecipeSetting();

    }

    private void Update()
    {
        
    }

    void RecipeTextUpdate()
    {
        string ingredientlist = "";
        
        for (int i = 0; i < randArray.Length; i++)
        {
            ingredientlist += (ingredientStringArray[randArray[i]].ToString() + "\n");
        }

        recipeText.text =
            "<recipe>\n\n" +
            ingredientlist +
            "\n need to processing";
    }

    public void RecipeSetting()
    {
        randArray = GetRandomInt(needNum, 0, ingredientArray.Length);
        
        nowRecipe.Clear();
        
        for (int i = 0; i < randArray.Length; i++)
        {
            nowRecipe.Add(ingredientArray[randArray[i]]);
        }
        RecipeTextUpdate();
    }

    public int[] GetRandomInt(int length, int min, int max)
    {
        int[] randArray = new int[length];
        bool isSame;

        for (int i = 0; i < length; i++)
        {
            while (true)
            {
                randArray[i] = Random.Range(min, max);
                isSame = false;

                for (int j = 0; j < i; j++)
                {
                    if (randArray[j] == randArray[i])
                    {
                        isSame = true;
                        break;
                    }
                }
                if(!isSame) break;
            }
        }
        return randArray;
    }
    
    
    
    
}
