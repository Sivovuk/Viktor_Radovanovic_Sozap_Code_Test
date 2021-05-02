using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveJSON : MonoBehaviour
{
    string path = "Assets/Resource/Levels-JSON";

    IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
        SerializeLevelData(LevelsManager.Instance.level.GetComponent<LevelData>());
    }

    public void SerializeLevelData(LevelData levelData)
    {
        LevelDataEnteti temp = new LevelDataEnteti(levelData.GetTiles, levelData.GetBoxes, 0, 1);

        string json = JsonUtility.ToJson(temp);

        SaveData(json);
    }

    public void SaveData(string json) 
    {
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(json);
        writer.Close();
    }
}
