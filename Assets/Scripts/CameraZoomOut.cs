using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomOut : MonoBehaviour
{
    public GameObject player1, player2;

    public float limit;
    public float cameraZoomSpeed;

    float originalOrth;

    float lastDiff, lastDiff2;

    private void Start()
    {
        originalOrth = Camera.main.orthographicSize;
    }

    private void Update()
    {
        if ((Mathf.Abs(player1.transform.position.x - player2.transform.position.x) > limit) || (Mathf.Abs(player1.transform.position.y - player2.transform.position.y) > limit) && Camera.main.orthographicSize != Mathf.Abs(player1.transform.position.x - player2.transform.position.x))
        {
            if (lastDiff > Mathf.Abs(player1.transform.position.x - player2.transform.position.x) && Camera.main.orthographicSize > originalOrth)
            {
                Camera.main.orthographicSize -= cameraZoomSpeed * Time.deltaTime;
                transform.position -= new Vector3(0, cameraZoomSpeed * Time.deltaTime, 0);
            }
            else if (lastDiff < Mathf.Abs(player1.transform.position.x - player2.transform.position.x) || Camera.main.orthographicSize <= (Mathf.Abs(player1.transform.position.x - player2.transform.position.x) + player1.transform.localScale.y * 1.5f) / 2)
            {
                Camera.main.orthographicSize += cameraZoomSpeed * Time.deltaTime;
                transform.position += new Vector3(0, cameraZoomSpeed * Time.deltaTime, 0);
            }

            if (lastDiff2 > Mathf.Abs(player1.transform.position.y - player2.transform.position.y) && Camera.main.orthographicSize > originalOrth)
            {
                Camera.main.orthographicSize -= cameraZoomSpeed * Time.deltaTime;
                transform.position -= new Vector3(0, cameraZoomSpeed * Time.deltaTime, 0);
            }
            else if (lastDiff2 < Mathf.Abs(player1.transform.position.y - player2.transform.position.y) || Camera.main.orthographicSize <= (Mathf.Abs(player1.transform.position.y - player2.transform.position.y) + player1.transform.localScale.y * 1.5f) / 2)
            {
                Camera.main.orthographicSize += cameraZoomSpeed * Time.deltaTime;
                transform.position += new Vector3(0, cameraZoomSpeed * 1.5f * Time.deltaTime, 0);
            }
        }
        lastDiff = Mathf.Abs(player1.transform.position.x - player2.transform.position.x);
        lastDiff2 = Mathf.Abs(player1.transform.position.y - player2.transform.position.y);

        transform.position = new Vector3((player1.transform.position.x + player2.transform.position.x) / 2, (player1.transform.position.y + player2.transform.position.y) / 2, transform.position.z);
    }
}