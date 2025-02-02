using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHookable
{
    public Vector2 GetHookPoint(Vector2 playerPos);
}

public class HookPoint : MonoBehaviour, IHookable
{
    public Vector2 GetHookPoint(Vector2 playerPos)
    {
        return transform.position;
    }
}
