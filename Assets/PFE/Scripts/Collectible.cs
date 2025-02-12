using UnityEngine;

public class Collectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerRespawn>(); 
        if (player != null)
        {
            CollectibleManager.Instance.AddCollectible(5);
            Destroy(gameObject); 
        }
        SoundManager.Instance.PlayCollectSound();
    }
}
