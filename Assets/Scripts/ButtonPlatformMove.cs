using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlatformMove : MonoBehaviour
{
    public float moveTill;
    float moveTill2, myX;
    public float moveSpeed = 3f;
    public bool moveRight, buttonUp, moveDown, dontStop;
    public GameObject moveThisObject;

    bool openIt, openItPerma;

    private void Start()
    {
        openIt = false;
        openItPerma = false;
        if (!moveDown)
            moveTill2 = moveThisObject.transform.position.x;
        else
            moveTill2 = moveThisObject.transform.position.y;
    }

    private void Update()
    {
        if (!dontStop)
        {
            if (!moveDown)
            {
                if (moveRight && openIt)
                {
                    if (moveThisObject.transform.position.x < moveTill2 - moveTill)
                        moveThisObject.transform.position += new Vector3(Time.deltaTime * moveSpeed, 0, 0);
                }
                else if (!moveRight && openIt)
                {
                    if (moveThisObject.transform.position.x > moveTill2 - moveTill)
                        moveThisObject.transform.position -= new Vector3(Time.deltaTime * moveSpeed, 0, 0);
                }

                if (moveRight && !openIt)
                {
                    if (moveThisObject.transform.position.x > moveTill2)
                        moveThisObject.transform.position -= new Vector3(Time.deltaTime * moveSpeed, 0, 0);
                }
                else if (!moveRight && !openIt)
                {
                    if (moveThisObject.transform.position.x < moveTill2)
                        moveThisObject.transform.position += new Vector3(Time.deltaTime * moveSpeed, 0, 0);
                }
            }
            else
            {
                if (moveRight && openIt)
                {
                    if (moveThisObject.transform.position.y < moveTill2 - moveTill)
                        moveThisObject.transform.position += new Vector3(0, Time.deltaTime * moveSpeed, 0);
                }
                else if (!moveRight && openIt)
                {
                    if (moveThisObject.transform.position.y > moveTill2 - moveTill)
                        moveThisObject.transform.position -= new Vector3(0, Time.deltaTime * moveSpeed, 0);
                }

                if (moveRight && !openIt)
                {
                    if (moveThisObject.transform.position.y > moveTill2)
                        moveThisObject.transform.position -= new Vector3(0, Time.deltaTime * moveSpeed, 0);
                }
                else if (!moveRight && !openIt)
                {
                    if (moveThisObject.transform.position.y < moveTill2)
                        moveThisObject.transform.position += new Vector3(0, Time.deltaTime * moveSpeed, 0);
                }
            }
        }
        else
        {
            if (!moveDown)
            {
                if (moveRight && openItPerma)
                {
                    if (moveThisObject.transform.position.x < moveTill2 - moveTill)
                        moveThisObject.transform.position += new Vector3(Time.deltaTime * moveSpeed, 0, 0);
                }
                else if (!moveRight && openItPerma)
                {
                    if (moveThisObject.transform.position.x > moveTill2 - moveTill)
                        moveThisObject.transform.position -= new Vector3(Time.deltaTime * moveSpeed, 0, 0);
                }

                if (moveRight && !openItPerma)
                {
                    if (moveThisObject.transform.position.x > moveTill2)
                        moveThisObject.transform.position -= new Vector3(Time.deltaTime * moveSpeed, 0, 0);
                }
                else if (!moveRight && !openItPerma)
                {
                    if (moveThisObject.transform.position.x < moveTill2)
                        moveThisObject.transform.position += new Vector3(Time.deltaTime * moveSpeed, 0, 0);
                }
            }
            else
            {
                if (moveRight && openItPerma)
                {
                    if (moveThisObject.transform.position.y < moveTill2 - moveTill)
                        moveThisObject.transform.position += new Vector3(0, Time.deltaTime * moveSpeed, 0);
                }
                else if (!moveRight && openItPerma)
                {
                    if (moveThisObject.transform.position.y > moveTill2 - moveTill)
                        moveThisObject.transform.position -= new Vector3(0, Time.deltaTime * moveSpeed, 0);
                }

                if (moveRight && !openItPerma)
                {
                    if (moveThisObject.transform.position.y > moveTill2)
                        moveThisObject.transform.position -= new Vector3(0, Time.deltaTime * moveSpeed, 0);
                }
                else if (!moveRight && !openItPerma)
                {
                    if (moveThisObject.transform.position.y < moveTill2)
                        moveThisObject.transform.position += new Vector3(0, Time.deltaTime * moveSpeed, 0);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player2" || collision.gameObject.tag == "Player")
        {
            openIt = true;
            openItPerma = true;
            SoundManaging.instance.as1.PlayOneShot(SoundManaging.instance.sounds[3]);
            if (!buttonUp)
                transform.position -= new Vector3(0, 0.1f, 0);
            else
                transform.position += new Vector3(0, 0.1f, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player2" || collision.gameObject.tag == "Player")
        {
            openIt = false;
            if (!buttonUp)
                transform.position += new Vector3(0, 0.1f, 0);
            else
                transform.position -= new Vector3(0, 0.1f, 0);
        }
    }
}
