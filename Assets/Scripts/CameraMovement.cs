using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject player;
    private Vector3 cameraOffset = new Vector3(0, 0, -10);
    private float smoothSpeed = 0.05f;
    //float positions for each edge of the map
    private float northEdge = 100;
    private float eastEdge = 50;
    private float southEdge = -100;
    private float westEdge = -50;

    private float cameraXOffset;
    private float cameraYOffset;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");


        //Gets offset of camera from center of screen
        Vector3 offset = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 1));
        cameraXOffset = offset.x;
        cameraYOffset = offset.y;
    }

    void FixedUpdate()
    {

        Vector3 desiredPosition = player.transform.position + cameraOffset;
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, westEdge + cameraXOffset, eastEdge - cameraXOffset);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, southEdge + cameraYOffset, northEdge - cameraYOffset);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

    }
}
