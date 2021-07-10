using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    public Vector3 moveDirection;
    public float movementSpeed;

    public enum PhotonState { SemiAbsorbed, Absorbed, Reflected, SemiReflected, Pass}
    public PhotonState state;

    void Start()
    {
        state = PhotonState.Pass;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == PhotonState.Pass)
        {
            DefaultMovement();
        }

        else if (state == PhotonState.Reflected)
        {

        }

        else if (state == PhotonState.SemiReflected)
        {

        }
    }

    public void UpdatePhotonState(PhotonState _state)
    {
        state = _state;
    }

    void DefaultMovement()
    {
        transform.position += moveDirection * movementSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Conductor")
        {
            //UpdatePhotonState(PhotonState.Absorbed);
            Destroy(gameObject);
        }

        if (collision.tag == "GridConductor")
        {
            //UpdatePhotonState(PhotonState.SemiAbsorbed);
            if(Random.Range(0, 3) != 0) Destroy(gameObject);
        }

        if (collision.tag == "NTypeSilicon")
        {
            UpdatePhotonState(PhotonState.SemiReflected);
        }

        if (collision.tag == "PTypeSilicon")
        {
            UpdatePhotonState(PhotonState.SemiReflected);
        }
    }
}
