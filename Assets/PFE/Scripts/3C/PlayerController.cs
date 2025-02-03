using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public enum PlayerState
{
    Moving,
    Jumping,
    Falling,
    Grabbing,
    Interacting
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{
    private Camera cam;
    private Rigidbody2D body;
    private CapsuleCollider2D col;

    [SerializeField, ReadOnly] private bool _isTouchingGround;
    public bool IsTouchingGround
    {
        get => _isTouchingGround;
        private set
        {
            if (value != _isTouchingGround)
            {
                OnFloorContactChange(value);
            }

            _isTouchingGround = value;
        }
    }
    [SerializeField, ReadOnly] private bool _isFalling;
    public bool IsFalling
    {
        get => _isFalling;
        private set
        {
            if (value != _isFalling)
            {
                OnVelocityYChange(value);
            }

            _isFalling = value;
        }
    }

    public PlayerState State
    {
        get => _state;
        private set
        {
            if (value != _state)
            {
                Debug.Log($"Player State: {_state} -> {value}");
                OnPlayerStateChange(_state, value);
            }

            _state = value;
        }
    }
    [SerializeField, ReadOnly] private PlayerState _state = PlayerState.Moving;

    [SerializeField, Expandable] public PlayerStat Stats;

    private Vector2 HookPoint;
    public LineRenderer lineRenderer;
    public SpringJoint2D distanceJoint;

    private void Awake()
    {
        cam = Camera.main;
        body = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();

        PlayerInput.OnMove += Move;
        PlayerInput.OnInteract += Interact;
        PlayerInput.OnJumpPressed += OnJumpPressed;
        PlayerInput.OnJumpReleased += OnJumpReleased;
        PlayerInput.OnGrabMaintain += StartGrab;
        PlayerInput.OnGrabRelease += StopGrab;
    }

    private void Start()
    {
        distanceJoint.enabled = false;
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        lineRenderer.SetPosition(1, transform.position);
    }
    private void FixedUpdate()
    {
        if (State == PlayerState.Interacting) return;

        //Clamp Speed
        if (State == PlayerState.Grabbing)
        {
            if(body.velocity.magnitude > Stats.maxSpeedGrab)
            {
                body.velocity = body.velocity.normalized * Stats.maxSpeedGrab;
            }
        }
        else
        {
            Vector2 velocity = body.velocity;
            velocity.x = Mathf.Clamp(velocity.x, -Stats.maxSpeedHorizontal, Stats.maxSpeedHorizontal);
            velocity.y = Mathf.Clamp(velocity.y, -Stats.maxSpeedVertical, Stats.maxSpeedVertical);
            body.velocity = velocity;
        }

        //IsFalling
        IsFalling = body.velocity.y < -1f;

        //Check Touching Ground
        IsTouchingGround = Physics2D.Raycast(body.position + Vector2.down * (col.size.y / 2 + 0.1f), Vector2.down, .2f);
        Debug.DrawRay(body.position + Vector2.down * (col.size.y / 2 + 0.1f), Vector2.down * .2f, IsTouchingGround ? Color.green : Color.red);
    }

    private void OnPlayerStateChange(PlayerState oldState, PlayerState newState)
    {
        switch (newState)
        {
            case PlayerState.Moving:
                body.gravityScale = 3;
                break;
            case PlayerState.Jumping:
                break;
            case PlayerState.Falling:
                body.gravityScale = Stats.fallGravityScale;
                break;
            case PlayerState.Grabbing:
                lineRenderer.SetPosition(0, HookPoint);
                distanceJoint.connectedAnchor = HookPoint;

                distanceJoint.enabled = true;
                lineRenderer.enabled = true;
                break;
            case PlayerState.Interacting:
                break;
            default:
                break;
        }

        switch (oldState)
        {
            case PlayerState.Moving:
                break;
            case PlayerState.Jumping:
                break;
            case PlayerState.Falling:
                break;
            case PlayerState.Grabbing:
                distanceJoint.enabled = false;
                lineRenderer.enabled = false;
                break;
            case PlayerState.Interacting:
                break;
            default:
                break;
        }
    }

    private void Interact()
    {

    }

    private void Move(Vector2 direction)
    {
        if (State == PlayerState.Interacting) return;
        direction.y = 0;

        if (State == PlayerState.Jumping || State == PlayerState.Falling)
        {
            body.AddForce(direction * Stats.airSpeed, ForceMode2D.Force);
        }
        else if (State == PlayerState.Grabbing)
        {
            Vector2 grabDirection = ((Vector2)transform.position - HookPoint).normalized;
            //Rotate grab direction by -90°
            Vector2 perpendicular = new Vector2(-grabDirection.y, grabDirection.x);

            body.AddForce(perpendicular * direction.x * Stats.grabSpeed, ForceMode2D.Force);
        }
        else
        {
            if (Mathf.Abs(direction.x) < 0.1f)
            {
                //Slow down
                float speed = Mathf.Lerp(body.velocity.x, 0, Stats.brakeSpeed * Time.deltaTime);
                body.velocity = new Vector2(body.velocity.x * 0.9f, body.velocity.y);
            }
            else
            {
                body.AddForce(direction * Stats.speed, ForceMode2D.Force);
            }
        }
    }

    private void OnJumpPressed()
    {
        if (State == PlayerState.Interacting) return;

        //Check if the player is grounded
        if (IsTouchingGround) Jump(Stats.jumpForce);
        else if (State == PlayerState.Grabbing) Jump(Stats.jumpForce * .5f);

    }
    private void Jump(float force)
    {
        State = PlayerState.Jumping;
        body.AddForce(Vector2.up * Stats.jumpForce, ForceMode2D.Impulse);
    }

    private void OnJumpReleased()
    {
        if (State != PlayerState.Jumping) return;
        State = PlayerState.Falling;
    }
    private void OnFloorContactChange(bool isTouchingGround)
    {
        if (isTouchingGround)
        {
            //Atterrissage
            State = PlayerState.Moving;
        }
        else
        {
            if (State != PlayerState.Jumping) State = PlayerState.Falling;
        }
    }
    private void OnVelocityYChange(bool isFalling)
    {
        if (isFalling)
        {
            if (State == PlayerState.Jumping)
                State = PlayerState.Falling;
        }
    }

    private void StartGrab()
    {
        if (State == PlayerState.Interacting) return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider == null) return;

        //Check Layer
        if (Stats.grabLayer != hit.collider.gameObject.layer) return;

        IHookable hookable = hit.collider.GetComponent<IHookable>();
        if (hookable == null) return;

        HookPoint = hookable.GetHookPoint(hit.point);
        State = PlayerState.Grabbing;
    }
    private void StopGrab()
    {
        if (State != PlayerState.Grabbing) return;

        State = PlayerState.Falling;
    }
}