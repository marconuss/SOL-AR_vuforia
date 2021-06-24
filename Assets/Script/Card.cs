using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Card : MonoBehaviour
{
    public cardType type;
    [SerializeField]
    GameObject prefab;
    public Sprite sprite;
    public int ID;

    GameObject gameObject;

    public void InstantiateCard(Vector3 pos)
    {
        gameObject = Instantiate(prefab, pos, Quaternion.identity);
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void RemoveCard()
    {
        if (gameObject) Destroy(gameObject);
    }

}
