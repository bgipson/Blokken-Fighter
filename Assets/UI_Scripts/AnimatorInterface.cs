using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorInterface : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetSelected(bool selected)
    {
        if (animator)
        {
            animator.SetBool("Selected", selected);
        }
    }
}
