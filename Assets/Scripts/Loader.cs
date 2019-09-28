﻿using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject soundManager; 

    void Awake()
    {
        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }
    }
}