using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 2f;
    Vector2 moveInput;
    Rigidbody2D playerRigidbody;
    Animator playerAnimator;
    CapsuleCollider2D playerBodyCollider;
    BoxCollider2D playerFeetCollider;
    float gravityScaleAtStart;
    #endregion

    #region EVENTS
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        gravityScaleAtStart = playerRigidbody.gravityScale;
        playerAnimator = GetComponent<Animator>();
        playerBodyCollider = GetComponent<CapsuleCollider2D>();
        playerFeetCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }
    #endregion

    #region METHODS
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if(playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && value.isPressed)
        {
            playerRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, playerRigidbody.velocity.y);
        playerRigidbody.velocity = playerVelocity;

        bool isPlayerMoving = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;
        playerAnimator.SetBool("isRunning", isPlayerMoving);
    }

    void FlipSprite()
    {
        bool isPlayerMoving = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;

        if(isPlayerMoving)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidbody.velocity.x), 1f);
        }
    }

    void ClimbLadder()
    {
        if(!playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            playerRigidbody.gravityScale = gravityScaleAtStart;
            return;
        }

        Vector2 climbVelocity = new Vector2(playerRigidbody.velocity.x, moveInput.y * climbSpeed);
        playerRigidbody.velocity = climbVelocity;
        playerRigidbody.gravityScale = 0f;

        bool isPlayerClimbing = Mathf.Abs(playerRigidbody.velocity.y) > Mathf.Epsilon;
        playerAnimator.SetBool("isClimbing", isPlayerClimbing);
    }
    #endregion
}
