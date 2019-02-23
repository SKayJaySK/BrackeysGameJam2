using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int jumpCount = 2;

    [Space]
    public LayerMask player1, wall;
    
    int moveDir, inputCounterL, inputCounterR, downDashCount;
    float timer, originalJumpForce, lastDash, downDashTime;
    bool isGrounded, overriding, startCouroutine, dashDown, dashingBack, leftWall, rightWall, playerTop;

    GameObject otherPlayer;
    GameObject Trail;
    Rigidbody2D rb, rb2;
    Animator anim;

    private void Start()
    {
        otherPlayer = FindObjectOfType<Player1Controller>().gameObject;
        rb = GetComponent<Rigidbody2D>();
        rb2 = otherPlayer.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Trail = transform.GetChild(0).gameObject;
        Trail.SetActive(false);
        isGrounded = overriding = false;
        startCouroutine = true;
        moveDir = 0; // moveDir : 1 Right Dash -1 Left Dash -3 Left 3 Right 2 Up -2 Down 0 Stationary
        inputCounterL = inputCounterR = 0;
        originalJumpForce = jumpForce;
        dashDown = dashingBack = leftWall = rightWall = playerTop = false;
        downDashCount = 0;
        downDashTime = Time.time;
    }

    private void Update()
    {
        RayCasting();
        Move();
    }

    //void UpdateDash()
    //{
    //    DashHealth.fillAmount = Mathf.Clamp(1 - (((lastDash + dashDelay) - Time.time) / dashDelay), 0, 1);
    //}

    void RayCasting()
    {
        if (Physics2D.Raycast(new Vector2(transform.position.x - transform.localScale.x / 2 + 0.05f, transform.position.y - transform.localScale.y / 2 + 0.05f), Vector2.left, 0.1f, wall))
            leftWall = true;
        else leftWall = false;

        if (Physics2D.Raycast(new Vector2(transform.position.x + transform.localScale.x / 2 - 0.05f, transform.position.y - transform.localScale.y / 2 + 0.05f), Vector2.right, 0.1f, wall))
            rightWall = true;
        else rightWall = false;

        if (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + transform.localScale.y / 2 - 0.05f), Vector2.up, 2, player1) || Physics2D.Raycast(new Vector2(transform.position.x - transform.localScale.x / 2 + 0.05f, transform.position.y + transform.localScale.y / 2 - 0.05f), Vector2.up, 2, player1) || Physics2D.Raycast(new Vector2(transform.position.x + transform.localScale.x / 2 - 0.05f, transform.position.y + transform.localScale.y / 2 - 0.05f), Vector2.up, 2, player1))
        {
            playerTop = true;
        }
        else playerTop = false;

        if (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - transform.localScale.y / 2 + 0.05f), Vector2.down, 0.1f, player1) || Physics2D.Raycast(new Vector2(transform.position.x - transform.localScale.x / 2 + 0.05f, transform.position.y - transform.localScale.y / 2 + 0.05f), Vector2.down, 0.1f, player1) || Physics2D.Raycast(new Vector2(transform.position.x + transform.localScale.x / 2 - 0.05f, transform.position.y - transform.localScale.y / 2 + 0.05f), Vector2.down, 0.1f, player1))
        {
            isGrounded = true;
        }
    }

    void Move()
    {
        if (dashingBack)
            StartCoroutine("DashingFalse");

        if (!Input.GetKey(up) && !Input.GetKey(down) && !Input.GetKey(left) && !Input.GetKey(right) && moveDir != -2)
        {
            moveDir = 0;
            Trail.SetActive(false);
        }

        if (downDashCount == 0)
            jumpForce = originalJumpForce;

        if (Input.GetKeyDown(up) && isGrounded && !playerTop)
        {
            anim.SetBool("isJumping", true);
            anim.Play("JumpPlayer2", 0, 0);
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            moveDir = 2;
        }

        if (Input.GetKey(left) && !dashingBack && !leftWall)
        {
            rb.velocity = new Vector2(-moveSpeed * Time.deltaTime, rb.velocity.y);

            if (inputCounterL == 0)
            {
                inputCounterL = 1;
                inputCounterR = 0;
                timer = Time.time;
            }

            if (moveDir != -2 && moveDir != -1 && moveDir != 1)
                moveDir = -3;
        }

        if (Input.GetKey(right) && !dashingBack && !rightWall)
        {
            rb.velocity = new Vector2(moveSpeed * Time.deltaTime, rb.velocity.y);
            if (inputCounterR == 0)
            {
                inputCounterR = 1;
                inputCounterL = 0;
                timer = Time.time;
            }
            if (moveDir != -2 && moveDir != -1 && moveDir != 1)
                moveDir = 3;
        }

        if (Input.GetKeyUp(left) || Input.GetKeyUp(right) && (moveDir != -1 && moveDir != 1))
        {
            moveDir = 0;
            Trail.SetActive(false);
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (Input.GetKeyDown(left) && Time.time - timer < mostTimeForDash && Time.time - timer > leastTimeForDash && Time.time - lastDash > dashDelay && inputCounterL == 1 && !overriding)
        {
            Trail.SetActive(true);
            StartCoroutine("DashingFalse");
            rb.AddForce(new Vector2(moveSpeed * -dashMultiplier * Time.deltaTime, rb.velocity.y), ForceMode2D.Impulse);
            moveDir = -1;
            lastDash = Time.time;
        }

        if (Input.GetKeyDown(right) && Time.time - timer < mostTimeForDash && Time.time - timer > leastTimeForDash && Time.time - lastDash > dashDelay && inputCounterR == 1 && !overriding)
        {
            Trail.SetActive(true);
            StartCoroutine("DashingFalse");
            rb.AddForce(new Vector2(moveSpeed * dashMultiplier * Time.deltaTime, rb.velocity.y), ForceMode2D.Impulse);
            moveDir = 1;
            lastDash = Time.time;
        }

        if (Input.GetKey(down) && !overriding)
        {
            Trail.SetActive(true);
            rb.velocity = new Vector2(rb.velocity.x, -jumpForce * 2);
            if (!isGrounded)
            {
                moveDir = -2;
                dashDown = true;
            }
        }

        if ((Time.time - timer >= mostTimeForDash || moveDir == 1 || moveDir == -1) && !overriding)
        {
            inputCounterL = inputCounterR = 0;
        }

        if (moveDir == 0 || moveDir == 3 || moveDir == -3)
            overriding = false;

        if (overriding)
        {
            switch (moveDir)
            {
                case -2:
                    if (dashDown/* && Time.time - downDashTime > 1*/)
                    {
                        Trail.SetActive(true);
                        anim.SetBool("isJumping", true);
                        anim.Play("JumpPlayer2", 0, 0);
                        dashDown = false;
                        rb.velocity = new Vector2(rb.velocity.x, 0);
                        rb.AddForce(new Vector2(rb.velocity.x, jumpForce), ForceMode2D.Impulse);
                        downDashCount++;
                        if (downDashCount < numberOfDownDashOnPlayer1)
                            jumpForce += jumpForce * 0.5f;
                    }
                    break;
                case -1:
                    Trail.SetActive(true);
                    rb.velocity = new Vector2(0, rb.velocity.y);
                    rb.AddForce(new Vector2(moveSpeed / 1.5f, rb.velocity.y), ForceMode2D.Impulse);
                    dashingBack = true;
                    rb2.constraints = RigidbodyConstraints2D.FreezeAll;
                    break;
                case 1:
                    Trail.SetActive(true);
                    rb.velocity = new Vector2(0, rb.velocity.y);
                    rb.AddForce(new Vector2(-moveSpeed / 1.5f, rb.velocity.y), ForceMode2D.Impulse);
                    dashingBack = true;
                    rb2.constraints = RigidbodyConstraints2D.FreezeAll;
                    break;
            }

            if (startCouroutine)
                StartCoroutine("Delay");
        }

        anim.SetFloat("velocity", rb.velocity.x);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            anim.SetBool("isJumping", false);

            if (moveDir == -2)
            {
                moveDir = 0;
                Trail.SetActive(false);
            }
        }

        if (collision.gameObject.tag == "Break")
        {
            anim.SetBool("isJumping", false);

            if (moveDir == 1 || moveDir == -1)
            {
                Trail.SetActive(false);
                moveDir = 0;
                collision.gameObject.SetActive(false);
                StartCoroutine("DashingFalse");
            }
        }

        if (collision.gameObject.tag == "BreakF")
        {
            isGrounded = true;
            anim.SetBool("isJumping", false);

            if (moveDir == -2)
            {
                Trail.SetActive(false);
                moveDir = 0;
                collision.gameObject.SetActive(false);
                StartCoroutine("DashingFalse");
            }
        }

        if (collision.gameObject.tag == "Player")
        {
            if (moveDir == -1 || moveDir == 1 || moveDir == -2)
                overriding = true;

            if (moveDir == 0/* && downDashCount > numberOfDownDashOnPlayer1 - 1*/)
            {
                jumpForce = originalJumpForce;
                downDashCount = 0;
                downDashTime = Time.time;
            }
        }
        else/* if (moveDir != -2 && collision.gameObject.tag != "Player")*/
        {
            jumpForce = originalJumpForce;
            downDashCount = 0;
            dashDown = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "BreakF")
            isGrounded = true;

        if (collision.gameObject.tag == "Player")
        {
            if (moveDir == -1 || moveDir == 1 || moveDir == -2)
                overriding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    IEnumerator Delay()
    {
        startCouroutine = false;
        yield return new WaitForSeconds(0.1f);
        Trail.SetActive(false);
        yield return new WaitForSeconds(0.4f);
        overriding = false;
        startCouroutine = true;
        moveDir = 0;
        rb2.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    IEnumerator DashingFalse()
    {
        yield return new WaitForSeconds(0.2f);
        Trail.SetActive(false);
        dashingBack = false;
    }
}