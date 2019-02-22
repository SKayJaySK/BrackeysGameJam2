using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCameraMotion : MonoBehaviour
{
    public GameObject P1, P2;

    bool inside;
    Vector3 unitVec;
    CameraZoomOut cm;
    CamFollow cf;

    private void Start()
    {
        inside = false;
        cm = Camera.main.gameObject.GetComponent<CameraZoomOut>();
        cf = Camera.main.gameObject.GetComponent<CamFollow>();
        cm.enabled = false;
    }

    private void Update()
    {
        if (inside)
        {
            cf.enabled = false;
            cm.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            inside = true;
    }
}
