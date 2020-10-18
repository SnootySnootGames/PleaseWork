﻿using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f; //Used to determine how fast the player speed will be, can be set in inspector
    [SerializeField] private LayerMask collidableObject;
    [SerializeField] private float speedBoostTimeLimit = 0.4f;
    [SerializeField] private float speedBoostTimer = 0.4f;
    [SerializeField] private GameObject afterImagePrefab;
    [SerializeField] private Animator playerAni;
    [SerializeField] private float afterImageTimeWait = 8f;

    private Vector2 BoxCastSize = new Vector2(0.9f, 1f);
    private Vector2 CastRightDirection = new Vector2(1f, 0f);
    private Vector2 CastLeftDirection = new Vector2(-1f, 0f);
    private Vector2 CastUpDirection = new Vector2(0f, 1f);
    private Vector2 CastDownDirection = new Vector2(0f, -1f);
    private bool afterImageCanInstantiate = true;
    private float baseMoveSpeed = 8f;
    
    private Transform body;
    private float horizontal; //Used to store player input for x axis movement
    private float vertical;
    private float DiagnolSpeedReduction = 0.7f;
    private bool rightMovementAllowed = true;
    private bool lefttMovementAllowed = true;
    private bool upMovementAllowed = true;
    private bool downMovementAllowed = true;

    public static bool speedBoostBool = false;
    public bool isMoving = false; //public bool used to let other potential scripts to know player is moving

    // Start is called before the first frame update
    private void Start()
    {
        speedBoostTimer = speedBoostTimeLimit;
        baseMoveSpeed = moveSpeed;
        body = transform;
    }

    // Update is called once per frame
    private void Update() //used for non physic update methods, player input collection,check if player has moved logic...
    {
        DetectCollisions();
        InputCollection();
        CheckMovement();
        //Movement();
        SpeedBoost();
        PlayerAnimation();
    }

    private void InputCollection()
    {
        //Gives a value between -1 and 1. Uses old input manager. Can be viewd in Unity via Edit>Project Settings>Input Manager>Axes drop down arrow
        horizontal = Input.GetAxisRaw("Horizontal"); //-1 is left,use getaxisraw if you want snappy controls
        vertical = Input.GetAxisRaw("Vertical");
        Movement();
        PlayerIsStuckMethod();
    }

    private void Movement() //physics behind horizontal movement
    {
        float horizontalSpeed = horizontal * moveSpeed; //store product of horizontal (-1,0,1) and moveSpeed
        float verticalSpeed = vertical * moveSpeed;

        if (horizontal > 0 && rightMovementAllowed == false)
        {
            horizontalSpeed = 0;
        }
        
        if (horizontal < 0 && lefttMovementAllowed == false)
        {
            horizontalSpeed = 0;
        }

        if (vertical > 0 && upMovementAllowed == false)
        {
            verticalSpeed = 0;
        }

        if (vertical < 0 && downMovementAllowed == false)
        {
            verticalSpeed = 0;
        }

        if (horizontal != 0 && vertical != 0)
        {
            body.position += new Vector3(horizontalSpeed * DiagnolSpeedReduction * Time.deltaTime, verticalSpeed * DiagnolSpeedReduction * Time.deltaTime, 0f);
        }
        else
        {
            body.position += new Vector3(horizontalSpeed * Time.deltaTime, verticalSpeed * Time.deltaTime, 0f); //set the RB velocity to horizontalSpeed, results in horizontal player movement
        }
    }

    private void CheckMovement()
    {
        if (horizontal != 0 || vertical != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    private void DetectCollisions()
    {
        Vector2 pos = new Vector2(transform.position.x - 0.26f, transform.position.y - 1.1f);

        RaycastHit2D hitRight = Physics2D.BoxCast(pos, BoxCastSize, 0f, CastRightDirection, 0.3f, collidableObject);
        RaycastHit2D hitLeft = Physics2D.BoxCast(pos, BoxCastSize, 0f, CastLeftDirection, 0.3f, collidableObject);
        RaycastHit2D hitUp = Physics2D.BoxCast(pos, BoxCastSize, 0f, CastUpDirection, 0.3f, collidableObject);
        RaycastHit2D hitDown = Physics2D.BoxCast(pos, BoxCastSize, 0f, CastDownDirection, 0.3f, collidableObject);

        if (hitRight.collider != null)
        {
            rightMovementAllowed = false;
        }
        else
        {
            rightMovementAllowed = true;
        }

        if (hitLeft.collider != null)
        {
            lefttMovementAllowed = false;
        }
        else
        {
            lefttMovementAllowed = true;
        }

        if (hitUp.collider != null)
        {
            upMovementAllowed = false;
        }
        else
        {
            upMovementAllowed = true;
        }

        if (hitDown.collider != null)
        {
            downMovementAllowed = false;
        }
        else
        {
            downMovementAllowed = true;
        }
    }

    private void SpeedBoost()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            speedBoostBool = true;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (speedBoostBool == true)
            {
                moveSpeed = baseMoveSpeed * 3;
                if (speedBoostTimer > 0)
                {
                    speedBoostTimer -= Time.deltaTime;
                    if (afterImageCanInstantiate == true)
                    {
                        StartCoroutine(AfterImageRoutine());
                    }
                }
                else
                {
                    speedBoostBool = false;
                    moveSpeed = baseMoveSpeed;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            speedBoostBool = false;
            moveSpeed = baseMoveSpeed;
            afterImageCanInstantiate = true;
        }

        if (speedBoostBool == false && speedBoostTimer < speedBoostTimeLimit)
        {
            speedBoostTimer += Time.deltaTime;
        }
        else if (speedBoostTimer > speedBoostTimeLimit)
        {
            speedBoostTimer = speedBoostTimeLimit;
        }
    }

    private void InstantiateAfterImage()
    {
        Instantiate(afterImagePrefab, transform.position, Quaternion.identity);
    }

    private IEnumerator AfterImageRoutine()
    {
        afterImageCanInstantiate = false;
        InstantiateAfterImage();
        yield return new WaitForSeconds(speedBoostTimeLimit/afterImageTimeWait);
        afterImageCanInstantiate = true;
    }

    private void PlayerAnimation()
    {

        if (GameMasterScript.characterSelection == true)
        {
            if (isMoving == false)
            {
                playerAni.Play("girl-idle");
            }
            else if (horizontal == 1)
            {
                playerAni.Play("girl-walk-right");
            }
            else if (horizontal == -1)
            {
                playerAni.Play("girl-walk-left");
            }
            else if (vertical == 1)
            {
                playerAni.Play("girl-walk-up");
            }
            else if (vertical == -1)
            {
                playerAni.Play("girl-walk-down");
            }
        }
        else
        {
            if (isMoving == false)
            {
                playerAni.Play("boy-idle");
            }
            else if (horizontal == 1)
            {
                playerAni.Play("boy-walk-right");
            }
            else if (horizontal == -1)
            {
                playerAni.Play("boy-walk-left");
            }
            else if (vertical == 1)
            {
                playerAni.Play("boy-walk-up");
            }
            else if (vertical == -1)
            {
                playerAni.Play("boy-walk-down");
            }
        }
    }

    private void PlayerIsStuckMethod()
    {
        if (rightMovementAllowed == false && lefttMovementAllowed == false && upMovementAllowed == false && downMovementAllowed == false)
        {
            gameObject.transform.position = new Vector3(0, 0, 0);
        }
    }
}
