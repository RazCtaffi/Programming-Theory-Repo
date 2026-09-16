using UnityEngine;

//Inheritance
public class SpeedyEnemy : Enemy
{
    [Header("Speed Settings")]
    [SerializeField] private float _speedMultiplier = 1.3f;

    //Polymorphism: overriding variables to give them new values.
    protected override int ScoreValue
    {
        get { return 12; }
    }
    protected override float Speed
    {
        get { return base.Speed * _speedMultiplier; }
    }
}
