using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float topBound = 20f;
    private float botBound = -20f;
    void Update()
    {
        if (transform.position.z > topBound)
        {
            Destroy(gameObject);
        }
        if (transform.position.z < botBound)
        {
            Destroy(gameObject);
        }
    }
}
