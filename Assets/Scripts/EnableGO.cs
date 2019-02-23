using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGO : MonoBehaviour
{
    public GameObject nextGO;

    private void Start()
    {
        nextGO.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player2")
        {
            nextGO.SetActive(true);
        }
    }
}
