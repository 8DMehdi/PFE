using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerFlight : MonoBehaviour
{
    private Rigidbody2D body;
    private bool _canFly = false;

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
    Debug.Log($"_canFly: {_canFly}");
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
        float moveHorizontal = Input.GetAxis("Horizontal");

        if (Mathf.Abs(moveHorizontal) > 0.1f)
        {
            body.velocity = new Vector2(moveHorizontal * flySpeed, body.velocity.y);
        }

        if (Mathf.Abs(body.velocity.y) > maxVerticalSpeed)
        {
            body.velocity = new Vector2(body.velocity.x, Mathf.Sign(body.velocity.y) * maxVerticalSpeed);
        }

        if (Mathf.Abs(moveHorizontal) > 0.5f)
        {
            body.velocity = new Vector2(body.velocity.x, Mathf.Lerp(body.velocity.y, levitationForce, 0.5f));
        }
        else
        {
            body.velocity = new Vector2(body.velocity.x, Mathf.Lerp(body.velocity.y, -levitationForce, 0.5f));
        }

        body.AddForce(Vector2.down * gravity * Time.deltaTime);
    }

    public void EnableFly()
    {
        _canFly = true;
        Debug.Log("Flying ability enabled!");
    }

    public void DisableFly()
    {
        _canFly = false;
        Debug.Log("Flying ability disabled!");
    }

    private void ToggleFlight()
    
    {
        _canFly = !_canFly;
        Debug.Log(_canFly ? "Flying ability enabled!" : "Flying ability disabled!");
    }

    
}

