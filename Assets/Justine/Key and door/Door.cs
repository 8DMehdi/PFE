// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Door : MonoBehaviour
// {
//     private bool isUnlocked = false; // La porte est verrouillée par défaut
//     public GameObject doorObject; // L'objet de la porte que l'on souhaite ouvrir

//     public void UnlockDoor()
//     {
//         if (!isUnlocked)
//         {
//             isUnlocked = true;
//             OpenDoor();  // Ouvre la porte
//         }
//     }

//     private void OpenDoor()
//     {
        
//         doorObject.transform.position += new Vector3(0, 5, 0); // Déplace la porte vers le haut

//         Debug.Log("Porte ouverte !");
//     }
// }


using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject doorObject; // L'objet de la porte
    private bool isUnlocked = false;

    private void Update()
    {
        PlayerController player = FindObjectOfType<PlayerController>(); // Trouve le joueur

        if (player != null && player.hasKey && IsPlayerNear(player))
        {
            UnlockDoor(); // Ouvre la porte seulement si le joueur est proche et a la clé
        }
    }

    private bool IsPlayerNear(PlayerController player)
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        return distance < 2f; // Vérifie si le joueur est à 2 unités ou moins de la porte
    }

    public void UnlockDoor()
    {
        if (!isUnlocked)
        {
            isUnlocked = true;
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        doorObject.transform.position += new Vector3(0, 5, 0);
        Debug.Log("Porte ouverte !");
    }
}
