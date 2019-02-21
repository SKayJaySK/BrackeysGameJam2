using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextDialogue : MonoBehaviour
{
    public bool ShowSpecificText, thisEnablesNext;
    public int dialogueNumber, player;

    public GameObject nextDialogue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player == 1 && collision.tag == "Player")
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

            if (thisEnablesNext)
                nextDialogue.SetActive(true);
        }

        if (player == 2 && collision.tag == "Player2")
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

            if (thisEnablesNext)
                nextDialogue.SetActive(true);
        }
    }
}