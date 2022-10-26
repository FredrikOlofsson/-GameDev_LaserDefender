using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector2 rawInput;
    Rigidbody2D rb;

    Vector2 minGameFieldBounds;
    Vector2 maxGameFieldBounds;

    [Header("Player Movement")]
    [Range(5f, 15f)][SerializeField] float moveSpeed = 9f;



    [Header("GameField Settings")]
    [Range(0, 5)][SerializeField] int paddingTop;
    [Range(0, 5)][SerializeField] int paddingBottom;

    Shooter shooter;

    void Awake()
    {
        shooter = GetComponent<Shooter>();
    }
    void Start()
    {
        initBounds();
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        processMovementOfShip();
    }
    private void initBounds()
    {
        Camera mainCamera = Camera.main;
        minGameFieldBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxGameFieldBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));

        //Adjust screenbounds based on player sprite
        SpriteRenderer playerSprite = GetComponentInChildren<SpriteRenderer>();
        Vector2 spriteBounds = new Vector2(playerSprite.bounds.extents.x, playerSprite.bounds.extents.y);

        //Traps the sprite of the player inside the playable area
        minGameFieldBounds.x += spriteBounds.x;
        minGameFieldBounds.y += spriteBounds.y;
        maxGameFieldBounds.x -= spriteBounds.x;
        maxGameFieldBounds.y -= spriteBounds.y;

        //Player adjusted padding
        if (paddingTop > 0 || paddingBottom > 0)
        {
            minGameFieldBounds.y += paddingBottom;
            maxGameFieldBounds.y -= paddingTop;
        }
    }

    private void processMovementOfShip()
    {
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 clampedPos = new Vector2();
        clampedPos.x = Mathf.Clamp(transform.position.x + delta.x, minGameFieldBounds.x, maxGameFieldBounds.x);
        clampedPos.y = Mathf.Clamp(transform.position.y + delta.y, minGameFieldBounds.y, maxGameFieldBounds.y);
        transform.position = clampedPos;
    }

    private void OnDrowning()
    {
        print("I am drowning!");
    }
    private void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }
    private void OnFire(InputValue value)
    {
        if (shooter != null)
        {
            shooter.isFireing = value.isPressed;
        }
    }
}
