using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDevil : MonoBehaviour
{
    public GameObject enableThis;
    public float offset;
    GameObject P1, P2;
    Vector3 moveHere;
    DevilMayCome dm;
    CameraZoomOut cz;

    int trigg;

    private void Start()
    {
        dm = enableThis.GetComponent<DevilMayCome>();
        cz = Camera.main.GetComponent<CameraZoomOut>();
        enableThis.SetActive(false);
        trigg = 0;
        P1 = GameObject.FindGameObjectWithTag("Player");
        P2 = GameObject.FindGameObjectWithTag("Player2");
    }

    private void Update()
    {
        if (trigg == 1 && Camera.main.transform.position.x > enableThis.transform.position.x + offset)
        {
            dm.enabled = false;
            Camera.main.transform.position -= new Vector3(Time.deltaTime * 5, 0, 0);
        }
        else if (trigg == 1 && Camera.main.transform.position.x <= enableThis.transform.position.x + offset)
        {
            trigg = 2;
            dm.enabled = true;
        }

        if (trigg == 2)
            moveHere = new Vector3((P1.transform.position.x + P2.transform.position.x) / 2, (P1.transform.position.y + P2.transform.position.y) / 2, 0);

        if (trigg == 2 && Camera.main.transform.position.x < moveHere.x)
        {
            Camera.main.transform.position += new Vector3(Time.deltaTime * 10, 0, 0);
        }
        else if (trigg == 2 && Camera.main.transform.position.x >= moveHere.x)
        {
            trigg = 3;
            cz.enabled = true;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
        {
            enableThis.SetActive(true);
            trigg = 1;
            cz.enabled = false;
        }
    }
}
