using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField]
    private PlayerAnimations currentAnimation;
    private Animator animator;
    private CharacterController controller;
    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.controller = GetComponent<CharacterController>();
        setAnimation(PlayerAnimations.Player_Idle);
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (controller.isGrounded && animationIsNot(PlayerAnimations.Player_Jump) && animationIsNot(PlayerAnimations.Player_Landing) && animationIsNot(PlayerAnimations.Player_Falling))
        {
            if (direction.magnitude >= 0.1)
            {
                setAnimation(PlayerAnimations.Player_Walk);
            }
            else
            {
                setAnimation(PlayerAnimations.Player_Idle);
            }
            if (Input.GetButtonDown("Jump"))
            {
                setAnimation(PlayerAnimations.Player_Jump);
            }
        }
        if (animationIs(PlayerAnimations.Player_Falling) && controller.isGrounded)
        {
            setAnimation(PlayerAnimations.Player_Landing);
        }

        this.animator.Play(this.currentAnimation.ToString());
    }
    public void playerInAir()
    {
        this.setAnimation(PlayerAnimations.Player_Falling);
    }
    public void playerLanded()
    {
        setAnimation(PlayerAnimations.Player_Idle);
    }
    private void setAnimation(PlayerAnimations a)
    {
        this.currentAnimation = a;
    }
    private bool animationIsNot(PlayerAnimations a)
    {
        return this.currentAnimation != a;
    }
    private bool animationIs(PlayerAnimations a)
    {
        return this.currentAnimation == a;
    }
}
