using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    Vector2 moveInput;
    Rigidbody2D rb;
    Animator myAnimator;
    CapsuleCollider2D myCollider;
    #endregion

    #region EVENTS
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        Run();
        FlipSprite();
    }
    #endregion

    #region METHODS
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if(myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && value.isPressed)
        {
            rb.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;

        bool isPlayerMoving = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", isPlayerMoving);
    }

    void FlipSprite()
    {
        bool isPlayerMoving = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;

        if(isPlayerMoving)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }
    #endregion
}
