using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonBehavior : MonoBehaviour
{

    public Vector3 moveDirection = new Vector3(1, -1, 0);
    Vector3 reflectedDirection;
    public float movementSpeed;
    bool reflected = false;

    public float borderX = 80f;
    public float borderY = -100f;

    [Tooltip("This is the probability for a photon to get reflected in percent. (e.g. 30 for 30%)")]
    public float reflectionRate = 30f;

    SpriteRenderer renderer;

    public enum PhotonState { Reflected, Default}
    public PhotonState state;

    void Start()
    {
        state = PhotonState.Default;
        reflectedDirection = Vector3.Reflect(moveDirection, new Vector3(0, 1, 0));
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(state == PhotonState.Default)
        {
            DefaultMovement();
        }

        else if (state == PhotonState.Reflected)
        {
            ReflectedMovement();
        }

    }

    public void UpdatePhotonState(PhotonState _state)
    {
        state = _state;
    }

    void DefaultMovement()
    {
        transform.position += moveDirection * movementSpeed * Time.deltaTime;
        if (transform.position.x > borderX || transform.position.y < borderY) Destroy(gameObject);
       

    }

    void ReflectedMovement()
    {
        transform.position += reflectedDirection * movementSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Reflector")
        {
            if (reflected)
            {
                reflected = false;
                UpdatePhotonState(PhotonState.Default);
                renderer.flipX = false;
                renderer.flipY = false;

            }  
        }

        if (collision.tag == "Conductor")
        {
            //UpdatePhotonState(PhotonState.Absorbed);
            Destroy(gameObject);
        }

        if (collision.tag == "GridConductor")
        {
            //UpdatePhotonState(PhotonState.SemiAbsorbed);
            if(Random.Range(0, 3) == 0) Destroy(gameObject);
        }

        if (collision.tag == "NTypeSilicon")
        {
            float reflectionValue = reflectionRate / 100;
            if (Random.Range(0f, 1f) <= reflectionValue)
            {
                UpdatePhotonState(PhotonState.Reflected);
                reflected = true;
                renderer.flipY = true;

            }

        }

        if (collision.tag == "PTypeSilicon")
        {
            float reflectionValue = reflectionRate / 100;
            if (Random.Range(0f, 1f) <= reflectionValue)
            {
                UpdatePhotonState(PhotonState.Reflected);
                reflected = true;
                renderer.flipY = true;
            }
        }
    }
}
