using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    #region Variables
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D enemyRigidbody;
    #endregion

    #region EVENTS
    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        enemyRigidbody.velocity = new Vector2(moveSpeed, 0);
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        moveSpeed *= -1f;

        FlipEnemySprite();
    }
    #endregion

    #region METHODS
    void FlipEnemySprite()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(enemyRigidbody.velocity.x)), 1f);
    }
    #endregion
}
