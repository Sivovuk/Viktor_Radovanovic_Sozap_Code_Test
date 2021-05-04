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
        StreamWriter writer = new StreamWriter(path, false);

        Debug.Log(levelDataEnteti);
        foreach (LevelDataEnteti data in levelDataEnteti)
        {
            string temp = SerializeLevelData(data);
            writer.WriteLine(temp);
        }
        writer.Close();
    }

    public void FirstSave(List<GameObject> levelDataEnteti) 
    {
        List<LevelDataEnteti> levelData = new List<LevelDataEnteti>();

        foreach (GameObject data in levelDataEnteti)
        {
            levelData.Add(data.GetComponent<LevelData>().GetLevelDataEnteti);
        }

        SaveData(levelData);
    }
}
