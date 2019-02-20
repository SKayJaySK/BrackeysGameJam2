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
    public int jumpCount = 2;

    [Space]
    public Image DashHealth;

    [Space]
    public LayerMask player1, wall;
    
    int moveDir, inputCounterL, inputCounterR, downDashCount;
    float timer, originalJumpForce, lastDash, downDashTime;
    bool isGrounded, overriding, startCouroutine, dashDown, sidePlayer, dashingBack, leftWall, rightWall, playerTop;

    GameObject otherPlayer;
    Rigidbody2D rb, rb2;

    private void Start()
    {
        otherPlayer = FindObjectOfType<Player1Controller>().gameObject;
        rb = GetComponent<Rigidbody2D>();
        rb2 = otherPlayer.GetComponent<Rigidbody2D>();
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
        UpdateDash();
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

        if (Physics2D.Raycast(new Vector2(transform.position.x - transform.localScale.x / 2 + 0.05f, transform.position.y - transform.localScale.y / 2 + 0.05f), Vector2.left, 0.1f, wall))
            leftWall = true;
        else leftWall = false;

        if (Physics2D.Raycast(new Vector2(transform.position.x + transform.localScale.x / 2 - 0.05f, transform.position.y - transform.localScale.y / 2 + 0.05f), Vector2.right, 0.1f, wall))
            rightWall = true;
        else rightWall = false;

        if (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + transform.localScale.y / 2 - 0.05f), Vector2.up, 1, player1) || Physics2D.Raycast(new Vector2(transform.position.x - transform.localScale.x / 2 + 0.05f, transform.position.y + transform.localScale.y / 2 - 0.05f), Vector2.up, 1, player1) || Physics2D.Raycast(new Vector2(transform.position.x + transform.localScale.x / 2 - 0.05f, transform.position.y + transform.localScale.y / 2 - 0.05f), Vector2.up, 1, player1))
        {
            playerTop = true;
            isGrounded = false;
        }
        else playerTop = false;
    }

    void Move()
    {
        if (dashingBack)
            StartCoroutine("DashingFalse");

        if (!Input.GetKey(up) && !Input.GetKey(down) && !Input.GetKey(left) && !Input.GetKey(right) && moveDir != -2)
        {
            moveDir = 0;
            if (sidePlayer)
                rb.velocity = Vector2.zero;
        }

        if (downDashCount == 0)
            jumpForce = originalJumpForce;

        if (Input.GetKeyDown(up) && !sidePlayer && isGrounded && !playerTop)
        {
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
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (Input.GetKeyDown(left) && Time.time - timer < mostTimeForDash && Time.time - timer > leastTimeForDash && Time.time - lastDash > dashDelay && inputCounterL == 1 && !overriding && !sidePlayer)
        {
            rb.AddForce(new Vector2(moveSpeed * -dashMultiplier * Time.deltaTime, rb.velocity.y), ForceMode2D.Impulse);
            moveDir = -1;
            lastDash = Time.time;
        }

        if (Input.GetKeyDown(right) && Time.time - timer < mostTimeForDash && Time.time - timer > leastTimeForDash && Time.time - lastDash > dashDelay && inputCounterR == 1 && !overriding && !sidePlayer)
        {
            rb.AddForce(new Vector2(moveSpeed * dashMultiplier * Time.deltaTime, rb.velocity.y), ForceMode2D.Impulse);
            moveDir = 1;
            lastDash = Time.time;
        }

        if (Input.GetKey(down) && !overriding)
        {
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
                    if (dashDown && Time.time - downDashTime > 1)
                    {
                        if (downDashCount < numberOfDownDashOnPlayer1)
                            jumpForce += jumpForce * 0.3f;
                        dashDown = false;
                        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                        downDashCount++;
                    }
                    break;
                case -1:
                    rb.velocity = new Vector2(moveSpeed / 2, rb.velocity.y);
                    dashingBack = true;
                    rb2.constraints = RigidbodyConstraints2D.FreezeAll;
                    break;
                case 1:
                    rb.velocity = new Vector2(-moveSpeed / 2, rb.velocity.y);
                    dashingBack = true;
                    rb2.constraints = RigidbodyConstraints2D.FreezeAll;
                    break;
            }

            if (startCouroutine)
                StartCoroutine("Delay");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounded = true;

        if (collision.gameObject.tag == "Player")
        {
            isGrounded = true;

            if (moveDir == -1 || moveDir == 1 || moveDir == -2)
                overriding = true;

            if (moveDir == -2 && downDashCount > numberOfDownDashOnPlayer1 - 1)
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
        if (collision.gameObject.tag == "Ground")
            isGrounded = true;

        if (collision.gameObject.tag == "Player")
        {
            isGrounded = true;

            if (moveDir == -1 || moveDir == 1 || moveDir == -2)
                overriding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounded = false;

        if (collision.gameObject.tag == "Player")
        {
            isGrounded = false;
        }
    }

    IEnumerator Delay()
    {
        startCouroutine = false;
        yield return new WaitForSeconds(0.5f);
        overriding = false;
        startCouroutine = true;
        moveDir = 0;
        rb2.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    IEnumerator DashingFalse()
    {
        yield return new WaitForSeconds(0.2f);
        dashingBack = false;
    }
}