using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelItem : MonoBehaviour
{
    public Image lockImage;
    public TMP_Text levelText;
    public Button button;
    public Action<LevelItem> OnItemClicked;
    private int level;
    private bool isLocked;

    public int Level { get => level; set => level = value; }

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(() => {
            if (!isLocked)
            {
                OnItemClicked?.Invoke(this);
            }
           
        });
    }
    public void SetLevel(int level)
    {
        this.level = level;
        levelText.text = level.ToString();
    }

    public void Setlocked(bool status)
    {
        isLocked = status;
        lockImage.gameObject.SetActive(isLocked);
        levelText.gameObject.SetActive(!isLocked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
