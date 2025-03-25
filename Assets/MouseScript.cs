using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour
{
    public GameObject targetObject;  // Le GameObject dont tu veux que la souris suive la position

    void Start()
    {
        // Vérifie si un GameObject est assigné
        if (targetObject == null)
        {
            Debug.LogWarning("Aucun GameObject assigné pour suivre !");
        }
        else
        {
            // Cache le curseur et verrouille-le
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void Update()
    {
        if (targetObject != null)
        {
            // Convertir la position du GameObject du monde en coordonnées de l'écran
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetObject.transform.position);

            // Déplacer la souris à la position du GameObject (sur l'écran)
            Vector2 cursorPos = new Vector2(screenPosition.x, screenPosition.y);
            Cursor.SetCursor(null, cursorPos, CursorMode.Auto);  // Cette ligne sert à déplacer la souris (non fonctionnelle)
        }
    }
}