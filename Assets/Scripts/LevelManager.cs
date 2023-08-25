using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] levels;
    [SerializeField]private int levelCount;

    public int LevelCount { get => levelCount; set => levelCount = value; }

    private void Start()
    {
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
    }
}
