using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "PlayerStat", menuName = "PFE/PlayerStat", order = 0)]
public class PlayerStat : ScriptableObject
{
    public float brakeSpeed = .5f;
    public float speed = 8;
    public float airSpeed = 8;
    public float grabSpeed = 8;

    public float maxSpeedGrab = 8;
    public float maxSpeedVertical = 8;
    public float maxSpeedHorizontal = 8;

    public float jumpForce = 8;
    public float fallGravityScale = 2;

    [Layer] public int grabLayer;
    public float grabDistance = 30;
}
