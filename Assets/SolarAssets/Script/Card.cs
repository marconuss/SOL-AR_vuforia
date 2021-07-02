using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Card : MonoBehaviour
{
    public cardType type;
    [SerializeField]
    GameObject prefab;

    public Card[] neighbourCards;

    GameObject newGameObject;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        neighbourCards = new Card[4];
    }

    public void InstantiateCard(Vector3 pos)
    {
        newGameObject = Instantiate(prefab, pos, Quaternion.identity);
        //newGameObject.transform.localScale = new Vector3(0.3f, 0.3f, 0);
        newGameObject.transform.localScale = new Vector3(1.8f, 1.8f, 0);
        spriteRenderer = newGameObject.GetComponent<SpriteRenderer>();

        for (int i = 0; i < neighbourCards.Length; i++)
        {
            if(neighbourCards[i])
            Debug.LogWarning(neighbourCards[i].gameObject.name);
        }

        switch (type)
        {
            case cardType.Reflector:
                spriteRenderer.sprite = CardManager.instance.ReflectorSprite;
                break;
            case cardType.Glass:
                spriteRenderer.sprite = CardManager.instance.GlassSprite;
                break;
            case cardType.Conductor:
                spriteRenderer.sprite = CardManager.instance.ConductorSprite;
                break;
            case cardType.GridConductor:
                spriteRenderer.sprite = CardManager.instance.ConductorGridSprite;
                break;
            case cardType.NTypeSilicon:
                spriteRenderer.sprite = CardManager.instance.NTypeSiliconSprite;
                break;
            case cardType.PTypeSilicon:
                spriteRenderer.sprite = CardManager.instance.PTypeSiliconSprite;
                break;
            default:
                spriteRenderer.sprite = CardManager.instance.ReflectorSprite;
                Debug.LogWarning("No fitting sprite found");
                break;
        }
    }

    public void RemoveCard()
    {
        if (newGameObject) Destroy(newGameObject);
    }

}
