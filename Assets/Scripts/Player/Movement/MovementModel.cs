using UnityEngine;

public class MovementModel
{
    [Range(0, 1)] public float progress;
    public float velocity;
    public bool isRiding = false;
    public float totalVelocity;
}
