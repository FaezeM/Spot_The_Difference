using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
    private Transform entryContainer;//references
    private Transform entryTemplate;

    private void Awake()
    {
        entryContainer = transform.Find("HighScoresTable");
        entryTemplate = transform.Find("Row1");

        entryTemplate.gameObject.SetActive(false);//hide default template

        float templateHeight = 30f;
        for (int i = 0; i < 10; i++)//top 10
        {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
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
            entryTransform.Find("ID").GetComponent<Text>().text = rankString;

            int score = Random.Range(0, 100);
            entryTransform.Find("Score").GetComponent<Text>().text = score.ToString();

            string name = "AAAAAAA";
            entryTransform.Find("Name").GetComponent<Text>().text = name;
        }
    }
}
