using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float movePower;
    [SerializeField] private float jumpPower = 7f;
    [SerializeField] private float walkMovePower = 5f;
    [SerializeField] private float walkMaxSpeed = 5f;
    [SerializeField] private float runMovePower = 5f;
    [SerializeField] private float runMaxSpeed = 5f;
    public int keypress;

    private Rigidbody2D rb;
    private Vector2 inputDir;
    private Animator anim;
    private SpriteRenderer render;
    private bool isGround;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        //Move();
        WalkOrRun();
        
    }
    private void WalkOrRun()
    {
        

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            keypress++;
        if (keypress == 1)
        {
            movePower = walkMovePower;
            maxSpeed = walkMaxSpeed;
        }
        if (keypress == 2)
        {
            movePower = runMovePower;
            maxSpeed = runMaxSpeed;
        }
        if (keypress == 3)
            keypress = 1;

        Move();
    }
    private void Move()
    {
        
        if (inputDir.x < 0 && rb.velocity.x > -maxSpeed)
        {
            rb.AddForce(Vector2.right * inputDir.x * movePower, ForceMode2D.Force);
        }
        else if (inputDir.x > 0 && rb.velocity.x < maxSpeed)
        {
            rb.AddForce(Vector2.right * inputDir.x * movePower, ForceMode2D.Force);
        }

    }
    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        anim.SetBool("IsJump", true);
    }
    private void OnMove(InputValue value)
    {
        inputDir = value.Get<Vector2>();
        
        if (inputDir.x > 0) 
            render.flipX = false;
        else if (inputDir.x < 0)
            render.flipX = true;
        anim.SetBool("IsWalk", true);
        if (inputDir.x == 0)
            anim.SetBool("IsWalk", false);

    }
    private void OnJump(InputValue value)
    {
        if (isGround)
            Jump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGround = true;
        anim.SetBool("IsJump", false);
        //anim.SetBool("IsGround", true);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGround = false;
        //anim.SetBool("IsGround", false); 
    }
}
