
 using UnityEngine;

public class PlayerFly : MonoBehaviour
{
    public float vitesseH = 5f;     
    public float forceDeLevitation = 5f;  
    public float gravité = 5f;   

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        float moveHorizontal = Input.GetAxis("Horizontal"); 
        rb.velocity = new Vector2(moveHorizontal * vitesseH, rb.velocity.y);

        if (Mathf.Abs(moveHorizontal) > 0.1f) 
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Lerp(rb.velocity.y, forceDeLevitation, 0.1f));
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Lerp(rb.velocity.y, 0f, 0.1f));
        }

        rb.AddForce(Vector2.down * gravité); 
    }
}



// using UnityEngine;

// public class PlayerFly : MonoBehaviour
// {
//     public float vitesseH = 5f;            // Vitesse de déplacement horizontal
//     public float forceDeLevitation = 20f;  // Force qui aide à maintenir le joueur en l'air
//     public float gravité = 5f;            // Gravité qui tire le joueur vers le bas

//     private Rigidbody2D _rb;
//     private bool _canFly = false;         // Capacité de voler activée/désactivée

//     void Start()
//     {
//         _rb = GetComponent<Rigidbody2D>();
//         if (_rb == null)
//         {
//             Debug.LogError("Rigidbody2D is missing on the player!");
//         }
//     }

//     public void EnableFly()
//     {
//         _canFly = true;
//         Debug.Log("Flying ability enabled!"); // Vérifiez si ce message apparaît dans la console
//     }

//     private void Update()
//     {
//         if (_canFly)
//         {
//             Debug.Log("Handling flying..."); // Vérifiez si ce message apparaît
//             HandleFlying();
//         }
//         else
//         {
//             ApplyNormalGravity();
//         }


//     }
//     private void HandleFlying()
//     {
//         // Déplacement horizontal
//         float moveHorizontal = Input.GetAxis("Horizontal"); // touches fléchées ou Q/D
//         _rb.velocity = new Vector2(moveHorizontal * vitesseH, _rb.velocity.y);

//         // Contrôle de la lévitation
//         if (Mathf.Abs(moveHorizontal) > 0.1f)
//         {
//             // Appliquer une force ascendante lors du déplacement horizontal
//             _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Lerp(_rb.velocity.y, forceDeLevitation, 0.1f));
//             Debug.Log("Applying levitation force...");
//         }
//         else
//         {
//             // Maintenir une stabilité verticale quand il n'y a pas de mouvement horizontal
//             _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Lerp(_rb.velocity.y, 0f, 0.1f));
//         }

//         // Appliquer la gravité manuellement
//         _rb.AddForce(Vector2.down * gravité);
//     }

//     private void ApplyNormalGravity()
//     {
//         // Si le joueur ne vole pas, appliquer une gravité classique
//         _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Lerp(_rb.velocity.y, -gravité, 0.1f));
//         Debug.Log("Applying normal gravity...");
//     }
// }
