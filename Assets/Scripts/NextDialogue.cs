using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextDialogue : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player2")
        {
            collision.GetComponent<PlayerText>().ShowText();
            gameObject.SetActive(false);
        }
    }
}