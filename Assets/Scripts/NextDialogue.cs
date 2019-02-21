using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextDialogue : MonoBehaviour
{
    public bool ShowSpecificText;
    public int dialogueNumber;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player2")
        {
            if (!ShowSpecificText)
            {
                collision.GetComponent<PlayerText>().ShowText();
                gameObject.SetActive(false);
            }
            else
            {
                collision.GetComponent<PlayerText>().ShowTextExact(dialogueNumber);
                gameObject.SetActive(false);
            }
        }
    }
}