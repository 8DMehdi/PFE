using UnityEngine;
using UnityEngine.UI;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance;
    private int collectibleCount = 0;
    public Text collectibleText; // Référence au texte UI

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateUI();
    }


    public void AddCollectible(int valeur)
    {
        collectibleCount += valeur; // Ajoute la valeur spécifiée
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (collectibleText != null)
        {
            collectibleText.text = "Collectibles: " + collectibleCount;
        }
    }
}
