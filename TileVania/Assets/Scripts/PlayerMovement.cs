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
    [SerializeField] float shootTime = 2f;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    Vector2 moveInput;
    Rigidbody2D playerRigidbody;
    Animator playerAnimator;
    CapsuleCollider2D playerBodyCollider;
    BoxCollider2D playerFeetCollider;
    float gravityScaleAtStart;
    float shootTimeAtStart;
    bool isAlive = true;
    bool canShoot = true;
    bool isShooting = false;
    #endregion

    #region EVENTS
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        gravityScaleAtStart = playerRigidbody.gravityScale;
        playerAnimator = GetComponent<Animator>();
        playerBodyCollider = GetComponent<CapsuleCollider2D>();
        playerFeetCollider = GetComponent<BoxCollider2D>();
        shootTimeAtStart = shootTime;
    }

    void Update()
    {
        if(isAlive)
        {
            Run();
            FlipSprite();
            ClimbLadder();
            if(isShooting)
            {
                ShootTimer();
            }
        }

        if(playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            Die();
        }
    }
    #endregion

    #region METHODS
    void OnMove(InputValue value)
    {
        if(isAlive)
        {
            moveInput = value.Get<Vector2>();
        }
    }

    void OnJump(InputValue value)
    {
        if(isAlive)
        {
            if(playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && value.isPressed)
            {
                playerRigidbody.velocity += new Vector2(0f, jumpSpeed);
            }
        }
    }

    void OnFire(InputValue value)
    {
        if(isAlive)
        {
            if(canShoot)
            {
                playerAnimator.SetBool("isShooting", true);
                Instantiate(bullet, gun.position, bullet.transform.rotation);

                isShooting = true;
                canShoot = false;          
            }
        }
    }

    void ShootTimer()
    {
        shootTime -= Time.deltaTime;

        if(shootTime <= 0)
        {
            playerAnimator.SetBool("isShooting", false);
            isShooting = false;
            canShoot = true;
            shootTime = shootTimeAtStart;
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

    void Die()
    {
        isAlive = false;

        playerAnimator.SetTrigger("isDead");
        playerRigidbody.velocity = deathKick;

        gameOverPanel.gameObject.SetActive(true);
    }
    #endregion
}
