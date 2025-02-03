using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class PlayerInput : Singleton<PlayerInput>
{
    public static UnityAction OnInteract;
    public static UnityAction OnJumpPressed;
    public static UnityAction OnJumpReleased;
    public static UnityAction OnGrabMaintain;
    public static UnityAction OnGrabRelease;
    public static UnityAction<Vector2> OnMove;

    [SerializeField, ReadOnly] private Vector2 rawStick;
    [SerializeField, ReadOnly] private Vector2 cleanStick;

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            OnJumpPressed.Invoke();
        }
        else if (Input.GetButtonUp("Jump"))
        {
            OnJumpReleased.Invoke();
        }

        if (Input.GetButtonDown("Interact"))
        {
            OnInteract.Invoke();
        }

        if (Input.GetMouseButtonDown(0))
        {
            OnGrabMaintain.Invoke();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnGrabRelease.Invoke();
        }

        rawStick = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        cleanStick = rawStick.magnitude > 1 ? rawStick.normalized : rawStick;

        OnMove.Invoke(cleanStick);
    }
}
