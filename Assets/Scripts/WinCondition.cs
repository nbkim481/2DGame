using UnityEngine;
using UnityEngine.SceneManagement; // Required for reloading scenes

public class WinManager : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] private GameObject Win;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object entering the trigger is the Player
        if (collision.CompareTag("Player"))
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        Win.SetActive(true); // Show the win screen
        Time.timeScale = 0f;      // Freeze game logic/physics
    }

    // Call this function via the Button UI Component
    public void RestartGame()
    {
        Time.timeScale = 1f;      // Reset time back to normal before reloading
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}