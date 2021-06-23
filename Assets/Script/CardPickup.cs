using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPickup : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera camera;

    private Vector3 mousePos;
    
    private float startXpos;
    private float startYpos;

    private bool mouseHold = false;


    private void Awake()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
        mousePos = Input.mousePosition;
        mousePos = camera.ScreenToWorldPoint(mousePos);
        if(mouseHold)
        {
            this.gameObject.transform.localPosition = new Vector3(mousePos.x - startXpos, mousePos.y - startYpos, 0);
        }
    }

    private void OnMouseDown()
    {

        startXpos = mousePos.x - this.transform.localPosition.x;
        startYpos = mousePos.y - this.transform.localPosition.y;
        mouseHold = true;

    }


    private void OnMouseUp()
    {
        mouseHold = false;
    }
}
