
// using UnityEngine;

// public class FlyCollectable : MonoBehaviour
// {
//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.TryGetComponent<PlayerFly>(out var playerFly))
//         {
//             Debug.Log("Player entered trigger zone!");
//             playerFly.EnableFly(); 
//             Destroy(gameObject);  
//         }
//         else
//         {
//             Debug.LogWarning("Trigger entered, but no PlayerFly component found.");
//         }
//     }
// }
