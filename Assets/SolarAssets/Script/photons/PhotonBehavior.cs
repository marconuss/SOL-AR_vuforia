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

    [SerializeField] AudioClip[] reflectedClips;
    [SerializeField] AudioClip[] absorbedClips;
    [SerializeField] AudioClip[] passThroughClips;

    AudioSource audio;

    void Start()
    {
        state = PhotonState.Default;
        reflectedDirection = Vector3.Reflect(moveDirection, new Vector3(0, 1, 0));
        renderer = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();
        
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

    void SpawnElectron(Vector3 pos, Transform parent)
    {
        //electrons don't spawn in phase two
        //
        // ---- probably should change this later ---------
        ///
        if (!GameManager.instance.secondPhase)
        {
            GameObject electron = Instantiate(GameManager.instance.electronPrefab, pos, Quaternion.identity, parent);
        }
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
                audio.clip = reflectedClips[Random.Range(0, reflectedClips.Length)];
                audio.Play();
            }  
        }

        else if (collision.tag == "Conductor")
        {
            //UpdatePhotonState(PhotonState.Absorbed);
            audio.clip = absorbedClips[Random.Range(0, absorbedClips.Length)];
            audio.Play();
            Destroy(gameObject, 1f);
        }

        else if (collision.tag == "GridConductor")
        {
            //UpdatePhotonState(PhotonState.SemiAbsorbed);

            if (Random.Range(0, 3) == 0)
            {
                audio.clip = absorbedClips[Random.Range(0, absorbedClips.Length)];
                audio.Play();
                Destroy(gameObject, 0.2f);
            }
            else
            {
                audio.clip = passThroughClips[Random.Range(0, passThroughClips.Length)];
                audio.Play();
            }
        }

        else if (collision.tag == "NTypeSilicon")
        {
            float reflectionValue = reflectionRate / 100;
            if (Random.Range(0f, 1f) <= reflectionValue)
            {
                UpdatePhotonState(PhotonState.Reflected);
                reflected = true;
                renderer.flipY = true;
                audio.clip = reflectedClips[Random.Range(0, reflectedClips.Length)];
                audio.Play();

            }
            else
            {
                //instantiate electron
                Vector3 pos = new Vector3(transform.position.x, collision.transform.position.y, 0);
                SpawnElectron(pos, GameManager.instance.electronParent.transform);
                Destroy(gameObject);
                //activate elctron behavior
            }

          
        }

        else if (collision.tag == "PTypeSilicon")
        {
            float reflectionValue = reflectionRate / 100;
            if (Random.Range(0f, 1f) <= reflectionValue)
            {
                UpdatePhotonState(PhotonState.Reflected);
                reflected = true;
                renderer.flipY = true;
                audio.clip = reflectedClips[Random.Range(0, reflectedClips.Length)];
                audio.Play();
            }
            else
            {
                audio.clip = absorbedClips[Random.Range(0, absorbedClips.Length)];
                audio.Play();
                Destroy(gameObject, 0.2f);
            }
        }
        else if(collision.tag == "Glass")
        {
            audio.clip = passThroughClips[Random.Range(0, passThroughClips.Length)];
            audio.Play();
        }

    }
}
