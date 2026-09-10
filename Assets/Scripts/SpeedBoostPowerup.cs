using UnityEngine;

public class SpeedBoostPowerup : Powerup
{
    //Inheritance: By changing 'MonoBehaviour' to 'Powerup', this class instantly inherits
    //the timer, collision detection, and protected variables from the base class.
    [Header("Speed Settings")]
    [SerializeField] private float _speedBoostAmount = 8.0f;

    //Polymorphism: We override the empty base method to give it specific behavior.
    protected override void ApplyEffect(PlayerController player)
    {
        player.MoveSpeed += _speedBoostAmount;
        Debug.Log($"Speed Boost Applied! New Speed: {player.MoveSpeed}");
    }
    //Polymorphism: We revert the exact changes made in ApplyEffect.
    protected override void RemoveEffect(PlayerController player)
    {
        player.MoveSpeed -= _speedBoostAmount;
        Debug.Log($"Speed Boost Removed! Speed back to: {player.MoveSpeed}");
    }
}
