using UnityEngine;
using TMPro; // Required to control TextMeshPro elements

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 5;

    [Header("UI Reference")]
    // Drag your TextMeshPro text object into this slot in the Inspector
    [SerializeField] private TextMeshProUGUI healthTextElement;

    // Public read access so other systems can still check the player's health
    public int CurrentHealth { get; private set; }

    void Awake()
    {
        // Initialize health when the game starts
        CurrentHealth = maxHealth;
    }

    void Start()
    {
        // Display the starting health number immediately
        UpdateTextUI();
    }

    /// <summary>
    /// Reduces health and updates the onscreen number.
    /// </summary>
    public void TakeDamage(int damageAmount)
    {
        Debug.Log(
            $"{name}: Taking {damageAmount} damage. Health before: {CurrentHealth}",
            this
        );

        CurrentHealth -= damageAmount;
        CurrentHealth = Mathf.Max(CurrentHealth, 0);

        Debug.Log($"{name}: Health after: {CurrentHealth}", this);

        UpdateTextUI();

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Refreshes the text display on screen.
    /// </summary>
    private void UpdateTextUI()
    {
        if (healthTextElement != null)
        {
            healthTextElement.text = "Lives: " + CurrentHealth.ToString();

            // ALTERNATIVE: If you prefer a fraction layout (e.g. "100 / 100"), use this instead:
            // healthTextElement.text = $"{CurrentHealth} / {maxHealth}";
        }
        else
        {
            Debug.LogWarning($"Health text reference missing on {gameObject.name}!", this);
        }
    }

    private void Die()
    {
        Debug.Log("Player has died!");
        gameObject.SetActive(false);
    }
}
