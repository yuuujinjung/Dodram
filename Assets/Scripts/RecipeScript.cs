using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeScript : MonoBehaviour
{
    [SerializeField] private Text text_NeedItemName;
    [SerializeField] private Text text_NeedItemNumber;

    [SerializeField] private GameObject go_BaseToolTip;

    private void Clear()
    {
        text_NeedItemName.text = "";
        text_NeedItemNumber.text = "";
    }

    public void ShowTooltip(string[] _needItemName, int[] _needItemNumber)
    {
        Clear();
        go_BaseToolTip.SetActive(true); // ���� UI Ȱ��ȭ

        for (int i = 0; i < _needItemNumber.Length; i++)
        {
            text_NeedItemName.text += _needItemName[i] + "\n";
            text_NeedItemNumber.text += " x " + _needItemNumber[i] + "\n";
        }
    }

    public void HideToolTip()
    {
        Clear();
        go_BaseToolTip.SetActive(false); // ���� UI ��Ȱ��ȭ
    }
}
