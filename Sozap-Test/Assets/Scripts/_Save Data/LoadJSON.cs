using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadJSON : MonoBehaviour
{
    string path = "Assets/Resource/Levels-JSON";

    private static LoadJSON instance;

    public static LoadJSON Instance => instance;

    public void Awake()
    {
        instance = this;
    }

    public LevelDataEnteti DeserializeLevelData(string levelData)
    {
        LevelDataEnteti temp;

        temp = JsonUtility.FromJson<LevelDataEnteti>(levelData);

        return temp;
    }

    public List<LevelDataEnteti> LoadData()
    {
        if (!File.Exists(path))
        {
            List<LevelDataEnteti> temp = SaveJSON.Instance.FirstSave(LevelsManager.Instance.GetLevelPrefabs);
            return temp;
        }

        List<LevelDataEnteti> levelData = new List<LevelDataEnteti>();

        StreamReader reader = new StreamReader(path, true);


        for (int i = 0; i < 4; i++)
        {
            levelData.Add(DeserializeLevelData(reader.ReadLine()));
        }

        reader.Close();

        return levelData;
    }
}
