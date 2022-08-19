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

    //public List<DifferentObject> DifferentObjectsList {get {return differentObjectsList;}}


    public GameObject GameComplete { get {return gameComplete;}}//getter
    public Text TimerText { get { return timerText;}}

    private LevelManager levelManager;

    private void Awake()
    {
        if(instance == null) instance = this;
        else if(instance != null) Destroy(gameObject);
    }

    /*public void NextButton()
    {
        Debug.Log("button");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }*/
}
