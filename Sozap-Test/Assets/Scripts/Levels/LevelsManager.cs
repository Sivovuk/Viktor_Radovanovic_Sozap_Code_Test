using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    private int timer;
    private float timePass;
    private bool isLevelStarted = false;

    [SerializeField]
    private List<GameObject> LevelPrefabs = new List<GameObject>();

    public List<GameObject> GetLevelPrefabs { get { return LevelPrefabs; } set { LevelPrefabs = value; } }

    [SerializeField]
    private List<LevelData> LoadedLevelData = new List<LevelData>();

    [Space(10)]

    public GameObject currentLevel;
    public LevelData currentLevelData;

    [Space(10)]

    public List<GameObject> selectedTiles = new List<GameObject>();

    [Space(10)]

    public GameObject levelPassedUI;
    public GameObject gamePassedUI;

    public GameObject player;

    [Space(10)]

    public TextMeshProUGUI textTimer;

    private static LevelsManager instance;

    public static LevelsManager Instance => instance;

    private void Awake()
    {
        instance = this;

        LoadLevel();
    }

    private void Update()
    {
        if (isLevelStarted)
        {
            timePass += Time.deltaTime;

            if (timePass >= 1) 
            {
                timer++;
                int minutes = (int)(timer / 60);
                int sec = Mathf.Abs(((int)(timer / 60) * 60) - timer);
                textTimer.text = string.Format("{0:D2}:{1:D2}", minutes, sec);
                timePass = 0;
            }
        }
    }

    #region Load Levels

    public void LoadLevel()
    {
        List<LevelDataEnteti> temp = new List<LevelDataEnteti>();

        temp = LoadJSON.Instance.LoadData();

        if (temp != null)
        {
            LoadedLevelData = new List<LevelData>();

            for (int i = 0; i < temp.Count; i++)
            {
                if (LoadedLevelData.Count < 4)
                {
                    LoadedLevelData.Add(LevelPrefabs[i].GetComponent<LevelData>());
                    LoadedLevelData[i].GetLevelDataEnteti = temp[i];
                }
                else
                {
                    LoadedLevelData[i].GetLevelDataEnteti = temp[i];
                }
            }
        }

        CreateLevel();
    }

    #endregion

    #region Finish Level

    public void FinishLevel(int score)
    {
        if (score >= currentLevel.GetComponent<LevelData>().GetBoxes)
        {
            isLevelStarted = false;

            int levelIndex = PlayerPrefs.GetInt(SelectLevel.LEVEL_KEY) - 1;

            LoadedLevelData[levelIndex].GetTimePlayed++;
            LoadedLevelData[levelIndex].GetLevelPass = 1;

            if (LoadedLevelData[levelIndex].GetBestTime > timer || LoadedLevelData[levelIndex].GetBestTime == 0)
            {
                LoadedLevelData[levelIndex].GetBestTime = timer;
                timer = 0;
            }

            List<LevelDataEnteti> temp = new List<LevelDataEnteti>();

            for (int i = 0; i < LoadedLevelData.Count; i++)
            {
                temp.Add(LoadedLevelData[i].GetLevelDataEnteti);
            }

            SaveJSON.Instance.SaveData(temp);

            if (currentLevelData.GetLevelIndex < 4)
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

    #region Create Levels

    public void CreateLevel()
    {
        selectedTiles = new List<GameObject>();

        int levelIndex = PlayerPrefs.GetInt(SelectLevel.LEVEL_KEY) - 1;

        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        GameObject spawnLevel = Instantiate(LevelPrefabs[levelIndex]);
        spawnLevel.transform.parent = transform;

        currentLevel = spawnLevel;

        currentLevelData = spawnLevel.GetComponent<LevelData>();

        currentLevelData.GetLevelDataEnteti.bestTime = LoadedLevelData[levelIndex].GetLevelDataEnteti.bestTime;

        currentLevelData.GetLevelDataEnteti.levelPassed = LoadedLevelData[levelIndex].GetLevelDataEnteti.levelPassed;

        currentLevelData.GetLevelDataEnteti.timePlayed = LoadedLevelData[levelIndex].GetLevelDataEnteti.timePlayed;


        player.transform.position = currentLevelData.GetPlayerStartPosition;

        isLevelStarted = true;
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
