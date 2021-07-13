using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("SolAR", "ZoomIn", "allows to zoom in a card")]
public class ZoomIn : Command
{

    private GameObject zoomObj;
    //public Sprite zoomCard;
    [SerializeField]
    protected SpriteData zoomSprite;


    public override void OnEnter()
    {
        zoomObj = GameObject.FindGameObjectWithTag("ZoomImage");
        SpriteRenderer sr = zoomObj.GetComponent<SpriteRenderer>();

        if(!sr.enabled)
        {
            sr.sprite = zoomSprite;
            sr.enabled = true;
        }

        Continue();
    }
    
}

