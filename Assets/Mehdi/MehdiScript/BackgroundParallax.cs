using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    private float startPos;
    private float camStartPos; // On stocke aussi la position initiale de la caméra
    public GameObject cam;
    public float parallax;

    void Start()
    {
        startPos = transform.position.x; // Position initiale de l’objet
        camStartPos = cam.transform.position.x; // Position initiale de la caméra
    }

    void Update()
    {
        float distance = (cam.transform.position.x - camStartPos) * parallax; // Décalage par rapport à la position initiale de la caméra

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
    }
}