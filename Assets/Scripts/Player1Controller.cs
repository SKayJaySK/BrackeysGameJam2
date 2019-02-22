using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    public KeyCode up = KeyCode.W;
    public KeyCode down = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;

    public float jumpForce = 300f;
    public float moveSpeed = 100f;

    public LayerMask player2, wall;

    int jumpCounter;
    bool isGrounded, sidePlayer, leftWall, rightWall, playerTop;

    Rigidbody2D rb;
    Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isGrounded = sidePlayer = leftWall = rightWall = playerTop = false;
        jumpCounter = 0;
    }

    private void Update()
    {
        RayCasting();
        Move();
    }

    void RayCasting()
    {
        if (Physics2D.Raycast(new Vector2(transform.position.x - transform.localScale.x / 2 + 0.05f, transform.position.y - transform.localScale.y / 2 + 0.05f), Vector2.left, 0.3f, player2) || Physics2D.Raycast(new Vector2(transform.position.x + transform.localScale.x / 2 - 0.05f, transform.position.y - transform.localScale.y / 2 + 0.05f), Vector3.right, 0.3f, player2))
        {
            sidePlayer = true;
        }
        else sidePlayer = false;

        if (Physics2D.Raycast(new Vector2(transform.position.x - transform.localScale.x / 2 + 0.05f, transform.position.y - transform.localScale.y / 2 + 0.05f), Vector2.left, 0.3f, wall))
            leftWall = true;
        else leftWall = false;

        if (Physics2D.Raycast(new Vector2(transform.position.x + transform.localScale.x / 2 - 0.05f, transform.position.y - transform.localScale.y / 2 + 0.05f), Vector2.right, 0.3f, wall))
            rightWall = true;
        else rightWall = false;

        if (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + transform.localScale.y / 2 - 0.05f), Vector2.up, 3, player2) || Physics2D.Raycast(new Vector2(transform.position.x - transform.localScale.x / 2 + 0.05f, transform.position.y + transform.localScale.y / 2 - 0.05f), Vector2.up, 3, player2) || Physics2D.Raycast(new Vector2(transform.position.x + transform.localScale.x / 2 - 0.05f, transform.position.y + transform.localScale.y / 2 - 0.05f), Vector2.up, 3, player2))
        {
            playerTop = true;
        }
        else playerTop = false;

        if (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - transform.localScale.y / 2 + 0.05f), Vector2.down, 0.1f, player2) || Physics2D.Raycast(new Vector2(transform.position.x - transform.localScale.x / 2 + 0.05f, transform.position.y - transform.localScale.y / 2 + 0.05f), Vector2.down, 0.1f, player2) || Physics2D.Raycast(new Vector2(transform.position.x + transform.localScale.x / 2 - 0.05f, transform.position.y - transform.localScale.y / 2 + 0.05f), Vector2.down, 0.1f, player2))
        {
            isGrounded = true;
        }
    }

    void Move()
    {

        if (!Input.GetKey(up) && !Input.GetKey(down) && !Input.GetKey(left) && !Input.GetKey(right) && sidePlayer)
        {
            rb.velocity = Vector2.zero;
        }

        if (Input.GetKeyDown(up) && jumpCounter < 1 && !sidePlayer && !playerTop)
        {
            anim.SetBool("isJumping", true);
            anim.Play("JumpPlayer1", 0, 0);
            jumpCounter++;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        if (Input.GetKey(left) && !leftWall)
        {
            rb.velocity = new Vector2(-moveSpeed * Time.deltaTime, rb.velocity.y);
        }
        if (Input.GetKey(right) && !rightWall)
        {
            rb.velocity = new Vector2(moveSpeed * Time.deltaTime, rb.velocity.y);
        }

        if (Input.GetKeyUp(left) || Input.GetKeyUp(right))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        anim.SetFloat("velocity", rb.velocity.x);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Break")
        {
            isGrounded = true;
            anim.SetBool("isJumping", false);
            jumpCounter = 0;
        }

        if (collision.gameObject.tag == "Player2")
            jumpCounter = 0;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Break")
        {
            isGrounded = true;
            jumpCounter = 0;
        }

        if (collision.gameObject.tag == "Player2")
            jumpCounter = 0;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}