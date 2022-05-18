using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteController : MonoBehaviour
{
    private enum Sprites {IDLE, DUCK, JUMP, RUN, STRAIGHT_UP, DIAGONAL_UP, DIAGONAL_DOWN}

    private SpriteRenderer spriteRenderer;

    public Sprite spritePlayerIdleNotShooting;  // Player's idle sprite
    public Sprite spritePlayerIdleShooting;     // Player's idle sprite

    public Sprite spritePlayerDuckNotShooting;  // Player's duck sprite
    public Sprite spritePlayerDuckShooting;     // Player's duck sprite

    public Sprite spritePlayerJump;     // Player's jump sprite

    public Sprite spritePlayerRunNotShooting;
    public Sprite spritePlayerRunShooting;

    public Sprite spritePlayerStraightUpNotShooting;
    public Sprite spritePlayerStraightUpShooting;

    public Sprite spritePlayerDiagonalUpNotShooting;
    public Sprite spritePlayerDiagonalUpShooting;

    public Sprite spritePlayerDiagonalDownNotShooting;
    public Sprite spritePlayerDiagonalDownShooting;

    //private Sprites currentSprite;

    public bool isShooting;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void SetPlayerDuckSprite()
    {
        if(isShooting)
            spriteRenderer.sprite = spritePlayerDuckShooting;

        else
            spriteRenderer.sprite = spritePlayerDuckNotShooting;

        // Removing every collider
        foreach (Collider2D collider2D in gameObject.GetComponents<Collider2D>())
            Destroy(collider2D);

        // Adding a new collider
        gameObject.AddComponent<BoxCollider2D>();

        // Adding another collider to act as a trigger
        BoxCollider2D boxCollider2D = gameObject.AddComponent<BoxCollider2D>();
        boxCollider2D.isTrigger = true;

        isShooting = false;
        //currentSprite = Sprites.DUCK;
    }

    public void SetPlayerIdleSprite()
    {
        if(isShooting)
            spriteRenderer.sprite = spritePlayerIdleShooting;

        else
            spriteRenderer.sprite = spritePlayerIdleNotShooting;

        // Removing every collider
        foreach (Collider2D collider2D in gameObject.GetComponents<Collider2D>())
            Destroy(collider2D);

        // Adding a new collider
        gameObject.AddComponent<CapsuleCollider2D>();

        // Adding another collider to act as a trigger
        CapsuleCollider2D capsuleCollider2D = gameObject.AddComponent<CapsuleCollider2D>();
        capsuleCollider2D.isTrigger = true;

        isShooting = false;
        //currentSprite = Sprites.IDLE;
    }

    public void SetPlayerJumpSprite()
    {
        spriteRenderer.sprite = spritePlayerJump;

        // Removing every collider
        foreach (Collider2D collider2D in gameObject.GetComponents<Collider2D>())
            Destroy(collider2D);

        // Adding a new collider
        gameObject.AddComponent<CircleCollider2D>();

        // Adding another collider to act as a trigger
        CircleCollider2D circleCollider2D = gameObject.AddComponent<CircleCollider2D>();
        circleCollider2D.isTrigger = true;

        isShooting = false;
        //currentSprite = Sprites.JUMP;
    }

    public void SetPlayerRunSprite()
    {
        if (isShooting)
            spriteRenderer.sprite = spritePlayerRunShooting;

        else
            spriteRenderer.sprite = spritePlayerRunNotShooting;

        // Removing every collider
        foreach (Collider2D collider2D in gameObject.GetComponents<Collider2D>())
            Destroy(collider2D);

        // Adding a new collider
        gameObject.AddComponent<CapsuleCollider2D>();

        // Adding another collider to act as a trigger
        CapsuleCollider2D capsuleCollider2D = gameObject.AddComponent<CapsuleCollider2D>();
        capsuleCollider2D.isTrigger = true;

        isShooting = false;
        //currentSprite = Sprites.RUN;
    }

    public void SetPlayerStraightUpSprite()
    {
        if (isShooting)
            spriteRenderer.sprite = spritePlayerStraightUpShooting;

        else
            spriteRenderer.sprite = spritePlayerStraightUpNotShooting;

        // Removing every collider
        foreach (Collider2D collider2D in gameObject.GetComponents<Collider2D>())
            Destroy(collider2D);

        // Adding a new collider
        gameObject.AddComponent<CapsuleCollider2D>();

        // Adding another collider to act as a trigger
        CapsuleCollider2D capsuleCollider2D = gameObject.AddComponent<CapsuleCollider2D>();
        capsuleCollider2D.isTrigger = true;

        isShooting = false;
        //currentSprite = Sprites.STRAIGHT_UP;
    }

    public void SetPlayerDiagonalUpSprite()
    {
        if (isShooting)
            spriteRenderer.sprite = spritePlayerDiagonalUpShooting;

        else
            spriteRenderer.sprite = spritePlayerDiagonalUpNotShooting;

        // Removing every collider
        foreach (Collider2D collider2D in gameObject.GetComponents<Collider2D>())
            Destroy(collider2D);

        // Adding a new collider
        gameObject.AddComponent<CapsuleCollider2D>();

        // Adding another collider to act as a trigger
        CapsuleCollider2D capsuleCollider2D = gameObject.AddComponent<CapsuleCollider2D>();
        capsuleCollider2D.isTrigger = true;

        isShooting = false;
        //currentSprite = Sprites.DIAGONAL_UP;
    }

    public void SetPlayerDiagonalDownSprite()
    {
        if (isShooting)
            spriteRenderer.sprite = spritePlayerDiagonalDownShooting;

        else
            spriteRenderer.sprite = spritePlayerDiagonalDownNotShooting;

        // Removing every collider
        foreach (Collider2D collider2D in gameObject.GetComponents<Collider2D>())
            Destroy(collider2D);

        // Adding a new collider
        gameObject.AddComponent<CapsuleCollider2D>();

        // Adding another collider to act as a trigger
        CapsuleCollider2D capsuleCollider2D = gameObject.AddComponent<CapsuleCollider2D>();
        capsuleCollider2D.isTrigger = true;

        isShooting = false;
        //currentSprite = Sprites.DIAGONAL_DOWN;
    }
}