using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageHolder : MonoBehaviour
{
    [SerializeField]//editable for unity
    private List<DifferentObject> differentObjectsList;

    [SerializeField]//editable for unity
    public int maxCount;

    public List<DifferentObject> DifferentObjectsList {get {return differentObjectsList;}}

    /*public void LevelChange()
    {
        differentObjectsList = new List<DifferentObjectsList>();

        for (int i = 0; i < transform.childCount; i++)
        {
            DifferentObject differentObject = new DifferentObject();
            differentObject.difference = transform.GetChild(i).gameObject;
            differentObject.name = transform.GetChild(i).name;
            differentObject.makeHidden = true;

            differentObjectsList.Add(differentObject);
        }
    }*/
}
