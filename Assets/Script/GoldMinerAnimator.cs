using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMinerAnimator : MonoBehaviour
{
    public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ThrowHook()
    {
        animator.SetBool("ThrowHook", true);
    }
    public void PullHook()
    {
        animator.SetBool("ThrowHook", false);
        animator.SetBool("PullHook", true );
    }
    public void Stop()
    {
        animator.SetBool("PullHook",false);
    }
    public void ThrowExplosive()
    {
        animator.SetTrigger("ThrowExpl");
    }
}
