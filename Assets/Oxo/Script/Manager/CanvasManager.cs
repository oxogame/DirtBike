using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    [HideInInspector]
    public static CanvasManager instance;

    public GameObject mainMenuRect;
    public GameObject tapToPlayButton;
    public GameObject inGameRect;
    public GameObject inGameInfoText;
    public GameObject finishRect;
    public Text finishInfoText;
    public GameObject scoreText;
    public GameObject nextLevelButton;
    public GameObject retryLevelButton;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void TapToPlayButtonClick()
    {
        GameManager.instance.StartGame();
    }

    //this just call load scene
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenFinishRect(bool isSuccess)
    {
        if (isSuccess)
        {
            finishInfoText.text = "Congratulations";
            retryLevelButton.SetActive(false);
            nextLevelButton.SetActive(true);
        }
        else
        {
            finishInfoText.text = "Fail";
            retryLevelButton.SetActive(true);
            nextLevelButton.SetActive(false);
        }
        finishRect.SetActive(true);
    }

}
