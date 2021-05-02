using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject controlers;

    private void Start()
    {
#if UNITY_ANDROID || UNITY_IOS
        controlers.SetActive(true);
#endif
    }

    private void Update()
    {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        CheckForInputs();
#endif
    }

    private void CheckForInputs()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Move(1);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Move(2);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Move(3);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Move(4);
        }
    }

    public void Move(int direction)
    {
        GameObject tile = null;
        Vector2 newPosition = new Vector2();

        if (direction == 1)
        {
            newPosition = new Vector2(transform.position.x, transform.position.y + 1);
        }
        else if (direction == 2)
        {
            newPosition = new Vector2(transform.position.x + 1, transform.position.y);
        }
        else if (direction == 3)
        {
            newPosition = new Vector2(transform.position.x, transform.position.y - 1);
        }
        else if (direction == 4)
        {
            newPosition = new Vector2(transform.position.x - 1, transform.position.y);
        }

        tile = LevelsManager.Instance.GetTile(null, newPosition);

        LevelsManager.Instance.ResetList();

        if (tile == null) return;



        if (tile.CompareTag("Grass-tile") || tile.CompareTag("Holder-tile"))
        {
            transform.position = newPosition;
        }
        else if (tile.CompareTag("Box-tile"))
        {
            //Debug.Log("Pomera kutiju");

            bool tileBool = tile.GetComponent<TileManager>().MoveTile(direction);

            if (tileBool)
            {
                transform.position = newPosition;
            }
        }
        else
        {
            LevelsManager.Instance.ResetList();
        }
    }
}
