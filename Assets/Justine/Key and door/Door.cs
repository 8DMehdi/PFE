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
        return distance < 5f;
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
            SoundManager.Instance.PlayDoorSound();

        }

        doorObject.transform.position = openPosition;
    }
}