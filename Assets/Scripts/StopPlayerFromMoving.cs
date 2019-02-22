using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPlayerFromMoving : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player2")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            Destroy(gameObject);
        }
    }
}
