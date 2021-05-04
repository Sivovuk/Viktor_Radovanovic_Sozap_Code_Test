using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectLevel : MonoBehaviour
{
    public const string LEVEL_KEY = "selected-Level";

    public List<TextMeshProUGUI> bestTimeText = new List<TextMeshProUGUI>();

    private void Start()
    {
        List<LevelDataEnteti> temp = LoadJSON.Instance.LoadData();

        for (int i = 0; i < temp.Count; i++)
        {
            if (temp[i].bestTime > 0)
            {
                int minutes = (int)(temp[i].bestTime / 60);
                int sec = Mathf.Abs(((int)(temp[i].bestTime / 60) * 60) - temp[i].bestTime);
                bestTimeText[i].text = "Best time : " + string.Format("{0:D2}:{1:D2}", minutes, sec);
            }
            else 
            {
                bestTimeText[i].text = "";
            }
        }
    }

    public void BtnSelectLevel(int levelIndex) 
    {
        PlayerPrefs.SetInt(LEVEL_KEY, levelIndex);
        MenuManager.Instance.LoadScene(1);
    }
}
