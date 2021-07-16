using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class electronScript : MonoBehaviour
{

    public float radius;

    public float maxSpeed = 5f;
    public float minSpeed = 2f;
    //public float rotSpeed;


    private Vector2 destination;
    private float speed;


    private void Start()
    {

        ChangeDirection();
    }

    // Update is called once per frame
    void Update()
    {

        float step = speed * Time.deltaTime;

        // Vector2 dir = destination - new Vector2(transform.localPosition.x, transform.localPosition.y);

        if (Vector2.Distance(transform.localPosition, destination) < 0.005f)
        {
            ChangeDirection();
        }
        else
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, destination, step);

            // float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            // Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            // transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotSpeed * Time.deltaTime);

        }
    }

    void ChangeDirection()
    {
        destination = Random.insideUnitCircle * radius;

        speed = Random.Range(minSpeed, maxSpeed);
    }


}
