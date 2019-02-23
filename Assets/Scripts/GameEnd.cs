using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindObjectOfType<DevilMayCome>().spare = true;
            Camera.main.GetComponent<CameraZoomOut>().enabled = false;
        }
    }
}
