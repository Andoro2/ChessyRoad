using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    private Animator Anim;
    private bool AnimPlaying = false;

    void Start()
    {
        Anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            AnimPlaying = true;
        }
        else
        {
            AnimPlaying = false;
        }

        if (!AnimPlaying)
        {
            Destroy(gameObject);
        }
    }
}
