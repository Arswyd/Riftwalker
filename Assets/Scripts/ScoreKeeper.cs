using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    static ScoreKeeper instance;
    int currentScore = 0;
    int highScore = 0;
    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetCurrentScore()
    {
        currentScore = 0;
    }

    public void IncreaseCurrentScore()
    {
        currentScore++;     
    }

    public void UpdateHighScore()
    {
        if(currentScore > highScore)
            highScore = currentScore;
    }

    public int GetCurrentScore()
    {
        return currentScore * 100;
    }

    public int GetHighScore()
    {
        return highScore * 100;
    }
}
