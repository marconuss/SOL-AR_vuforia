using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronManager : MonoBehaviour
{
    public static ElectronManager instance;

    GameObject electronPrefab;

    ElectronBehavior electron;

    void Start()
    {
        if (instance != null) Destroy(instance);
        else instance = this;
    }

}
