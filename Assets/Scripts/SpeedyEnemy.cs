using UnityEngine;

public class SpeedyEnemy : Enemy
{
    [Header("Speed Settings")]
    [SerializeField] private float _speedMultiplier = 1.2f;
    protected override float Speed
    {
        get { return base.Speed * _speedMultiplier; }
    }
}
