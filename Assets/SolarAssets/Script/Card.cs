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
        newGameObject.tag = type.ToString();

        switch (type)
        {
            case cardType.Reflector:
                if (!GameManager.instance.secondPhase) spriteRenderer.sprite = GameManager.instance.ReflectorSprite[0];
                else spriteRenderer.sprite = GameManager.instance.ReflectorSprite[1];

                break;

            case cardType.Glass:
                if (!GameManager.instance.secondPhase) spriteRenderer.sprite = GameManager.instance.GlassSprite[0];
                else spriteRenderer.sprite = GameManager.instance.GlassSprite[1];
                break;

            case cardType.Conductor:
                if (!GameManager.instance.secondPhase) spriteRenderer.sprite = GameManager.instance.ConductorSprite[0];
                else spriteRenderer.sprite = GameManager.instance.ConductorSprite[1];
                break;

            case cardType.GridConductor:
                if (!GameManager.instance.secondPhase) spriteRenderer.sprite = GameManager.instance.ConductorGridSprite[0];
                else spriteRenderer.sprite = GameManager.instance.ConductorGridSprite[1];
                break;

            case cardType.NTypeSilicon:
                if (!GameManager.instance.secondPhase) spriteRenderer.sprite = GameManager.instance.NTypeSiliconSprite[0];
                else spriteRenderer.sprite = GameManager.instance.NTypeSiliconSprite[1];
                break;

            case cardType.PTypeSilicon:
                if (!GameManager.instance.secondPhase) spriteRenderer.sprite = GameManager.instance.PTypeSiliconSprite[0];
                else spriteRenderer.sprite = GameManager.instance.PTypeSiliconSprite[1];
                break;

            default:
                spriteRenderer.sprite = GameManager.instance.ReflectorSprite[0];
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
                spriteRenderer.sprite = GameManager.instance.ReflectorSprite[0];
                break;

            case cardType.Glass:
                spriteRenderer.sprite = GameManager.instance.GlassSprite[0];
                break;

            case cardType.Conductor:
                spriteRenderer.sprite = GameManager.instance.ConductorSprite[0];
                break;

            case cardType.GridConductor:
                spriteRenderer.sprite = GameManager.instance.ConductorGridSprite[0];
                break;

            case cardType.NTypeSilicon:
                spriteRenderer.sprite = GameManager.instance.NTypeSiliconSprite[0];
                break;

            case cardType.PTypeSilicon:
                spriteRenderer.sprite = GameManager.instance.PTypeSiliconSprite[0];
                break;

            default:
                spriteRenderer.sprite = GameManager.instance.ReflectorSprite[0];
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
