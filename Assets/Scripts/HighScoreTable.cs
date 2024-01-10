using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
    public Transform entryContainer;//references
    public Transform entryTemplate;
    public float templateHeight=30f;
    private void Awake()
    {

        entryTemplate.gameObject.SetActive(false);//hide default template

        for (int i = 0; i < 10; i++)//top 10
        {
            Transform entryTransform = Instantiate(entryTemplate,entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
            entryTransform.gameObject.SetActive(true);

            int rank = i + 1;
            string rankString;
            switch (rank) {//rank text
                case 1:
                    rankString = "1ST";
                    break;
                case 2:
                    rankString = "2ND";
                    break;
                case 3:
                    rankString = "3RD";
                    break;
                default:
                    rankString = rank + "TH";
                    break;
            }
            entryTransform.Find("ID").GetComponent<TextMeshProUGUI>().text = rankString;

            int score = Random.Range(0, 100);
            entryTransform.Find("Score").GetComponent<TextMeshProUGUI>().text = score.ToString();

            string name = "AAAAAAA";
            entryTransform.Find("Name").GetComponent<TextMeshProUGUI>().text = name;
        }
        entryTransform.Find("ID").GetComponent<Text>().text = rankString;

        int score = highScoreEntry.score;
        entryTransform.Find("Score").GetComponent<Text>().text = score.ToString();

        string name = highScoreEntry.name;
        entryTransform.Find("Name").GetComponent<Text>().text = name;

        transformList.Add(entryTransform);
    }

    private void AddHighScoreEntry(int score, string name)
    {
        //create entry
        HighScoreEntry highScoreEntry = new HighScoreEntry {score = score, name = name};
        //get list
        string jsonString = PlayerPrefs.GetString("highScoreTable");
        HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);
        //add
        highScores.highScoreEntryList.Add(highScoreEntry);
        //save
        string json = JsonUtility.ToJson(highScores);
        PlayerPrefs.SetString("highScoreTable", json);//store string
        PlayerPrefs.Save();
    }
    
    //List of all records
    private class HighScores
    {
        public List<HighScoreEntry> highScoreEntryList;
    }

    //a single highscore entry
    [System.Serializable]
    private class HighScoreEntry
    {
        public int score;
        public string name;
    }
}
