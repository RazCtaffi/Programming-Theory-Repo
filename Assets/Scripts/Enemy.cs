using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 8.0f;
    protected virtual float Speed
    {
        get { return _moveSpeed; }
        set { _moveSpeed = Mathf.Max(0f, value); }
    }
    protected Rigidbody rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    protected virtual void Move()
    {
        Vector3 targetPosition = rb.position + Vector3.back * (Speed * Time.fixedDeltaTime);
        rb.MovePosition(targetPosition);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            Die();
        }
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController player))
        {
            player.GameOver();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
