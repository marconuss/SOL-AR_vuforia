using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronManager : MonoBehaviour
{
    public static ElectronManager instance;

    GameObject electronPrefab;

    void Start()
    {
        if (instance != null) Destroy(instance);
        else instance = this;
    }
    public void SpawnElectron(Vector3 pos, Transform parent)
    {
        Instantiate(electronPrefab, pos, Quaternion.identity, parent);
    }

    
}
