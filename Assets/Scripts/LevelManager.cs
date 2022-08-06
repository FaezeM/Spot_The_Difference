using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField]//editable for unity
    private float timeLimit;//max time

    [SerializeField]//editable for unity
    private ImageHolder imageHolderPrefab;//all available different objs in the scene

    private List<DifferentObject> activeObjectsList;

    [SerializeField]//editable for unity
    private int maxCount;

    private int totalObjectsFound = 0;
    private float currentTime = 0;
    private GameStatus gameStatus = GameStatus.NEXT; //by default

    private void Awake() //for redundance
    {
        if(instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        activeObjectsList = new List<DifferentObject>();
        AssignObjects();
    }

    void AssignObjects()
    {
        ImageHolder imageHolder = Instantiate(imageHolderPrefab, Vector3.zero, Quaternion.identity);
        currentTime = timeLimit;
        UIManager.instance.TimerText.text = "" + currentTime;
        totalObjectsFound = 0;
        imageHolder.DifferentObjectsList.Clear();
        for(int i = 0; i < imageHolder.DifferentObjectsList.Count; i++)
        {
            imageHolder.DifferentObjectsList[i].difference.GetComponent<Collider2D>().enabled = true;//enable colliders
        }

        gameStatus = GameStatus.PLAYING;
        /*int k = 0;
        while(k < maxCount)
        {
            int rand = Random.Range(0, differentObjectsList.Count);

            if(differentObjectsList[rand].makeHidden)
            {
                differentObjectsList[rand].difference.name = "" + k;
                differentObjectsList[rand].makeHidden = false;
                differentObjectsList[k].difference.GetComponent<Collider2D>().enabled = false;

                foundObjectsList.Add(differentObjectsList[rand]);
            }
            
            k++;
        }*/
    }

    private void Update()
    {
        if(gameStatus == GameStatus.PLAYING)
        {

            if(Input.GetMouseButtonDown(0))//detect mouse button
            {
                Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(position, Vector3.zero);

                if(hit && hit.collider != null)//if hit an object
                {
                    Debug.Log(hit.collider.gameObject.name);

                    hit.collider.gameObject.GetComponent<Collider2D>().enabled = false;
                    hit.collider.gameObject.GetComponent<Image>().enabled = true;

                    for(int i = 0; i < activeObjectsList.Count; i++)//remove from active list
                    {
                        if(activeObjectsList[i].difference.name == hit.collider.gameObject.name)
                        {
                            activeObjectsList.RemoveAt(i);
                            break;
                        }
                    }

                    totalObjectsFound++;

                    if(totalObjectsFound >= maxCount)
                    {
                        Debug.Log("Level Complete");
                        //UIManager.instance.GameComplete.EndText.text = "Level Complete";
                        UIManager.instance.GameComplete.SetActive(true);
                        gameStatus = GameStatus.NEXT;
                    }
                }
            }

            currentTime -= Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            UIManager.instance.TimerText.text = time.ToString("mm' : 'ss");

            if(currentTime <= 0)
            {
                Debug.Log("Level Failed");
                //UIManager.instance.GameComplete.EndText.text = "Level Failed";
                UIManager.instance.GameComplete.SetActive(true);
                gameStatus = GameStatus.NEXT;
            }
        }
    }

}

[System.Serializable]//editable for unity
public class DifferentObject
{
    public string name;
    public GameObject difference;
    public bool makeHidden = true;
}

public enum GameStatus
{
    PLAYING,
    NEXT
}