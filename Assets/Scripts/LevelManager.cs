using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<LevelDetails> levelDetails;
    private int selectedLevel;
    public GameplayCanvasController gameplayCanvasController;
    public PipeManager2 pipeManager;
    private LevelDetails currentLevelDetails;
    // Start is called before the first frame update
    void Start()
    {

        selectedLevel = PlayerPrefs.GetInt("SelectedLevel", 1);
      // selectedLevel = 3;
        foreach(LevelDetails levelDetail in levelDetails)
        {
            levelDetail.levelItems.SetActive(false);
        }
        currentLevelDetails = levelDetails[selectedLevel - 1];
        currentLevelDetails.levelItems.SetActive(true);
        gameplayCanvasController.SetTimerDetails(currentLevelDetails.timer);
        pipeManager.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
[Serializable]
public class LevelDetails {
    public GameObject levelItems;
    public float timer;
}
