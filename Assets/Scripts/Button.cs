using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Sprite open, close;
    public GameObject door;

    public bool up;

    BoxCollider2D doorCol;

    public bool pressureDoor;

    float offset = 0.1f;
    bool inside;

    SpriteRenderer sp;

    private void Start()
    {
        inside = false;
        doorCol = door.GetComponent<BoxCollider2D>();

        if (pressureDoor)
        {
            sp = door.GetComponent<SpriteRenderer>();
            sp.sprite = close;
        }
    }

    private void Update()
    {
        if (inside && pressureDoor && sp.sprite != open)
        {
            sp.sprite = open;
            SoundManaging.instance.as1.PlayOneShot(SoundManaging.instance.sounds[5]);
            doorCol.enabled = false;
        }
        else if (!inside && pressureDoor && sp.sprite != close)
        {
            sp.sprite = close;
            StartCoroutine("Delay");
        }
        else if (inside && !pressureDoor && door.activeInHierarchy)
            door.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player2" || collision.gameObject.tag == "Player")
        {
            if (!up)
                transform.position -= new Vector3(0, offset, 0);
            else
                transform.position += new Vector3(0, offset, 0);
            inside = true;
            SoundManaging.instance.as1.PlayOneShot(SoundManaging.instance.sounds[3]);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player2" || collision.gameObject.tag == "Player")
        {
            if (!up)
                transform.position += new Vector3(0, offset, 0);
            else
                transform.position -= new Vector3(0, offset, 0);
            inside = false;
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.1f);
        doorCol.enabled = true;
    }
}
