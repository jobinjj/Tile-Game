using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionController : MonoBehaviour
{
    public int numberOfLevels;
    public LevelItem levelItemPrefab;
    public List<LevelItem> levelItems;
    private int levelsUnlocked;
    public Button exitButton;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        levelsUnlocked = PlayerPrefs.GetInt("LevelsUnlocked", 1);
        //levelsUnlocked = 5;
        for(int i = 0; i < numberOfLevels; i++)
        {
            LevelItem levelItem = Instantiate(levelItemPrefab, transform);
            levelItem.SetLevel(i + 1);
            if (i < levelsUnlocked)
            {
                levelItem.Setlocked(false);
            }
            else
            {
                levelItem.Setlocked(true);
            }
            levelItem.OnItemClicked += OnLevelItemClicked;
            levelItems.Add(levelItem);
        }
        exitButton.onClick.AddListener(Exit);
    }

    private void Exit()
    {
        SceneManager.LoadScene("StartingScene");
    }

    public void OnLevelItemClicked(LevelItem item)
    {
        PlayerPrefs.SetInt("SelectedLevel", item.Level);
        SceneManager.LoadScene("GameScene");
    }

}
