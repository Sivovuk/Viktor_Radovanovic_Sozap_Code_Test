using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroySingletone : MonoBehaviour
{
    private static DontDestroySingletone instance;

    public static DontDestroySingletone Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
