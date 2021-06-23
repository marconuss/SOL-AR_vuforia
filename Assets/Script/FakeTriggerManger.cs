using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeTriggerManger : MonoBehaviour
{
    public GameObject[] triggerList;

    void Awake()
    {
        triggerList = GameObject.FindGameObjectsWithTag("Trigger");

    }

}
