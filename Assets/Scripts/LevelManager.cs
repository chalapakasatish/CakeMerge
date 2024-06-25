using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] levels;
    [SerializeField]private int levelCount;
    public TMP_Text levelText;
    public List<HowManyChances> howManyChances = new List<HowManyChances>();
    public int LevelCount { get => levelCount; set => levelCount = value; }

    private void Start()
    {
        Application.targetFrameRate = 300;
        levelCount = PlayerPrefs.GetInt("Levels");
        GetLevel(levelCount);
    }
    public void GetLevel(int num)
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (num == i)
            {
                levels[i].SetActive(true);
            }
            else
            {
                levels[i].SetActive(false);
            }
        }
        levelText.text = "LEVEL " + (levelCount + 1);
    }
}
[Serializable]
public struct HowManyChances
{
    public int howmanyChances;
}
