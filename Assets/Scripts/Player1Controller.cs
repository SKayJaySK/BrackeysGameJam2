using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    public KeyCode up = KeyCode.W;
    public KeyCode down = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;

    public float jumpForce = 300f;
    public float moveSpeed = 100f;

    public LayerMask player2;

    int turn, jumpCounter;
    bool isGrounded, sidePlayer, topPlayer;

    GameObject otherPlayer;
    Rigidbody2D rb;

    private void Start()
    {
        otherPlayer = FindObjectOfType<Player2Controller>().gameObject;
        rb = GetComponent<Rigidbody2D>();
        isGrounded = sidePlayer = topPlayer = false;
        jumpCounter = 0;
    }

    private void Update()
    {
        TurnUpdate();
        RayCasting();
        Move();
    }

    void TurnUpdate()
    {
        if (transform.position.y > otherPlayer.transform.position.y)
            turn = 1;
        else turn = 2;
    }

    void RayCasting()
    {
        if (Physics2D.Raycast(new Vector2(transform.position.x - transform.localScale.x / 2 + 0.05f, transform.position.y - transform.localScale.y / 2 + 0.05f), Vector2.left, 0.1f, player2) || Physics2D.Raycast(new Vector2(transform.position.x + transform.localScale.x / 2 - 0.05f, transform.position.y - transform.localScale.y / 2 + 0.05f), Vector3.right, 0.1f, player2))
        {
            sidePlayer = true;
        }
        else sidePlayer = false;
    }

    void Move()
    {
        if (!Input.GetKey(up) && !Input.GetKey(down) && !Input.GetKey(left) && !Input.GetKey(right) && sidePlayer)
        {
            rb.velocity = Vector2.zero;
        }

        if (Input.GetKeyDown(up) && jumpCounter < 1 && !sidePlayer)
        {
            jumpCounter++;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, jumpForce * Time.deltaTime), ForceMode2D.Impulse);
        }

        if (Input.GetKey(left))
        {
            rb.velocity = new Vector2(-moveSpeed * Time.deltaTime, rb.velocity.y);
        }
        if (Input.GetKey(right))
        {
            rb.velocity = new Vector2(moveSpeed * Time.deltaTime, rb.velocity.y);
        }

        if (Input.GetKeyUp(left) || Input.GetKeyUp(right))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (topPlayer && turn != 1)
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        else rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
        jumpCounter = 0;

        if (collision.gameObject.tag == "Player2" && !sidePlayer)
            topPlayer = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;
        jumpCounter = 0;

        if (collision.gameObject.tag == "Player2" && !sidePlayer)
            topPlayer = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;

        if (collision.gameObject.tag == "Player2")
            topPlayer = false;
    }
}