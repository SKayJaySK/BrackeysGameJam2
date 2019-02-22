using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public GameObject toFollow;

    public float horizontalOffset, verticalOffset;

    private void Update()
    {
        transform.position = new Vector3(toFollow.transform.position.x + horizontalOffset, toFollow.transform.position.y + verticalOffset, -10);
    }
}
