using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    Camera cam;
    public Rigidbody2D playerRb; // Assigne directement l'objet dans l'Inspector

    bool zoomIn;

    [Range(2, 10)]
    public float zoomSize;

    [Range(0.01f, 0.1f)]
    public float zoomSpeed;

    [Range(1, 3)]
    public float waitTime;

    float waitCounter;

    private void Awake()
    {
        cam = Camera.main;
    }

    void ZoomIn()
    {
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomSize, zoomSpeed);
    }

    void ZoomOut()
    {
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 10, zoomSpeed);
    }

    private void LateUpdate()
    {
        if (playerRb == null) return; // Vérifie si le joueur est bien assigné

        if (Mathf.Abs(playerRb.velocity.magnitude) < 8) // Zoom in
        {
            waitCounter += Time.deltaTime;
            if (waitCounter > waitTime)
            {
                zoomIn = true;
            }
        }
        else // Zoom out
        {
            zoomIn = false;
            waitCounter = 0;
        }

        if (zoomIn)
        {
            ZoomIn();
        }
        else
        {
            ZoomOut();
        }
    }
}
