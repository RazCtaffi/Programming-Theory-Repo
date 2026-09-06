using System.Collections;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField] private float _rotationSpeed = 100f;
    
    [Header("Settings")]
    [SerializeField] private float _duration = 5f;
    public float Duration 
    {
        get { return _duration; }
        set { _duration = Mathf.Max(0f, value); }
    }

    private Collider _powerupCollider;
    protected Renderer _powerupRenderer;

    private void Awake()
    {
        _powerupCollider = GetComponent<Collider>();
        _powerupRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            Collect(player);
        }
    }

    private void Collect(PlayerController player)
    {
        if (_powerupCollider != null) _powerupCollider.enabled = false;
        if (_powerupRenderer != null) _powerupRenderer.enabled = false;

        StartCoroutine(PowerupRoutine(player));
    }
    private IEnumerator PowerupRoutine(PlayerController player)
    {
        ApplyEffect(player);
        yield return new WaitForSeconds(Duration);
        if (player != null)
        {
            RemoveEffect(player);
        }
        Destroy(gameObject);
    }

    //Polymorphism and Inheritance preparation:
    //These methods do nothing in the base class. 
    //Child classes will override them to create unique effects.

    protected virtual void ApplyEffect(PlayerController player) 
    {
        Debug.Log("Base Powerup Applied!");
    }

    protected virtual void RemoveEffect(PlayerController player) 
    {
        Debug.Log("Base Powerup Removed!");
    }
}
