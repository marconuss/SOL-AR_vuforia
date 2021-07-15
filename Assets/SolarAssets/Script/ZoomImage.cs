using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomImage : MonoBehaviour
{
    //instantiate prefabs to animate on the zoom image with random position and sizes
    //also cases according to fungus variables and the zoomed image

    public Card.cardType type;

    
    public int sizeOfPool = 12;

    public static ZoomImage instance;
    [SerializeField]
    private GameObject[] prefabs;
    [SerializeField]
    private List<GameObject> poolObjects;

    private void Start()
    {
        //singleton
        if (instance != null)
        {
            Destroy(this);
        }

        instance = this;

        StartAnimation();
    }
    /*
     
    public void PoolObjects()
    {
        objectPool = new List<GameObject>();
        for (int i = 0; i < sizeOfPool; i++)
        {
            for (int j = 0; j < objectsToPool.Count; j++)
            {

                GameObject clone = (GameObject)Instantiate(objectsToPool[j], this.transform);

                objectPool.Add(clone);
                
            }
        }
    }
    */
    public void StartAnimation()
    {
        if (this.gameObject.GetComponent<SpriteRenderer>().enabled)
        {
            switch (type)
            {
                case Card.cardType.Reflector:
                    break;
                case Card.cardType.Glass:
                    for (int i = 0; i < sizeOfPool; i++)
                    {
                        GameObject prefab = prefabs[Random.Range(0, prefabs.Length - 1)];
                        Instantiate(prefab, this.transform);
                        prefab.transform.position = new Vector3(Random.Range(-10f, 0f), Random.Range(10f, 7.5f), 1f);
                    }
                    break;
                case Card.cardType.Conductor:

                    SpawnInTheGlass();

                    break;
                case Card.cardType.GridConductor:

                    SpawnInTheGlass();

                    break;
                case Card.cardType.NTypeSilicon:

                    SpawnInTheGlass();

                    break;
                case Card.cardType.PTypeSilicon:

                    SpawnInTheGlass();
                    break;
                default:
                    break;
            }
        }
        else
        {
            DestroyMyChildern();
        }
    }
    public void DestroyMyChildern()
    {
        foreach(Transform child in this.GetComponentInChildren<Transform>())
        {
            GameObject.Destroy(child.gameObject);
            poolObjects.Clear();
        }
    }

    private void SpawnInTheGlass()
    {
        for (int i = 0; i < sizeOfPool; i++)
        {
            GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
            Instantiate(prefab, this.transform);
            prefab.transform.localPosition = Random.insideUnitCircle * 5f;
            poolObjects.Add(prefab);
        }
    }


    private void Update()
    {
        GameObject go = poolObjects[1];

        go.transform.position += new Vector3(1f, 1f, 0);


    }
    
}
