using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCameraMotion : MonoBehaviour
{
    public GameObject P1, P2, Sp1, Sp2, Sp3;

    bool inside;
    Vector3 unitVec;
    CameraZoomOut cm;
    CamFollow cf;

    private void Start()
    {
        inside = false;
        cm = Camera.main.gameObject.GetComponent<CameraZoomOut>();
        cf = Camera.main.gameObject.GetComponent<CamFollow>();
        Sp1.SetActive(false);
        Sp2.SetActive(false);
        Sp3.SetActive(false);
        cm.enabled = false;
    }

    private void Update()
    {
        if (inside)
        {
            cf.enabled = false;
            cm.enabled = true;
            Sp1.SetActive(true);
            Sp2.SetActive(true);
            Sp3.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            inside = true;
    }
}
