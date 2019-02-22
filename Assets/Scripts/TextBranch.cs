using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBranch : MonoBehaviour
{
    PlayerText pt;
    bool workOnce;

    private void Start()
    {
        workOnce = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (workOnce)
            {
                workOnce = false;
                StartCoroutine("InvokeFn");
                pt = FindObjectOfType<PlayerText>();
            }
        }
    }

    IEnumerator InvokeFn()
    {
        yield return new WaitForSeconds(0.5f);
        pt.ShowTextExact(2);
        yield return new WaitForSeconds(2);
        pt.ShowTextExact(3);
        yield return new WaitForSeconds(2);
        pt.ShowTextExact(4);
        Destroy(this);
    }
}
