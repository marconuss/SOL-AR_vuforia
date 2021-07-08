using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Card : MonoBehaviour
{
    public cardType type;
    [SerializeField]
    GameObject prefab;

    public Card[] neighbourCards;

    public Vector2Int gridPosition;

    GameObject newGameObject;
    SpriteRenderer spriteRenderer;


    private void Start()
    {
        neighbourCards = new Card[4];
    }

    public void InstantiateCard(Vector3 pos, GameObject prefab)
    {
        newGameObject = Instantiate(prefab, pos, Quaternion.identity);
        //newGameObject.transform.localScale = new Vector3(0.3f, 0.3f, 0);
        //newGameObject.transform.localScale = new Vector3(1.8f, 1.8f, 0);
        spriteRenderer = newGameObject.GetComponent<SpriteRenderer>();
        neighbourCards = new Card[4];

        switch (type)
        {
            case cardType.Reflector:
                spriteRenderer.sprite = GameManager.instance.ReflectorSprite;
                break;

            case cardType.Glass:
                spriteRenderer.sprite = GameManager.instance.GlassSprite;
                break;

            case cardType.Conductor:
                spriteRenderer.sprite = GameManager.instance.ConductorSprite;
                break;

            case cardType.GridConductor:
                spriteRenderer.sprite = GameManager.instance.ConductorGridSprite;
                break;

            case cardType.NTypeSilicon:
                spriteRenderer.sprite = GameManager.instance.NTypeSiliconSprite;
                break;

            case cardType.PTypeSilicon:
                spriteRenderer.sprite = GameManager.instance.PTypeSiliconSprite;
                break;

            default:
                spriteRenderer.sprite = GameManager.instance.ReflectorSprite;
                Debug.LogWarning("No fitting sprite found");
                break;
        }

    }

    public void InstatiateCardSprite(Vector3 pos, cardType _type)
    {
        newGameObject = Instantiate(prefab, pos, Quaternion.identity);
        newGameObject.transform.localScale = new Vector3(1.8f, 1.8f, 0);
        spriteRenderer = newGameObject.GetComponent<SpriteRenderer>();

        switch (_type)
        {
            case cardType.Reflector:
                spriteRenderer.sprite = GameManager.instance.ReflectorSprite;
                break;

            case cardType.Glass:
                spriteRenderer.sprite = GameManager.instance.GlassSprite;
                break;

            case cardType.Conductor:
                spriteRenderer.sprite = GameManager.instance.ConductorSprite;
                break;

            case cardType.GridConductor:
                spriteRenderer.sprite = GameManager.instance.ConductorGridSprite;
                break;

            case cardType.NTypeSilicon:
                spriteRenderer.sprite = GameManager.instance.NTypeSiliconSprite;
                break;

            case cardType.PTypeSilicon:
                spriteRenderer.sprite = GameManager.instance.PTypeSiliconSprite;
                break;

            default:
                spriteRenderer.sprite = GameManager.instance.ReflectorSprite;
                Debug.LogWarning("No fitting sprite found");
                break;
        }

    }


    public void RemoveCard()
    {
        if (newGameObject) Destroy(newGameObject);

        for (int i = 0; i < neighbourCards.Length; i++)
        {
            neighbourCards[i] = null;
        }
    }

}
