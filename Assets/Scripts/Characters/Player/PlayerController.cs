﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int playerSpeed = 10;
    public bool facingRight = true;
    public int playerJumpPower = 1;
    public float moveX;
    public Animator anim;

    public Light luzFarol;
    public bool grounded = true;

    private Rigidbody2D rb;

    private bool isTorchEnabled;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("Grounded", grounded);
        anim.SetBool("FacingRight", facingRight);
    }
    // Update is called once per frame
    void Update () {
        PlayerMove();

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (luzFarol.intensity > 0)
                luzFarol.intensity = 0;
            else if (luzFarol.intensity < 1)
                luzFarol.intensity = 1;
        }
	}

    void PlayerMove()
    {
        if (!GameController.instance.IsStopped())
        {
            // Controls
            moveX = Input.GetAxis("Horizontal");
            // Animation
            anim.SetFloat("MoveX", moveX);
            // DIRECTION

            anim.SetBool("Grounded", grounded);
            if (Input.GetButtonDown("Jump") && grounded == true)
            {
                Jump();
            }

            if (moveX < 0.0f && facingRight == true)//cuando movimiento
                                                    //menor que 0 y NO está girado hacia la derecha
            {
                FlipPlayer();
            }
            else if (moveX > 0.0f && facingRight == false)
            {
                FlipPlayer();
            }
            // Physics

            rb.velocity = new Vector2(moveX * playerSpeed, rb.velocity.y);
        }
    }

    void Jump()
    {
        // JUMP
        rb.AddForce(Vector2.up * playerJumpPower);
        grounded = false;
    }



    void FlipPlayer()
    {
        facingRight = !facingRight;
        //gameObject.GetComponent<SpriteRenderer>().flipX = facingRight;
        anim.SetBool("FacingRight",facingRight);
    }
    public bool IsTorchEnabled()
    {
        return true;
    }

 }
