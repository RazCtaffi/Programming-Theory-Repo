using UnityEngine;

public class RapidFirePowerup : Powerup
{
    [Header("Fire Settings")]
    [SerializeField, Range(0.05f, 0.75f)] private float _newFireRate = 0.25f;
    private float NewFireRate
    {
        get {  return _newFireRate; }
        set { _newFireRate = Mathf.Clamp(value, 0.05f, 0.75f); }
    }
    private float originalFireRate;

    private void OnValidate()
    {
        NewFireRate = _newFireRate;
    }
    protected override void ApplyEffect(PlayerController player)
    {
        originalFireRate = player.FireRate;
        player.FireRate = NewFireRate;
        Debug.Log($"Rapid Fire Activated! Fire rate is now: {player.FireRate}");
    }

    protected override void RemoveEffect(PlayerController player)
    {
        player.FireRate = originalFireRate;
        Debug.Log($"Rapid Fire Dectivated! Fire rate is now back to: {player.FireRate}");
    }
}
