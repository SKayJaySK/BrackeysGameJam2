using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2Controller : MonoBehaviour
{
    public KeyCode up = KeyCode.UpArrow;
    public KeyCode down = KeyCode.DownArrow;
    public KeyCode left = KeyCode.LeftArrow;
    public KeyCode right = KeyCode.RightArrow;

    [Space]
    public float jumpForce;
    public float moveSpeed;
    public float dashMultiplier;
    public float dashTime;
    public float leastTimeForDash;
    public float mostTimeForDash;
    public float dashDelay;

    [Space]
    public int numberOfDownDashOnPlayer1;

    [Space]
    public Image DashHealth;

    [Space]
    public LayerMask player1;
    
    int moveDir, inputLCounter, inputRCounter, downDashCount, turn;
    float timer, originalJumpForce, lastDash, downDashTime;
    bool isGrounded, overriding, startCouroutine, dashDown, sidePlayer, topPlayer;

    GameObject otherPlayer;
    Rigidbody2D rb;

    private void Start()
    {
        otherPlayer = FindObjectOfType<Player1Controller>().gameObject;
        rb = GetComponent<Rigidbody2D>();
        isGrounded = overriding = false;
        startCouroutine = true;
        moveDir = 0; // moveDir : 1 Right Dash -1 Left Dash -3 Left 3 Right 2 Up -2 Down 0 Stationary
        inputLCounter = inputRCounter = 0;
        originalJumpForce = jumpForce;
        dashDown = false;
        downDashCount = 0;
        downDashTime = Time.time;
    }

    private void Update()
    {
        TurnUpdate();
        RayCasting();
        Move();
        UpdateDash();
    }

    void TurnUpdate()
    {
        if (transform.position.y > otherPlayer.transform.position.y)
            turn = 2;
        else turn = 1;
    }

    void UpdateDash()
    {
        DashHealth.fillAmount = Mathf.Clamp(1 - (((lastDash + dashDelay) - Time.time) / dashDelay), 0, 1);
    }

    void RayCasting()
    {
        if (Physics2D.Raycast(new Vector2(transform.position.x - transform.localScale.x / 2 + 0.05f, transform.position.y - transform.localScale.y / 2 + 0.05f), Vector2.left, 0.1f, player1) || Physics2D.Raycast(new Vector2(transform.position.x + transform.localScale.x / 2 - 0.05f, transform.position.y - transform.localScale.y / 2 + 0.05f), Vector3.right, 0.1f, player1))
            sidePlayer = true;
        else sidePlayer = false;
    }

    void Move()
    {
        /*if (!Input.GetKey(up) && !Input.GetKey(down) && !Input.GetKey(left) && !Input.GetKey(right) && isGrounded && !overriding && !sidePlayer)
        {
            if (moveDir != 2)
            {
                rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
                moveDir = 0;
                rb.velocity = new Vector2(0, -jumpForce * Time.deltaTime);
            }
        }*/

        if (downDashCount == 0)
            jumpForce = originalJumpForce;

        if (Input.GetKeyDown(up) && isGrounded && !sidePlayer)
        {
            //rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
            moveDir = 2;
        }

        if (Input.GetKey(left))
        {
            //rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.velocity = new Vector2(-moveSpeed * Time.deltaTime, rb.velocity.y);
            if (inputLCounter == 0)
            {
                inputLCounter = 1;
                timer = Time.time;
            }
            moveDir = -3;
        }

        if (Input.GetKey(right))
        {
            //rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.velocity = new Vector2(moveSpeed * Time.deltaTime, rb.velocity.y);
            if (inputRCounter == 0)
            {
                inputRCounter = 1;
                timer = Time.time;
            }
            moveDir = 3;
        }

        if (Input.GetKeyUp(left) || Input.GetKeyUp(right))
        {
            moveDir = 0;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (Input.GetKeyDown(left) && Time.time - timer < mostTimeForDash && Time.time - timer > leastTimeForDash && Time.time - lastDash > dashDelay && inputLCounter == 1 && !overriding)
        {
            //rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.velocity = new Vector2(-moveSpeed * dashMultiplier * Time.deltaTime, rb.velocity.y);
            moveDir = -1;
            lastDash = Time.time;
        }

        if (Input.GetKeyDown(right) && Time.time - timer < mostTimeForDash && Time.time - timer > leastTimeForDash && Time.time - lastDash > dashDelay && inputRCounter == 1 && !overriding)
        {
            //rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.velocity = new Vector2(moveSpeed * dashMultiplier * Time.deltaTime, rb.velocity.y);
            moveDir = 1;
            lastDash = Time.time;
        }

        if (Input.GetKey(down) && !overriding)
        {
            //rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.velocity = new Vector2(rb.velocity.x, -jumpForce * 2 * Time.deltaTime);
            if (!isGrounded)
            {
                moveDir = -2;
                dashDown = true;
            }
        }

        if ((Time.time - timer >= mostTimeForDash || moveDir == 1 || moveDir == -1) && !overriding)
        {
            //rb.velocity = new Vector2(0, rb.velocity.y);
            inputLCounter = 0;
            inputRCounter = 0;
        }

        if (moveDir == 0 || moveDir == 3 || moveDir == -3)
            overriding = false;

        if (overriding)
        {
            switch (moveDir)
            {
                case -2:
                    if (dashDown && downDashCount < numberOfDownDashOnPlayer1 && Time.time - downDashTime > 1)
                    {
                        dashDown = false;
                        //rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                        rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
                        //rb.AddForce(new Vector2(rb.velocity.x, jumpForce * Time.deltaTime), ForceMode2D.Impulse);
                        downDashCount++;
                        if (jumpForce < 1200)
                            jumpForce += jumpForce * 0.1f;
                    }
                    break;
                case -1:
                    //rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                    rb.velocity = new Vector2(moveSpeed * 2.5f * Time.deltaTime, rb.velocity.y);
                    break;
                case 1:
                    //rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                    rb.velocity = new Vector2(-moveSpeed * 2.5f * Time.deltaTime, rb.velocity.y);
                    break;
            }

            if (startCouroutine)
                StartCoroutine("Delay");
        }

        if (topPlayer && !sidePlayer && turn != 2)
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        else rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;

        if (collision.gameObject.tag == "Player")
        {
            overriding = true;
            if (moveDir == -2 && downDashCount > numberOfDownDashOnPlayer1 - 1)
            {
                jumpForce = originalJumpForce;
                downDashCount = 0;
                downDashTime = Time.time;
            }

            if (!sidePlayer)
                topPlayer = true;
        }
        else if (moveDir != -2 && collision.gameObject.tag != "Player")
        {
            jumpForce = originalJumpForce;
            dashDown = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;

        if (collision.gameObject.tag == "Player")
        {
            overriding = true;
            if (!sidePlayer)
                topPlayer = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;

        if (collision.gameObject.tag == "Player")
        {
            topPlayer = false;
        }
    }

    IEnumerator Delay()
    {
        startCouroutine = false;
        yield return new WaitForSeconds(0.5f);
        overriding = false;
        startCouroutine = true;
        moveDir = 0;
    }
}