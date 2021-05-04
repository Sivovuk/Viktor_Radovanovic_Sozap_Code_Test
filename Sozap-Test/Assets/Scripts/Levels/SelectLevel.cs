using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevel : MonoBehaviour
{
    public const string LEVEL_KEY = "selected-Level";

    public void BtnSelectLevel(int levelIndex) 
    {
        PlayerPrefs.SetInt(LEVEL_KEY, levelIndex);
        MenuManager.Instance.LoadScene(1);
    }
}
