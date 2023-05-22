using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float movePower;
    [SerializeField] private float jumpPower;

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
        Move();
    }
    private void Move()
    {
        
        if (inputDir.x < 0 && rb.velocity.x > -maxSpeed)
            rb.AddForce(Vector2.right * inputDir.x * movePower, ForceMode2D.Force);
        else if ( inputDir.x > 0 && rb.velocity.x < maxSpeed)
            rb.AddForce(Vector2.right * inputDir.x * movePower, ForceMode2D.Force);
    }
    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        
    }

    private void OnMove(InputValue value)
    {
        inputDir = value.Get<Vector2>();
        anim.SetFloat("MoveSpeed", Mathf.Abs( inputDir.x));
        if (inputDir.x > 0)
            render.flipX = false;
        else if (inputDir.x < 0)
            render.flipX = true;
        
    }
    private void OnJump(InputValue value)
    {
        if (isGround)
            Jump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGround = true;
        anim.SetBool("IsGround", true);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGround = false;
        anim.SetBool("IsGround", false); 
    }
}
