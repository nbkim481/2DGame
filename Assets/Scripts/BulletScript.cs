using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 3f;

    void Start()
    {
        // Destroy the bullet after a set time to clean up memory
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Move the bullet forward on the X-axis (or transform.up for top-down)
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Optional: Destroy bullet on hitting an enemy or obstacle
        if (other.CompareTag("Enemy"))
        {
            // Add damage logic here if needed
            Destroy(gameObject);
        }
    }
}
