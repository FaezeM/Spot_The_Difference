using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private Text timerText;
    [SerializeField] private GameObject gameComplete;
    [SerializeField] private GameObject levelFailed;
    [SerializeField] private GameObject youWin;
    [SerializeField] private GameObject inputField;

    //public List<DifferentObject> DifferentObjectsList {get {return differentObjectsList;}}


    public GameObject GameComplete { get {return gameComplete;}}//getter
    public Text TimerText { get { return timerText;}}
    public GameObject LevelFailed { get { return levelFailed;}}
    public GameObject YouWin { get { return youWin;}}

    private LevelManager levelManager;

    private void Awake()
    {
        if(instance == null) instance = this;
        else if(instance != null) Destroy(gameObject);
        //DontDestroyOnLoad(this);
    }

    public void SaveScore(int score)
    {
        string name = inputField.GetComponent<Text>().text;
        //create entry
        HighScoreEntry highScoreEntry = new HighScoreEntry {score = score, name = name};
        //get list
        string jsonString = PlayerPrefs.GetString("highScoreTable");
        HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);
        //add
        int i = highScores.highScoreEntryList.Count;
        if(highScores.highScoreEntryList[i].score <= score)
        {
            highScores.highScoreEntryList.Add(highScoreEntry);
            //save
            string json = JsonUtility.ToJson(highScores);
            PlayerPrefs.SetString("highScoreTable", json);//store string
            PlayerPrefs.Save();
        }
    }

    public void HighScoresButton()
    {
        Debug.Log("button");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

    private class HighScores
    {
        public List<HighScoreEntry> highScoreEntryList;
    }

    [System.Serializable]
    private class HighScoreEntry
    {
        public int score;
        public string name;
    }
}
