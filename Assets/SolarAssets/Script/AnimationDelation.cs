using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDelation : MonoBehaviour
{
    [Range(0, 3)]
    public float delay;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(playAnimation());
    }
    public IEnumerator playAnimation()
    {
        yield return new WaitForSeconds(delay);
        animator.Play("idle");
    }
}
