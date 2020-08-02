using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;// playerı tanımlamış olduk. Unityde de bunu attachledik player diye.
    private Animator anim;
    private enum State {idle, running, jumping}
    private State state = State.idle;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //

    }

  
    // Update is called once per frame
    private void Update()
    {


        float hDirection = Input.GetAxis("Horizontal");
        

       if(hDirection<0)
        {
            rb.velocity = new Vector2(-5, rb.velocity.y); //x , y yi belirledik velocitynin içinde.
            transform.localScale = new Vector2(-1, transform.localScale.y);// karakteri flip yapar sağa sola
          
        }
       else if (hDirection>0)
        {
            rb.velocity = new Vector2(5, rb.velocity.y);//.1f yazınca ordaki f float olduğunu söyler. y kısmı gravityi belirler(y kısmına rb.velocity.y yazmak daha mantıklı.)
            transform.localScale = new Vector2(1, transform.localScale.y);
           
        

        }
        else
        {
            
            //rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, 5);
            state = State.jumping;

            //if( rb.velocity == new Vector2(rb.velocity.x, 5))
            //{
            //    if (Input.GetKey(KeyCode.Space))
            //    {
            //        rb.velocity = new Vector2(rb.velocity.x, 5);
            //    }
            //}
        }

        VelocityState();
        anim.SetInteger("state", (int)state);
    }

    private void VelocityState()
    {
        if(state == State.jumping)
        {
            //Jumping
        }
       
        else if (Mathf.Abs(rb.velocity.x) > .2f)
        {
            //Moving
            state = State.running;
        }
        else
        {
            state = State.idle;
        }

    }
}
