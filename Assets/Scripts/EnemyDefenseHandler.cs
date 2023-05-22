using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefenseHandler : MonoBehaviour
{
    [SerializeField] float defenseLevelTime = 10f;

    float passedTime;

    int currentDefenseLevel = 1;

    void Update()
    {
        if (passedTime >= defenseLevelTime)
        {
            currentDefenseLevel++;
            passedTime = 0;
        }

        passedTime += Time.deltaTime;
    }

    public int GetDefenseLevel()
    {
        return currentDefenseLevel;
    }

    public void ResetDefenseLevel()
    {
        currentDefenseLevel = 1;
        passedTime = 0;
    }
}
