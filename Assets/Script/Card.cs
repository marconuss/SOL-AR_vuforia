using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public partial class Card : ScriptableObject
{
    public cardType type;

    public Sprite sprite;
    public int ID;

    GameObject gameObject;

    public void InstantiateCard(Vector3 pos)
    {
        gameObject = GameObject.Instantiate(Resources.Load<GameObject>("Square"), pos, Quaternion.identity);
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void Remove()
    {
        if (gameObject) Destroy(gameObject);
    }

}
