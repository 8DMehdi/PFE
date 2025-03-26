// using UnityEngine;

// public class Door : MonoBehaviour
// {
//     public GameObject doorObject; // L'objet de la porte
//     private bool isUnlocked = false;

//     private void Update()
//     {
//         PlayerController player = FindObjectOfType<PlayerController>(); // Trouve le joueur

//         if (player != null && player.hasKey && IsPlayerNear(player))
//         {
//             UnlockDoor(); // Ouvre la porte seulement si le joueur est proche et a la clé
//         }
//     }

//     private bool IsPlayerNear(PlayerController player)
//     {
//         float distance = Vector2.Distance(player.transform.position, transform.position);
//         return distance < 2f; // Vérifie si le joueur est à 2 unités ou moins de la porte
//     }

//     public void UnlockDoor()
//     {
//         if (!isUnlocked)
//         {
//             isUnlocked = true;
//             OpenDoor();
//         }
//     }

//     private void OpenDoor()
//     {
//         doorObject.transform.position += new Vector3(0, 5, 0);
//         Debug.Log("Porte ouverte !");
//         SoundManager.Instance.PlayDoorSound();

//     }
// }


using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    public GameObject doorObject; 
    private bool isUnlocked = false;
    public float openSpeed = 2f;
    private Vector3 closedPosition;
    private Vector3 openPosition;
    private Collider2D doorCollider; // Si en 2D
    private Collider doorCollider3D; // Si en 3D

    private void Start()
    {
        closedPosition = doorObject.transform.position;
        openPosition = closedPosition + new Vector3(0, 5, 0); // Vers le haut
        doorCollider = doorObject.GetComponent<Collider2D>(); // Si 2D
        doorCollider3D = doorObject.GetComponent<Collider>(); // Si 3D
    }

    private void Update()
    {
        PlayerController player = FindObjectOfType<PlayerController>();

        if (player != null && player.hasKey && IsPlayerNear(player))
        {
            UnlockDoor();
        }
    }

    private bool IsPlayerNear(PlayerController player)
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        return distance < 2f;
    }

    public void UnlockDoor()
    {
        if (!isUnlocked)
        {
            isUnlocked = true;
            StartCoroutine(OpenDoorSmoothly());
        }
    }

    private IEnumerator OpenDoorSmoothly()
    {
        float elapsedTime = 0f;

        // Désactiver le collider pour éviter le blocage
        if (doorCollider) doorCollider.enabled = false;
        if (doorCollider3D) doorCollider3D.enabled = false;

        while (elapsedTime < 1f)
        {
            doorObject.transform.position = Vector3.Lerp(closedPosition, openPosition, elapsedTime);
            elapsedTime += Time.deltaTime * openSpeed;
            yield return null;
        }

        doorObject.transform.position = openPosition;
        SoundManager.Instance.PlayDoorSound();
    }
}
