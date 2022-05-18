using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprites : MonoBehaviour
{
    public enum SpriteName
    {
        Idle, StandShoot, Up, Jump, Duck, RunIdle, RunShootStraight, RunDiagonalUp, RunDiagonalDown, Die
    }

    public SpriteName currentSprite { get; private set; }

    public Sprite[] spritesIdle = new Sprite[2];

    public Sprite[] spritesStandShoot1 = new Sprite[1];
    public Sprite[] spritesStandShoot2 = new Sprite[1];

    public Sprite[] spritesUp1 = new Sprite[1];
    public Sprite[] spritesUp2 = new Sprite[1];

    public Sprite[] spritesJump = new Sprite[4];

    public Sprite[] spritesDuck1 = new Sprite[1];
    public Sprite[] spritesDuck2 = new Sprite[1];

    public Sprite[] spritesRunIdle = new Sprite[6];

    public Sprite[] spritesRunShootStraight1 = new Sprite[3];
    public Sprite[] spritesRunShootStraight2 = new Sprite[3];

    public Sprite[] spritesRunDiagonalUp1 = new Sprite[3];
    public Sprite[] spritesRunDiagonalUp2 = new Sprite[3];

    public Sprite[] spritesRunDiagonalDown1 = new Sprite[3];
    public Sprite[] spritesRunDiagonalDown2 = new Sprite[3];

    public Sprite[] spritesDie = new Sprite[5];

    public Collider2D collider2DMain;
    public Collider2D duckTrigger;
    public Collider2D jumpTrigger;
    public Collider2D normalTrigger;

    public Transform firePoint;

    private Vector3 firePointPositionStraight = new Vector3(1.3372f, 0.3892f);
    private Vector3 firePointPositionDuck = new Vector3(1.8923f, -1.0381f);
    private Vector3 firePointPositionUp = new Vector3(0.2096f, 2.0878f);
    private Vector3 firePointPositionDiagonalUp = new Vector3(1.131f, 1.287f);
    private Vector3 firePointPositionDiagonalDown = new Vector3(1.1234f, -0.346f);
    private Vector3 firePointPositionJump = Vector3.zero;

    public float animationTime;
    private float timer = 0;
    private float runShootToRunIdleTimer = 0;
    private int spriteIndex = 0;
    private bool first;
    private bool canChangeFromRunShootToRunIdle = true;

    private PlayerMovement playerMovement;
    private PlayerFire playerFire;
    private GroundCheck groundCheck;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerFire = FindObjectOfType<PlayerFire>();
        groundCheck = transform.Find(Properties.PLAYER_GROUND_CHECK_NAME).gameObject.GetComponent<GroundCheck>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        animationTime = 0.15f;
    }

    private void Start()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        timer = timer + Time.deltaTime;

        // Idle
        if ((groundCheck.isGrounded && !playerFire.isPressingFireButton && playerMovement.noMovement) || playerMovement.isFalling)
            StandShoot();

        // Jump
        if(playerMovement.isJumping && !playerMovement.isFalling)
            Jump();

        // Die

        if (groundCheck.isGrounded && !playerMovement.isFalling)
        {
            // StandShoot
            if (playerFire.isPressingFireButton && playerMovement.noMovement)
                StandShoot();

            // Up
            if (playerMovement.movementDirection.x == 0 && playerMovement.movementDirection.y > 0)
                Up();

            // Duck
            if (playerMovement.isDucking)
                Duck();

            // Idle Run and Run Straight Shoot
            if(playerMovement.isRunningStraight)
            {
                if(playerFire.fired)
                {
                    canChangeFromRunShootToRunIdle = false;
                    RunShootStraight();
                }

                else
                {
                    if (canChangeFromRunShootToRunIdle)
                        RunIdle();
                    else
                    {
                        runShootToRunIdleTimer = runShootToRunIdleTimer + Time.deltaTime;
                        if (runShootToRunIdleTimer >= animationTime)
                        {
                            runShootToRunIdleTimer = 0;
                            //RunIdle();
                            canChangeFromRunShootToRunIdle = true;
                        }

                        else
                            RunShootStraight();
                    }
                }
            }

            // RunDiagonalUp
            if (playerMovement.movementDirection.x != 0 && playerMovement.movementDirection.y > 0)
                RunDiagonalUp();

            // RunDiagonalDown
            if (playerMovement.movementDirection.x != 0 && playerMovement.movementDirection.y < 0)
                RunDiagonalDown();
        }
    }

    private bool checkAnimationChange(SpriteName spriteName)
    {
        if (currentSprite == spriteName)
        {
            if (playerFire.recoil && first)
            {
                first = false;
                SetRecoilSprite();
            }

            else if(!playerFire.recoil && !first)
            {
                first = true;
                SetRecoilSprite();
            }

            if (timer < animationTime)
                return false;

            timer = 0;
        }

        else
        {
            if (currentSprite == SpriteName.RunIdle && spriteName == SpriteName.RunShootStraight)
                spriteIndex = spriteIndex - 1;
            else
                timer = 0;

            if (spriteName == SpriteName.RunIdle || spriteName == SpriteName.RunShootStraight || spriteName == SpriteName.RunDiagonalUp || spriteName == SpriteName.RunDiagonalDown)
            {
                if (currentSprite == SpriteName.Idle || currentSprite == SpriteName.StandShoot || currentSprite == SpriteName.Up)
                    spriteIndex = 1;

                else if (!(currentSprite == SpriteName.RunIdle || currentSprite == SpriteName.RunShootStraight || currentSprite == SpriteName.RunDiagonalUp || currentSprite == SpriteName.RunDiagonalDown))
                    spriteIndex = 0;
            }

            else
                spriteIndex = 0;

            if((spriteName == SpriteName.Die || spriteName == SpriteName.Duck) && (currentSprite != SpriteName.Duck || currentSprite != SpriteName.Die))
            {
                collider2DMain.enabled = true;
                normalTrigger.enabled = jumpTrigger.enabled = false;
                duckTrigger.enabled = true;
            }

            else if(spriteName == SpriteName.Jump)
            {
                normalTrigger.enabled = duckTrigger.enabled = false;
                jumpTrigger.enabled = true;
                collider2DMain.enabled = false;
            }

            // Normal/standing sprite
            else if(currentSprite == SpriteName.Jump || currentSprite == SpriteName.Die || currentSprite == SpriteName.Duck)
            {
                collider2DMain.enabled = true;
                jumpTrigger.enabled = duckTrigger.enabled = false;
                normalTrigger.enabled = true;
            }
        }

        return true;
    }

    private void StandShoot()
    {
        if (!checkAnimationChange(SpriteName.StandShoot))
            return;

        if (transform.eulerAngles.y == 0)
            firePoint.position = transform.position + firePointPositionStraight;
        else
            firePoint.position = new Vector3(transform.position.x - firePointPositionStraight.x, transform.position.y + firePointPositionStraight.y);

        spriteIndex = spriteIndex % spritesStandShoot1.Length;
        spriteRenderer.sprite = (first)? spritesStandShoot1[spriteIndex] : spritesStandShoot2[spriteIndex];
        spriteIndex = (spriteIndex + 1) % spritesStandShoot1.Length;
        currentSprite = SpriteName.StandShoot;
    }

    private void Up()
    {
        if (!checkAnimationChange(SpriteName.Up))
            return;

        if (transform.eulerAngles.y == 0)
            firePoint.position = transform.position + firePointPositionUp;
        else
            firePoint.position = new Vector3(transform.position.x - firePointPositionUp.x, transform.position.y + firePointPositionUp.y);

        spriteIndex = spriteIndex % spritesRunDiagonalUp1.Length;
        spriteRenderer.sprite = (first)? spritesUp1[spriteIndex] : spritesUp2[spriteIndex];
        spriteIndex = (spriteIndex + 1) % spritesUp1.Length;
        currentSprite = SpriteName.Up;
    }

    private void Jump()
    {
        if (!checkAnimationChange(SpriteName.Jump))
            return;

        firePoint.position = transform.position + firePointPositionJump;
        firePoint.eulerAngles = transform.eulerAngles;

        spriteIndex = spriteIndex % spritesJump.Length;
        spriteRenderer.sprite = spritesJump[spriteIndex];
        spriteIndex = (spriteIndex + 1) % spritesJump.Length;
        currentSprite = SpriteName.Jump;
    }

    private void Duck()
    {
        if (!checkAnimationChange(SpriteName.Duck))
            return;

        if (transform.eulerAngles.y == 0)
            firePoint.position = transform.position + firePointPositionDuck;
        else
            firePoint.position = new Vector3(transform.position.x - firePointPositionDuck.x, transform.position.y + firePointPositionDuck.y);

        spriteIndex = spriteIndex % spritesDuck1.Length;
        spriteRenderer.sprite = (first)? spritesDuck1[spriteIndex] : spritesDuck2[spriteIndex];
        spriteIndex = (spriteIndex + 1) % spritesDuck1.Length;
        currentSprite = SpriteName.Duck;
    }

    private void RunIdle()
    {
        if (!checkAnimationChange(SpriteName.RunIdle))
            return;

        if (transform.eulerAngles.y == 0)
            firePoint.position = transform.position + firePointPositionStraight;
        else
            firePoint.position = new Vector3(transform.position.x - firePointPositionStraight.x, transform.position.y + firePointPositionStraight.y);

        spriteIndex = spriteIndex % spritesRunIdle.Length;
        spriteRenderer.sprite = spritesRunIdle[spriteIndex];
        spriteIndex = (spriteIndex + 1) % spritesRunIdle.Length;
        currentSprite = SpriteName.RunIdle;
    }

    private void RunShootStraight()
    {
        if (!checkAnimationChange(SpriteName.RunShootStraight))
            return;

        if (transform.eulerAngles.y == 0)
            firePoint.position = transform.position + firePointPositionStraight;
        else
            firePoint.position = new Vector3(transform.position.x - firePointPositionStraight.x, transform.position.y + firePointPositionStraight.y);
        
        spriteIndex = spriteIndex % spritesRunShootStraight1.Length;
        spriteRenderer.sprite = (first)? spritesRunShootStraight1[spriteIndex] : spritesRunShootStraight2[spriteIndex];
        spriteIndex = (spriteIndex + 1) % spritesRunShootStraight1.Length;
        currentSprite = SpriteName.RunShootStraight;
    }

    private void RunDiagonalUp()
    {
        if (!checkAnimationChange(SpriteName.RunDiagonalUp))
            return;

        if (transform.eulerAngles.y == 0)
            firePoint.position = transform.position + firePointPositionDiagonalUp;
        else
            firePoint.position = new Vector3(transform.position.x - firePointPositionDiagonalUp.x, transform.position.y + firePointPositionDiagonalUp.y);

        spriteIndex = spriteIndex % spritesRunDiagonalUp1.Length;
        spriteRenderer.sprite = (first)? spritesRunDiagonalUp1[spriteIndex] : spritesRunDiagonalUp2[spriteIndex];
        spriteIndex = (spriteIndex + 1) % spritesRunDiagonalUp1.Length;
        currentSprite = SpriteName.RunDiagonalUp;
    }

    private void RunDiagonalDown()
    {
        if (!checkAnimationChange(SpriteName.RunDiagonalDown))
            return;

        if (transform.eulerAngles.y == 0)
            firePoint.position = transform.position + firePointPositionDiagonalDown;
        else
            firePoint.position = new Vector3(transform.position.x - firePointPositionDiagonalDown.x, transform.position.y + firePointPositionDiagonalDown.y);

        spriteIndex = spriteIndex % spritesRunDiagonalDown1.Length;
        spriteRenderer.sprite = (first)? spritesRunDiagonalDown1[spriteIndex] : spritesRunDiagonalDown2[spriteIndex];
        spriteIndex = (spriteIndex + 1) % spritesRunDiagonalDown1.Length;
        currentSprite = SpriteName.RunDiagonalDown;
    }

    private void Die()
    {
        if (!checkAnimationChange(SpriteName.Die))
            return;

        spriteIndex = spriteIndex % spritesDie.Length;
        spriteRenderer.sprite = spritesDie[0];
        currentSprite = SpriteName.Die;
    }

    private void SetRecoilSprite()
    {
        switch(currentSprite)
        {
            case SpriteName.StandShoot:
                spriteRenderer.sprite = (first)? spritesStandShoot1[spriteIndex] : spritesStandShoot2[spriteIndex];
                break;

            case SpriteName.Up:
                spriteRenderer.sprite = (first)? spritesUp1[spriteIndex] : spritesUp2[spriteIndex];
                break;

            case SpriteName.Duck:
                spriteRenderer.sprite = (first)? spritesDuck1[spriteIndex] : spritesDuck2[spriteIndex];
                break;

            case SpriteName.RunShootStraight:
                spriteRenderer.sprite = (first)? spritesRunShootStraight1[spriteIndex] : spritesRunShootStraight2[spriteIndex];
                break;

            case SpriteName.RunDiagonalUp:
                spriteRenderer.sprite = (first)? spritesRunDiagonalUp1[spriteIndex] : spritesRunDiagonalUp2[spriteIndex];
                break;

            case SpriteName.RunDiagonalDown:
                spriteRenderer.sprite = (first)? spritesRunDiagonalDown1[spriteIndex] : spritesRunDiagonalDown2[spriteIndex];
                break;
        }
    }
}