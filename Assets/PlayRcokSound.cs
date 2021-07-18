using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRcokSound : MonoBehaviour
{

    public AudioClip[] audioClips;
    [Range(0, 1)]
    public float clipsVolume;

    //public float yOffset;
    private AudioSource audioSource;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    public void PlaySound()
    {
        if(!audioSource.isPlaying)
        {
            int clipIndex = Random.Range(0, audioClips.Length);
            audioSource.PlayOneShot(audioClips[clipIndex], clipsVolume);
        }
    }
}
