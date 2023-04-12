using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations
{
    private Animator animator { get; set; }
    public Animations(Animator anim)
    {
        animator = anim;
    }
    public void PlayAnimaton(string animationName){
        animator.Play(animationName);
    }
}
