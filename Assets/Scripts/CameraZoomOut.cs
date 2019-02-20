using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomOut : MonoBehaviour
{
    public GameObject player1, player2;

    public float limit;
    public float cameraZoomSpeed;

    float lastDiff;

    private void Update()
    {
        if (Mathf.Abs(player1.transform.position.x - player2.transform.position.x) > limit && Camera.main.orthographicSize != Mathf.Abs(player1.transform.position.x - player2.transform.position.x))
        {
            if (lastDiff > Mathf.Abs(player1.transform.position.x - player2.transform.position.x))
            {
                Camera.main.orthographicSize -= cameraZoomSpeed * Time.deltaTime;
                transform.position -= new Vector3(0, cameraZoomSpeed * Time.deltaTime, 0);
            }
            else if (lastDiff < Mathf.Abs(player1.transform.position.x - player2.transform.position.x))
            {
                Camera.main.orthographicSize += cameraZoomSpeed * Time.deltaTime;
                transform.position += new Vector3(0, cameraZoomSpeed * Time.deltaTime, 0);
            }
        }
        lastDiff = Mathf.Abs(player1.transform.position.x - player2.transform.position.x);

        transform.position = new Vector3((player1.transform.position.x + player2.transform.position.x) / 2, transform.position.y, transform.position.z);
    }
}