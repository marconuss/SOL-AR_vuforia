using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class Test : MonoBehaviour
{
    public Text Text;
    public Text PosText;
    Vector2 fixedScreenPos;
    bool active = false;

    public void OnFound()
    {
        if(!active) active = true;

        Text.text = gameObject.name + " found! ";
      
        
    }

    public void OnLost()   
    {
        if (active) active = false;
        Text.text = gameObject.name + " lost!";
        fixedScreenPos = new Vector2(0, 0);
        
    }

    private void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        fixedScreenPos = new Vector2(screenPos.x / Screen.width, (1 - (screenPos.y / Screen.height)));
        fixedScreenPos.x = Mathf.Round(fixedScreenPos.x * 100f) * 0.01f;
        fixedScreenPos.y = Mathf.Round(fixedScreenPos.y * 100f) * 0.01f;

        if(active)
        PosText.text = "Pos:" + fixedScreenPos.x + ", " + fixedScreenPos.y;
    }
}
