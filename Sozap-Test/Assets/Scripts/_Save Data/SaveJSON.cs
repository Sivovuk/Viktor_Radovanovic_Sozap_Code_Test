using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveJSON : MonoBehaviour
{
    string path = "Assets/Resource/Levels-JSON";

    private static SaveJSON instance;

    public static SaveJSON Instance => instance;

    public void Awake() 
    {
        instance = this;
    }

    public string SerializeLevelData(LevelDataEnteti levelDataEnteti)
    {
        string json = JsonUtility.ToJson(levelDataEnteti);
        return json;
    }

    public void SaveData(List<LevelDataEnteti> levelDataEnteti)
    {
        if (levelDataEnteti != null)
        {
            StreamWriter writer = new StreamWriter(path, false);

            foreach (LevelDataEnteti data in levelDataEnteti)
            {
                //Debug.Log(data.levelIndex);
                //Debug.Log(data.timePlayed);
                //Debug.Log(data.levelPassed);
                string temp = SerializeLevelData(data);
                writer.WriteLine(temp);
            }
            writer.Close();
        }
        else 
        {
            Debug.LogError("nema podatak za upis");
        }
    }

    public List<LevelDataEnteti> FirstSave(List<GameObject> levelDataEnteti) 
    {
        List<LevelDataEnteti> levelData = new List<LevelDataEnteti>();

        foreach (GameObject data in levelDataEnteti)
        {
            levelData.Add(data.GetComponent<LevelData>().GetLevelDataEnteti);
        }

        SaveData(levelData);

        return levelData;
    }
}
