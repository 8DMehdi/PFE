using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TarodevController;

public class Grapling : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer _lineRenderer;
    // public DistanceJoint2D _distanceJoint;
    [SerializeField] private DistanceJoint2D _distanceJoint;

    public bool isTouchingTriggerZone = false;

    [SerializeField] private ScriptableStats _stats;

    // Start is called before the first frame update
    void Start()
    {
        _distanceJoint.enabled = false;

        if (_stats == null)
    {
        Debug.LogError("ScriptableStats (_stats) is not assigned in the Inspector!");
        return; // Sortir de la fonction si _stats est null
    }
    }

    // Update is called once per frame
    void Update()
    {   

        // Crée un rayon qui part de la position de la souris dans la direction de la caméra
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        // Vérifie si le raycast touche un objet avec un collider (en mode Trigger)
        if (hit.collider != null && hit.collider.CompareTag("GrapZone"))
        {
            // Si l'objet touché est une zone de Trigger, on met le booléen à true
            isTouchingTriggerZone = true;
        }
        else
        {
            // Sinon, on le met à false
            isTouchingTriggerZone = false;
        }

        // Affichage pour tester
        Debug.Log("Is touching trigger: " + isTouchingTriggerZone);


        if (Input.GetKeyDown(KeyCode.Mouse0) && isTouchingTriggerZone)
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _stats.MaxFallSpeed = -0;
            _stats.MaxSpeed = 33;
            Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _lineRenderer.SetPosition(0, mousePos);
            _lineRenderer.SetPosition(1, transform.position);
            _distanceJoint.connectedAnchor = mousePos;
            _distanceJoint.enabled = true;
            _lineRenderer.enabled = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _distanceJoint.enabled = false;
            _lineRenderer.enabled = false;
            //_stats.MaxSpeed = 14;
            StartCoroutine(ChangeIntAfterDelay());
        }
        if (_distanceJoint.enabled)
        {
            _lineRenderer.SetPosition(1, transform.position);
        }
    }

    public IEnumerator ChangeIntAfterDelay()
    {
        // Attente de 1 seconde
        
        yield return new WaitForSeconds(0f);
        
        // Changer la valeur de myInt
        _stats.MaxFallSpeed = 40;
        _stats.MaxSpeed = 14;
        //Debug.LogError("ca marche");
    }
}