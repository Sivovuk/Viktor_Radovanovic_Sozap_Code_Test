using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    private int timer;

    [SerializeField]
    private List<GameObject> levels = new List<GameObject>();

    public List<GameObject> Getlevels { get { return levels; } set { levels = value; } }

    private List<LevelData> levelData = new List<LevelData>();

    [SerializeField]
    public GameObject currentLevel;
    [SerializeField]
    public LevelData currentLevelData;

    [Space(20)]
    public List<GameObject> selectedTiles = new List<GameObject>();

    public GameObject levelPassedUI;
    public GameObject gamePassedUI;

    public GameObject player;

    private static LevelsManager instance;

    public static LevelsManager Instance => instance;

    private void Awake()
    {
        instance = this;

        LoadLevel();
    }


    #region Load Levels

    public void LoadLevel()
    {
        selectedTiles = new List<GameObject>();

        int levelIndex = PlayerPrefs.GetInt(SelectLevel.LEVEL_KEY) - 1;

        List<LevelDataEnteti> temp = new List<LevelDataEnteti>();
        temp = LoadJSON.Instance.LoadData();

        if (temp != null)
        {
            for (int i = 0; i < levelData.Count; i++)
            {
                levelData[i].GetLevelDataEnteti = temp[i];
            }
        }

        currentLevel = levels[levelIndex];
        currentLevelData = levels[levelIndex].GetComponent<LevelData>();

        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        GameObject spawnLevel = Instantiate(levels[levelIndex]);
        spawnLevel.transform.parent = transform;

        currentLevel = spawnLevel;
        currentLevelData = spawnLevel.GetComponent<LevelData>();

        player.transform.position = currentLevelData.GetPlayerStartPosition;
    }

    public void FinishLevel(int score)
    {
        if (score >= currentLevel.GetComponent<LevelData>().GetBoxes)
        {
            int levelIndex = PlayerPrefs.GetInt(SelectLevel.LEVEL_KEY) - 1;

            Debug.LogError(levelIndex);

            levels[levelIndex].GetComponent<LevelData>().GetTimePlayed++;
            levels[levelIndex].GetComponent<LevelData>().GetBestTime = timer;
            levels[levelIndex].GetComponent<LevelData>().GetLevelPass = 1;

            List<LevelDataEnteti> temp = new List<LevelDataEnteti>();

            for (int i = 0; i < levelData.Count; i++)
            {
                temp.Add(levelData[i].GetLevelDataEnteti);
            }

            SaveJSON.Instance.SaveData(temp);

            if (currentLevelData.GetLevelIndex < 3)
            {
                PlayerPrefs.SetInt(SelectLevel.LEVEL_KEY, currentLevelData.GetLevelIndex + 1);
                levelPassedUI.SetActive(true);
            }
            else
            {
                gamePassedUI.SetActive(true);
            }
        }
    }

    #endregion

    #region Tiles

    public GameObject GetTile(string tag, Vector2 position)
    {
        for (int i = 0; i < currentLevelData.GetTiles.Count; i++)
        {
            if (tag != null && currentLevelData.GetTiles[i].CompareTag(tag))
            {
                selectedTiles.Add(currentLevelData.GetTiles[i]);
            }
            else if (position != null && (Vector2)currentLevelData.GetTiles[i].transform.position == position)
            {
                selectedTiles.Add(currentLevelData.GetTiles[i]);
            }
        }

        if (selectedTiles != null)
        {
            foreach (GameObject tile in selectedTiles)
            {
                if (tile.CompareTag("Box-tile"))
                {
                    return tile;
                }
            }

            foreach (GameObject tile in selectedTiles)
            {
                if (tile.CompareTag("Holder-tile"))
                {
                    return tile;
                }
            }

            foreach (GameObject tile in selectedTiles)
            {
                return tile;
            }
        }

        Debug.LogError("Nije nasao tile! Ne sme da se desava!");
        return new GameObject();
    }

    public void ResetList()
    {
        selectedTiles = new List<GameObject>();
    }

    #endregion

    #region Score

    [SerializeField]
    private int score = 0;

    public void CheckScore()
    {
        score = 0;

        foreach (GameObject tileHolder in currentLevelData.GetTiles)
        {
            if (tileHolder.CompareTag("Holder-tile"))
            {
                foreach (GameObject tileBox in currentLevelData.GetTiles)
                {
                    if (tileBox.CompareTag("Box-tile"))
                    {
                        if (tileHolder.transform.position == tileBox.transform.position)
                        {
                            score++;
                            FinishLevel(score);
                        }
                    }
                }
            }
        }
    }

    #endregion
}
