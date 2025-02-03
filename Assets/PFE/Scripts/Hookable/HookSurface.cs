using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HookSurface : MonoBehaviour, IHookable
{
    Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }
    public Vector2 GetHookPoint(Vector2 playerPos)
    {
        return col.ClosestPoint(playerPos);
    }
}