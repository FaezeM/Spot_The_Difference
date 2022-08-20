using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject highScoresTable;

    public GameObject HighScoresTable { get { return highScoresTable;}}

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void ActivateLeaderboard()
    {
        highScoresTable.SetActive(true);
    }
}
