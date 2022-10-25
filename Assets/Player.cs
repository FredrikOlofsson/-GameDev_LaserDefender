using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D rb;

    [SerializeField] float moveSpeed = 2f;

    void Update()
    {
        rb.velocity = new Vector2(moveInput.x, moveInput.y);
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}
