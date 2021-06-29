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

    GameObject newGameObject;
    SpriteRenderer spriteRenderer;

    public void InstantiateCard(Vector3 pos)
    {
        newGameObject = Instantiate(prefab, pos, Quaternion.identity);
        gameObject.transform.localScale = new Vector3(0.3f, 0.3f, 0);
        //newGameObject.transform.localScale = new Vector3(1.8f, 1.8f, 0);
        spriteRenderer = newGameObject.GetComponent<SpriteRenderer>();

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
