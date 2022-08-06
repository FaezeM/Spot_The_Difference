using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageHolder : MonoBehaviour
{
    [SerializeField]//editable for unity
    private List<DifferentObject> differentObjectsList;

    public List<DifferentObject> DifferentObjectsList {get {return differentObjectsList;}}
}
