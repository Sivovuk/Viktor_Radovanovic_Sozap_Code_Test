using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public LevelDataEnteti levelData;

    public int GetBoxes { get { return levelData.boxes; } set { levelData.boxes = value; } }

    public List<GameObject> GetTiles { get { return levelData.tiles; } set { levelData.tiles = value; } }
}

[System.Serializable]
public class LevelDataEnteti
{
    public int levelIndex;
    public int version;

    public int boxes;
    public int bestTime;
    public int timePlayed;

    [Space(10)]

    public List<GameObject> tiles = new List<GameObject>();

    public LevelDataEnteti(List<GameObject> tiles, int boxes, int bestTime, int timePlayed)
    {
        this.tiles = tiles;
        this.boxes = boxes;
        this.bestTime = bestTime;
        this.timePlayed = timePlayed;
    }
}