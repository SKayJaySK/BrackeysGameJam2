using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlatformMove : MonoBehaviour
{
    public float moveTill;
    float moveTill2;
    public bool moveRight, up;
    public GameObject moveThisObject;

    bool openIt;

    private void Start()
    {
        openIt = false;
        moveTill2 = moveThisObject.transform.position.x;
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

        if (moveRight && !openIt)
        {
            if (moveThisObject.transform.position.x > moveTill2)
                moveThisObject.transform.position -= new Vector3(Time.deltaTime, 0, 0);
        }
        else if (!moveRight && !openIt)
        {
            if (moveThisObject.transform.position.x < moveTill2)
                moveThisObject.transform.position += new Vector3(Time.deltaTime, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player2")
        {
            openIt = true;
            if (!up)
                transform.position -= new Vector3(0, 0.1f, 0);
            else
                transform.position += new Vector3(0, 0.1f, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player2")
        {
            openIt = false;
            if (!up)
                transform.position += new Vector3(0, 0.1f, 0);
            else
                transform.position -= new Vector3(0, 0.1f, 0);
        }
    }
}
