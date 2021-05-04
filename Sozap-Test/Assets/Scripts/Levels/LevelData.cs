using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    [SerializeField]
    private LevelDataEnteti levelDataEnteti = new LevelDataEnteti();

    [SerializeField]
    private int boxes;

    [Space(10)]

    [SerializeField]
    private List<GameObject> tiles = new List<GameObject>();
    [SerializeField]
    private Vector2 playerStartPos = new Vector2();

    public int GetLevelIndex { get { return levelDataEnteti.levelIndex; } set { levelDataEnteti.levelIndex = value; } }
    public int GetBestTime { get { return levelDataEnteti.bestTime; } set { levelDataEnteti.bestTime = value; } }
    public int GetTimePlayed { get { return levelDataEnteti.timePlayed; } set { levelDataEnteti.timePlayed = value; } }
    public int GetBoxes { get { return boxes; } set { boxes = value; } }
    public int GetLevelPass { get { return levelDataEnteti.levelPassed; } set { levelDataEnteti.levelPassed = value; } }

    public List<GameObject> GetTiles { get { return tiles; } set { tiles = value; } }

    public LevelDataEnteti GetLevelDataEnteti { get { return levelDataEnteti; } set { levelDataEnteti = value; } }

    public Vector2 GetPlayerStartPosition { get { return playerStartPos; } set { playerStartPos = value; } }
}

[System.Serializable]
public class LevelDataEnteti
{
    public int levelIndex;
    public int bestTime;
    public int timePlayed;
    public int levelPassed = 0; //  0 = no, 1 = yes
}