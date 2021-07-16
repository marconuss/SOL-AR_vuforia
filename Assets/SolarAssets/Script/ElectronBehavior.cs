using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    float Ypos;
    float Xpos;
    Vector3 originalPos;

    float frequency = 2f;
    float amplitude = 1f;
    public float moveSpeed = 5f;

    void Start()
    {
        originalPos = transform.position;
        Xpos = originalPos.x;
    }

    void Update()
    {
        Ypos = originalPos.y;
        Ypos += Mathf.Sin((Time.time * Mathf.PI * frequency)) * amplitude;

        Xpos += moveSpeed * Time.deltaTime;

        transform.position = new Vector3(Xpos, Ypos, 0);
    }
}
