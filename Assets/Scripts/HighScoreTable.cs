using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
    private Transform entryContainer;//references
    private Transform entryTemplate;
    private List<HighScoreEntry> highScoreEntryList;
    private List<Transform> highScoreEntryTransformList;

    private void Awake()
    {
        entryContainer = transform.Find("HighScoresTable");
        entryTemplate = transform.Find("Row1");

        entryTemplate.gameObject.SetActive(false);//hide default template

        highScoreEntryList = new List<HighScoreEntry>()
        {
            new HighScoreEntry{ score = 100, name = "Ali" },
            new HighScoreEntry{ score = 90, name = "Faeze" },
            new HighScoreEntry{ score = 40, name = "Sajjad" },
            new HighScoreEntry{ score = 60, name = "Fateme" },
            new HighScoreEntry{ score = 110, name = "Zahra" },
            new HighScoreEntry{ score = 200, name = "Amir" },
            new HighScoreEntry{ score = 130, name = "Mohammad" },
            new HighScoreEntry{ score = 500, name = "Saeed" }
        };

        //sort
        for(int i = 0; i < highScoreEntryList.Count; i++)
        {
            for(int j = 0; j < highScoreEntryList.Count; j++)
            {
                if(highScoreEntryList[i].score > highScoreEntryList[j].score)
                {
                    //swap
                    HighScoreEntry tmp = highScoreEntryList[i];
                    highScoreEntryList[i] = highScoreEntryList[j];
                    highScoreEntryList[j] = tmp;
                }
            }
        }

        highScoreEntryTransformList = new List<Transform>();
        foreach (HighScoreEntry highScoreEntry in highScoreEntryList)
        {
            CreateHighScoreEntryTransform(highScoreEntry, entryContainer, highScoreEntryTransformList);
        }

    }
    //transformList to add instantiated transform onto
    private void CreateHighScoreEntryTransform(HighScoreEntry highScoreEntry, Transform container, List<Transform> transformList)//add new entry to the table
    {
        float templateHeight = 30f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        entryTransform.SetParent(container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
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
