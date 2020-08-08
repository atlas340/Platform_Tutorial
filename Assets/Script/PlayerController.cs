using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;// playerı tanımlamış olduk. Unityde de bunu attachledik player diye.
    private Animator anim;
    private enum State { idle, running, jump, fall };
    private State state = State.idle;

    public float speed;
    public float jumpForce;

    private bool isJumping;
    

    [SerializeField] private LayerMask ground;
    private Collider2D coll;
 
    
 
    // Start is called before the first frame update
    private void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        

    }

  
    // Update is called once per frame
    private void Update()
    {

        Jump();
        float hDirection = Input.GetAxis("Horizontal");
        //float jDirection = Input.GetAxis("Jump");

        //if (rb.velocity.y < -0.1)
        //{
        //    isFalling = true;
        //}
        //else
        //{
        //    isFalling = false;
        //}

        if (hDirection<0)
        {
            rb.velocity = new Vector2(-5, rb.velocity.y); //x , y yi belirledik velocitynin içinde.
            transform.localScale = new Vector2(-1, transform.localScale.y);// karakteri flip yapar sağa sola
            

            if (isJumping)
            {
                state = State.jump;

            }
            else if (!coll.IsTouchingLayers(ground) && !isJumping)
            {
                state = State.fall;
            }
            else
            {
                state = State.running;
                isJumping = false;
            }

          
        }
       else if (hDirection>0)
        {
            rb.velocity = new Vector2(5, rb.velocity.y);//.1f yazınca ordaki f float olduğunu söyler. y kısmı gravityi belirler(y kısmına rb.velocity.y yazmak daha mantıklı.)
            transform.localScale = new Vector2(1, transform.localScale.y);
            

            if (isJumping)
            {
                state = State.jump;

            }
            else if (!coll.IsTouchingLayers(ground) && !isJumping)
            {
                state = State.fall;
            }
            else
            {
                state = State.running;
                isJumping = false;
            }


        }
        else if (!coll.IsTouchingLayers(ground) && !isJumping)
        {
            state = State.fall;
        }
        else
        {
            if (isJumping)
            {
                state = State.jump;
            }
            else
            {
                state = State.idle;
                isJumping = false;

            }

            //rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
            
        }


        //if (Input.GetKeyDown("Space")) 
        //{
            
            
            
           
            
        //    //if( rb.velocity == new Vector2(rb.velocity.x, 5))
        //    //{
        //    //    if (Input.GetKey(KeyCode.Space))
        //    //    {
        //    //        rb.velocity = new Vector2(rb.velocity.x, 5);
        //    //    }
        //    //}
            
        //}
        


        //VelocityState();
        anim.SetInteger("State", (int)state);
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;

            rb.AddForce(new Vector2(rb.velocity.x, jumpForce));
            state = State.jump;
            
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;

            rb.velocity = Vector2.zero;
        }
    }


    
    

    //private void VelocityState()
    //{
    //    if(state == State.jumping)
    //    {
    //        //Jumping
    //    }

    //    else if (Mathf.Abs(rb.velocity.x) > .2f)
    //    {
    //        //Moving
    //        state = State.running;
    //    }
    //    else
    //    {
    //        state = State.idle;
    //    }

    //}
    //
}
