using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonBehaviourMarco : MonoBehaviour
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

    public enum PhotonState { Reflected, Default }
    public PhotonState state;

    [Header("Audio")]
    [Tooltip("Place here the audioclips that shold be played when the photos passes the glass")]
    public AudioClip[] audioClips;
    [Range(0, 1)]
    public float clipsVolume;

    //public float yOffset;
    private AudioSource audioSource;
    private Animator animator;

    void Start()
    {
        state = PhotonState.Default;
        reflectedDirection = Vector3.Reflect(moveDirection, new Vector3(0, 1, 0));
        renderer = GetComponent<SpriteRenderer>();

        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (state == PhotonState.Default)
        {
            DefaultMovement();
        }

        else if (state == PhotonState.Reflected)
        {
            ReflectedMovement();
        }

        if (FindParentWithTag(this.gameObject, "ZoomImage"))
        {
            ZoomBehavior();
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
            if (Random.Range(0, 3) == 0) Destroy(gameObject);
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
            else
            {
                //instantiate electron
                Instantiate(GameManager.instance.electronPrefab, collision.gameObject.transform.position, Quaternion.identity, collision.gameObject.transform);
                Destroy(gameObject);
                //activate elctron behavior
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
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void ZoomBehavior()
    {
        //Vector2 parentPos = new Vector2(transform.parent.position.x, transform.parent.position.y);
        //Vector2 randTarget = parentPos + (Random.insideUnitCircle * 4f);
        //randTarget.y += yOffset;

        float randTargetY = Random.Range(-4f, 4f);

        if (transform.position.y < randTargetY)
        {
            if (!audioSource.isPlaying)
            {
                int clipIndex = Random.Range(0, audioClips.Length);
                audioSource.PlayOneShot(audioClips[clipIndex], clipsVolume);
            }
            renderer.sortingOrder = 2;
            animator.Play("photonsShrink");
            Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length + 0.3f);
        }
    }

    private GameObject FindParentWithTag(GameObject child, string tag)
    {
        Transform t = child.transform;
        while (t.parent != null)
        {
            if (t.parent.tag == tag)
            {
                return t.parent.gameObject;
            }
            t = t.parent;
        }
        return null;
    }
}