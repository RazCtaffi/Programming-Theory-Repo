using System;
using UnityEngine;


//Inheritance
public class SpreadShotPowerup : Powerup
{
    [Header("Spread Shot Settings")]
    [SerializeField, Range(20f, 60f)]
    private float _spreadAngle = 20f;

    //Encapsulation: The Spread Angle variable can only be between 20f and 60f.
    private float SpreadAngle
    {
        get { return _spreadAngle; }
        set { _spreadAngle = Mathf.Clamp(value, 20.0f, 60.0f); }
    }

    private void OnValidate()
    {
        SpreadAngle = _spreadAngle;
    }

    //Polymorphism
    protected override void ApplyEffect(PlayerController player)
    {
        player.ActiveSpreadAngle = SpreadAngle;
        player.IsSpreadShotActive = true;
        Debug.Log("Spread Shot Activated!");
    }
    //Polymorphism
    protected override void RemoveEffect(PlayerController player)
    {
        player.IsSpreadShotActive = false;
        Debug.Log("Spread Shot Deactivated!");
    }
}
