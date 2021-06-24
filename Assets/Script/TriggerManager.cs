using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TriggerManager : MonoBehaviour
{
    public static TriggerManager instance;
    public List<BoxCollider> triggerZones;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }

        instance = this;
    }

    // Update is called once per frame
    private void Start()
    {
        //only for testing sorry
        GridSystem grid = new GridSystem(3, 3, 10f);
    }

    public void UpdateCardList()
    {
        foreach (BoxCollider box in triggerZones)
        {
            if(box.gameObject.GetComponent<TriggerTest>().cardPlaced)
            {
                Debug.LogWarning("Card in" + box.gameObject.name);
            }
        }
    }


}
