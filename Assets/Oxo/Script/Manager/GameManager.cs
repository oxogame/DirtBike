using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager instance;

    public bool isGameRunning;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        Application.targetFrameRate = 60;
    }

    public void StartGame()
    {
        Debug.Log("Start Game");
        isGameRunning = true;
    }

    public void FinishTheGame()
    {
        isGameRunning = false;
    }

    public void LevelComplated()
    {
        FinishTheGame();
        CanvasManager.instance.OpenFinishRect(true);
    }

    public void LevelFailed()
    {
        FinishTheGame();
        CanvasManager.instance.OpenFinishRect(false);
    }

}
