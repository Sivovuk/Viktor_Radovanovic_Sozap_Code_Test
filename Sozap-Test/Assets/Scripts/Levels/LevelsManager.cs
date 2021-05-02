using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    [SerializeField]
    public GameObject level;

    public List<GameObject> tiles = new List<GameObject>();
    [Space(20)]
    public List<GameObject> selectedTiles = new List<GameObject>();

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
        tiles = level.GetComponent<LevelData>().GetTiles;
    }

    public void FinishLevel(int score) 
    {
        if (score >= level.GetComponent<LevelData>().GetBoxes)
        {
            
        }
    }

    #endregion

    #region Tiles

    public GameObject GetTile(string tag, Vector2 position)
    {

        for (int i = 0; i < tiles.Count; i++)
        {
            if (tag != null && tiles[i].CompareTag(tag))
            {
                selectedTiles.Add(tiles[i]);
            }
            else if (position != null && (Vector2)tiles[i].transform.position == position)
            {
                selectedTiles.Add(tiles[i]);
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

        foreach (GameObject tileHolder in tiles)
        {
            if (tileHolder.CompareTag("Holder-tile"))
            {
                foreach (GameObject tileBox in tiles)
                {
                    if (tileBox.CompareTag("Box-tile"))
                    {
                        Debug.LogError(tileHolder.transform.position + " " + tileBox.transform.position);
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
