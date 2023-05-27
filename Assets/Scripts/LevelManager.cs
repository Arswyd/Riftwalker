using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    ScoreKeeper scoreKeeper;
    AudioManager audioManager;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        audioManager = FindObjectOfType<AudioManager>();
        Debug.Assert(scoreKeeper != null);
    }

    public void LoadNewGame()
    {
        Debug.Assert(scoreKeeper != null);
        scoreKeeper.ResetCurrentScore();
        audioManager.SwapAudioSources();
        SceneManager.LoadScene(1);
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene(0);
        scoreKeeper.UpdateHighScore();
        audioManager.SwapAudioSources();
    }
}
