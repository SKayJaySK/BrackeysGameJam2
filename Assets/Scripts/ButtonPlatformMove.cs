using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlatformMove : MonoBehaviour
{
    public float moveTill;
    public bool moveRight;
    public GameObject moveThisObject;

    bool openIt;

    private void Start()
    {
        openIt = false;
    }

    private void Update()
    {
        if(moveRight && openIt)
        {
            if (moveThisObject.transform.position.x < moveTill)
                moveThisObject.transform.position += new Vector3(Time.deltaTime, 0, 0);
        }
        else if(!moveRight && openIt)
        {
            if (moveThisObject.transform.position.x > moveTill)
                moveThisObject.transform.position -= new Vector3(Time.deltaTime, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player2")
        {
            openIt = true;
        }
    }
}
