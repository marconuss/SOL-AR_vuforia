using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockScript : MonoBehaviour
{
    [Range(0, 1)]
    public float volume;
    [SerializeField]
    private AudioClip[] audioClips;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void PlaySound()
    {
        int cIndex = Random.Range(0, audioClips.Length);
        audioSource.PlayOneShot(audioClips[cIndex], volume);
    }
}
