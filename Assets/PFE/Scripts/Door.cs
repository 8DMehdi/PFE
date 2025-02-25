using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isUnlocked = false; // La porte est verrouillée par défaut
    public GameObject doorObject; // L'objet de la porte que l'on souhaite ouvrir

    public void UnlockDoor()
    {
        if (!isUnlocked)
        {
            isUnlocked = true;
            OpenDoor();  // Ouvre la porte
        }
    }

    private void OpenDoor()
    {
        
        doorObject.transform.position += new Vector3(0, 5, 0); // Déplace la porte vers le haut

        Debug.Log("Porte ouverte !");
    }
}

