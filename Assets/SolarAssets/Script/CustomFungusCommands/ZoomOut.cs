using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("SolAR", "ZoomOut", "Zooms back to the game")]
public class ZoomOut : Command
{
    private GameObject zoomObj;
    public override void OnEnter()
    {
        zoomObj = GameObject.FindGameObjectWithTag("ZoomImage");
        SpriteRenderer sr = zoomObj.GetComponent<SpriteRenderer>();

        if(sr.enabled)
        {
            sr.enabled = false;
        }

        Continue();
    }
    
}
