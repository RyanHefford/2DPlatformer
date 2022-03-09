using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private float speed = 10;
    [SerializeField] private float jumpHeight = 12;
    private Rigidbody2D body;
    private Animator animator;
    public bool isGrounded = false;
    public float horizontalInput;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //first check if in attack animation
        if (true)//!animator.GetBool("LightAttack") && !animator.GetBool("HeavyAttack"))
        {
            //get horizontal Movement

            horizontalInput = Input.GetAxis("Horizontal");

            if (Input.GetKey(KeyCode.LeftShift))
            {
                horizontalInput /= 2;
            }

            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
            //animator.SetFloat("MoveSpeed", Mathf.Abs(body.velocity.x));

            //check flip direction
            if (horizontalInput > 0.01f)
            {
                transform.localScale = Vector3.one;
            }
            else if (horizontalInput < -0.01f)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            //check jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                body.velocity = new Vector2(body.velocity.x, jumpHeight);
                
            }

            //check for attacks
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //animator.SetBool("LightAttack", true);
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                //animator.SetBool("HeavyAttack", true);
            }
        }
        else
        {
            horizontalInput = 0;
            body.velocity = new Vector2(0, body.velocity.y);
        }

        //animator.SetFloat("VerticalVelocity", body.velocity.y);

        //animator.SetBool("IsGrounded", isGrounded);
    }

    public void FinishLightAttack()
    {
        //animator.SetBool("LightAttack", false);
    }

    public void FinishHeavyAttack()
    {
        //animator.SetBool("HeavyAttack", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Terrain"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Terrain"))
        {
            isGrounded = false;
        }
    }
}
