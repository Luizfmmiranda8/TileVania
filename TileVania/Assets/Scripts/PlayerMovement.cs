using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] float runSpeed = 10f;
    Vector2 moveInput;
    Rigidbody2D rb;
    Animator myAnimator;
    #endregion

    #region EVENTS
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
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
        Debug.Log(moveInput);
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
