using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region VARIABLES
    Vector2 moveInput;
    #endregion

    #region EVENTS
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    #endregion

    #region METHODS
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }
    #endregion
}
