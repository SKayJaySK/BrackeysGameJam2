using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDevil2 : MonoBehaviour
{
    public GameObject Devil;

    private void Start()
    {
        Devil.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player2")
        {
            Devil.SetActive(true);
            Destroy(gameObject);
        }
    }
}
