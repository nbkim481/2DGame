using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private float detectionRange = 5f; // How close the player needs to be
    [SerializeField] private int damageValue = 1;

    private Transform playerTransform;

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // Calculate the current distance between this enemy and the player
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

            // Only move if the player is inside the detection range
            if (distanceToPlayer <= detectionRange)
            {
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    playerTransform.position,
                    speed * Time.deltaTime
                );
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet") || other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        // Try to get the consolidated PlayerHealth script from the object we overlapped
        if (other.TryGetComponent<PlayerHealth>(out var playerHealth))
        {
            playerHealth.TakeDamage(damageValue);
        }
    }
}
