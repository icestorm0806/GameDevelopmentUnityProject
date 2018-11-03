﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement : MonoBehaviour
{

    private Rigidbody2D rb2D;
    private GameObject player;
    Animator animator;

    public string platformName;
    public LayerMask groundLayers;

    public float playerSpeed, jumpHight;

    public bool isgrounded;
    private bool facingLeft, facingRight;
    private float moveHorizontal;

    void Start()
    {
        isgrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - 0.05f, transform.position.y - 0.05f)
            , new Vector2(transform.position.x + 0.005f, transform.position.y - 0.051f), groundLayers);
        facingRight = true;
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isgrounded == true)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpHight);
            }

            transform.parent = null;
        }
    }

    void FixedUpdate()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        if (moveHorizontal < 0)
        {
            TurnLeft();
        }
        if (moveHorizontal > 0)
        {
            TurnRight();
        }
        animator.SetFloat("moveHorizontal", Mathf.Abs(moveHorizontal));
        moveHorizontal = moveHorizontal * Time.deltaTime * playerSpeed;
        transform.Translate(moveHorizontal, 0, 0);
        // Prevents movement beyond those x coordinates
        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, -4.85f, 4.85f),
        //transform.position.y, transform.position.z);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.gameObject.CompareTag("ground")) || (other.gameObject.CompareTag("platform")))
        {
            isgrounded = true;
        }

        if (other.gameObject.name.StartsWith("block"))
            rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if ((other.gameObject.CompareTag("ground")) || (other.gameObject.CompareTag("platform")))
        {
            isgrounded = false;
        }
    }



    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "platform")
        {
            transform.parent = other.transform;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "platform")
        {
            transform.parent = null;

        }
    }






    /*
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Platform")
        {
            transform.position = collision.transform.position;
            transform.rotation = collision.transform.rotation;
        }
    }*/


    // faces player animation left
    public void TurnLeft()
    {
        if (facingLeft)
            return;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingLeft = true;
        facingRight = false;
    }

    //faces player animation right
    public void TurnRight()
    {
        if (facingRight)
            return;
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        facingLeft = false;
        facingRight = true;
    }
}