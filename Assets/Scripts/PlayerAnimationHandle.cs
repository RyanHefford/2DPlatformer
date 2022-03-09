using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandle : MonoBehaviour
{
    private float maxVerticalVelocity = 10.0f;
    private bool isBusy = false;
    private bool justLanded = false;

    private Animator animator;
    private Rigidbody2D body;
    private PlayerMovement movement;

    const string PLAYER_IDLE = "Idle";
    const string PLAYER_LOOK_AROUND = "LookAround";
    const string PLAYER_WALK = "Walk";
    const string PLAYER_LIGHT_ATTACK = "LightAttack";
    const string PLAYER_HEAVY_ATTACK = "HeavyAttack";
    const string PLAYER_JUMP_1 = "JumpState1";
    const string PLAYER_JUMP_2 = "JumpState2";
    const string PLAYER_JUMP_3 = "JumpState3";
    const string PLAYER_JUMP_4 = "JumpState4";
    const string PLAYER_JUMP_5 = "JumpState5";
    const string PLAYER_JUMP_6 = "JumpState6";
    const string PLAYER_JUMP_7 = "JumpState7";
    const string PLAYER_LANDING = "Landing";

    private string currentAnimation = PLAYER_IDLE;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isBusy)
        {
            if (!CheckAnimationComplete())
            {
                return;
            }
            else
            {
                animator.Play(PLAYER_IDLE);
            }
        }else if (justLanded)   //begin land animation
        {
            justLanded = false;
            ChangeAnimation(PLAYER_LANDING);
            if (body.velocity.y <= -14) //Play landing animation if fall is hard enough
            {
                isBusy = true;

                return;
            }
            
        }

        //check if grounded
        if (!movement.isGrounded)
        {
            float verticalVelocity = body.velocity.y;
            //accending
            if (verticalVelocity > 0)
            {
                if (verticalVelocity >= maxVerticalVelocity)
                {
                    ChangeAnimation(PLAYER_JUMP_1);
                }
                else if (verticalVelocity >= maxVerticalVelocity / 3)
                {
                    ChangeAnimation(PLAYER_JUMP_2);
                }
                else if (verticalVelocity >= maxVerticalVelocity / 5)
                {
                    ChangeAnimation(PLAYER_JUMP_3);
                }
                else if (verticalVelocity >= maxVerticalVelocity / 7)
                {
                    ChangeAnimation(PLAYER_JUMP_4);
                }
            }
            //deccending
            else
            {
                if (verticalVelocity >= -maxVerticalVelocity / 5)
                {
                    ChangeAnimation(PLAYER_JUMP_5);
                }
                else if (verticalVelocity >= -maxVerticalVelocity / 2)
                {
                    ChangeAnimation(PLAYER_JUMP_6);
                }
                else if (verticalVelocity >= -maxVerticalVelocity)
                {
                    ChangeAnimation(PLAYER_JUMP_7);
                }
            }
        }
        else
        {
            if (movement.horizontalInput != 0)
            {
                ChangeAnimation(PLAYER_WALK);
            }
            else
            {
                ChangeAnimation(PLAYER_IDLE);
            }
        }

        
    }


    private void ChangeAnimation(string newAnimation)
    {
        if (newAnimation != currentAnimation)
        {
            animator.Play(newAnimation);
            currentAnimation = newAnimation;
        }
    }


    private bool CheckAnimationComplete()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
        {
            isBusy = false;
            return true;
        }
        return false;
    }

    public bool CheckIsBusy()
    {
        return isBusy;
    }

    public void JustLanded()
    {
        justLanded = true;
    }

    public void MakeLightAttack()
    {
        animator.Play(PLAYER_LIGHT_ATTACK);
        isBusy = true;

    }

    public void MakeHeavyAttack()
    {
        animator.Play(PLAYER_HEAVY_ATTACK);
        isBusy = true;
    }
}
