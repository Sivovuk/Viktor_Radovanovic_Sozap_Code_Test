using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    //                          ^
    // Direction :              |
    // 1 = up                   1
    // 2 = right        <-- 4       2 -->
    // 3 = down                 3
    // 4 = left                 |
    //                          V


    //private bool isBox, isPlaceHolder, isWall, isGrass = false;
    private bool isMoveable = false;

    private void Awake()
    {
        if (gameObject.CompareTag("Box-tile")) 
        {
            //isBox = true;
            isMoveable = true;
        }
        //else if (gameObject.CompareTag("Holder-tile"))
        //{
        //    isPlaceHolder = true;
        //}
        //else if (gameObject.CompareTag("Wall-tile"))
        //{
        //    isWall = true;
        //}
        //else if (gameObject.CompareTag("Grass-tile"))
        //{
        //    isGrass = true;
        //}
    }

    public bool MoveTile(int direction) 
    {
        if (isMoveable && direction == 1 || direction == 2 || direction == 3 || direction == 4)
        {
            if (CheckTheNextTile(direction))
            {
                LevelsManager.Instance.CheckScore();
                return true;
            }
            else return false;
        }
        else
        {
            Debug.LogError("Los index za pravac ili isMoveable je false!");
            return false;
        }
    }

    public bool CheckTheNextTile(int direction) 
    {
        Vector2 tilePosition = transform.position;
        float x;
        float y;

        if (direction == 1)
        {
            x = tilePosition.x;
            y = tilePosition.y + 1;
            //Debug.Log("pomeri na gore");
        }
        else if (direction == 2)
        {
            x = tilePosition.x + 1;
            y = tilePosition.y;
            //Debug.Log("pomeri na desno");
        }
        else if (direction == 3)
        {
            x = tilePosition.x;
            y = tilePosition.y - 1;
            //Debug.Log("pomeri na dole");
        }
        else if (direction == 4)
        {
            x = tilePosition.x - 1;
            y = tilePosition.y;
            //Debug.Log("pomeri na levo");
        }
        else 
        {
            Debug.LogError("Los pravac!");
            return false;
        }

        bool check = CheckTileOnPosition(new Vector2(x, y));

        if (check)
        {
            transform.position = new Vector2(x, y);
            return true;
        }
        else 
        {
            return false;
        }
    }

    public bool CheckTileOnPosition(Vector2 position)
    {
        GameObject tile = LevelsManager.Instance.GetTile(null, position);

        if (tile.tag == "Grass-tile" || tile.tag == "Holder-tile")
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
}
