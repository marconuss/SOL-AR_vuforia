using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FakeTriggerManger : MonoBehaviour
{
    public GameObject[] triggerList;
    public List<GameObject> triggerList2 = new List<GameObject>();
    public GameObject[,] triggerGrid;


    void Awake()
    {
        triggerList2 = GameObject.FindGameObjectsWithTag("Trigger").ToList<GameObject>();

    }

}

    //for loop wo alle trigger in den 
