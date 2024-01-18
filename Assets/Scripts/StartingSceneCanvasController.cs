using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartingSceneCanvasController : MonoBehaviour
{
    public Button playButton;
    public Button exitButton;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        playButton.onClick.AddListener(ClickedPlayButton);
        exitButton.onClick.AddListener(ExitGame);
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void ClickedPlayButton()
    {
        SceneManager.LoadScene("LevelSelector");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
