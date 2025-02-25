using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerFlight : MonoBehaviour
{
    private Rigidbody2D body;
    
    // Indique si le mode vol est actuellement activé
    private bool _canFly = false;
    // Indique si le joueur a débloqué l'aptitude de vol (par exemple, en récupérant une clé)
    private bool flightUnlocked = false;

    [SerializeField] private float flySpeed = 5f;
    [SerializeField] private float levitationForce = 5f;
    [SerializeField] private float gravity = 5f;
    [SerializeField] private float maxVerticalSpeed = 10f;

    private float lastToggleTime = 0f;
    private float toggleCooldown = 0.2f; // 200 ms entre chaque activation

private void Awake()
{
    body = GetComponent<Rigidbody2D>();
}


private void Update()
{
    Debug.Log($"Update: _canFly = {_canFly}");
    if (Input.GetKeyDown(KeyCode.P) && Time.time - lastToggleTime > toggleCooldown)
    {
        ToggleFlight();
        lastToggleTime = Time.time;
    }
}

private void FixedUpdate()
{
    Debug.Log($"FixedUpdate: _canFly = {_canFly}");
    if (_canFly)
    {
        HandleFlying();
    }
}

    private void HandleFlying()
    {
        Debug.Log("HandleFlying() exécuté !");
        float moveHorizontal = Input.GetAxis("Horizontal");

        if (Mathf.Abs(moveHorizontal) > 0.1f)
        {
            body.velocity = new Vector2(moveHorizontal * flySpeed, body.velocity.y);
        }

        // Limiter la vitesse verticale
        if (Mathf.Abs(body.velocity.y) > maxVerticalSpeed)
        {
            body.velocity = new Vector2(body.velocity.x, Mathf.Sign(body.velocity.y) * maxVerticalSpeed);
        }

        // Appliquer une force de levitation ou de descente selon l'entrée horizontale
        if (Mathf.Abs(moveHorizontal) > 0.5f)
        {
            body.velocity = new Vector2(body.velocity.x, Mathf.Lerp(body.velocity.y, levitationForce, 0.5f));
        }
        else
        {
            body.velocity = new Vector2(body.velocity.x, Mathf.Lerp(body.velocity.y, -levitationForce, 0.5f));
        }

        // Appliquer une force de gravité manuelle
        body.AddForce(Vector2.down * gravity * Time.deltaTime);
    }

    // Méthode à appeler lorsque le joueur récupère la clé de vol

    public void UnlockFlight()
{
    Debug.Log("UnlockFlight() appelé !");
    flightUnlocked = true;
    _canFly = true;
    Debug.Log($"Après UnlockFlight: flightUnlocked = {flightUnlocked}, _canFly = {_canFly}");
}

    // Vous pouvez également "refroisser" l'aptitude si besoin
    public void LockFlight()
    {
        flightUnlocked = true;
        _canFly = false;
        Debug.Log("Aptitude de vol verrouillée !");
    }

    // Active le vol si le joueur a débloqué l'aptitude
    public void EnableFly()
    {
        if (flightUnlocked)
        {
            _canFly = true;
            Debug.Log("Vol activé !");
        }
        else
        {
            Debug.Log("L'aptitude de vol n'est pas encore débloquée !");
        }
    }

    // Désactive le vol
    public void DisableFly()
    {
        _canFly = false;
        Debug.Log("Vol désactivé !");
    }
    private void ToggleFlight()
{
    Debug.Log($"ToggleFlight() appelé. flightUnlocked = {flightUnlocked}");

    if (!flightUnlocked)
    {
        Debug.Log("L'aptitude de vol n'est pas encore débloquée !");
        return;
    }

    _canFly = !_canFly;
    Debug.Log(_canFly ? "Vol activé !" : "Vol désactivé !");
}

}
