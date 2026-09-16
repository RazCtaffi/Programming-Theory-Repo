using UnityEngine;

//Inheritance
public class ZigZagEnemy : Enemy
{
    [Header("Zig-Zag Settings")]
    [SerializeField] private float _frequency = 2f;
    [SerializeField] private float _amplitude = 3f;

    private float _offset;

    //Polymorphism
    protected override void Awake()
    {
        base.Awake();
        _offset = Random.Range(0f, 2f * Mathf.PI);
    }

    //Polymorphism
    protected override void Move()
    {
        float horizontalWave = Mathf.Sin((Time.time+ _offset) * _frequency) * _amplitude;
        Vector3 direction = new Vector3(horizontalWave, 0, -1).normalized;
        Vector3 targetPosition = rb.position + direction * (Speed * Time.fixedDeltaTime);
        rb.MovePosition(targetPosition);
    }
}
