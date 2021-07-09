using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    public Vector3 moveDirection;
    public float movementSpeed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * movementSpeed * Time.deltaTime;
    }
}
