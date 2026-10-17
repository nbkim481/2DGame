using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerShoot : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform Aim;
    public float fireRate = 0.5f;

    [Header("Ammo Settings")]
    [SerializeField] private int maxAmmo = 10;
    [SerializeField] private int startingAmmo = 10;
    [SerializeField] private int ammoPerPickup = 5;

    [Header("UI Reference")]
    [SerializeField] private TextMeshProUGUI ammoTextElement;

    public int CurrentAmmo { get; private set; }

    private float nextFireTime = 0f;

    private void Awake()
    {
        CurrentAmmo = Mathf.Clamp(startingAmmo, 0, maxAmmo);
    }

    private void Start()
    {
        UpdateAmmoUI();
    }

    private void Update()
    {
        if (Mouse.current == null)
        {
            return;
        }

        if (Mouse.current.leftButton.isPressed &&
            Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        if (CurrentAmmo <= 0)
        {
            return;
        }

        if (bulletPrefab == null || Aim == null)
        {
            Debug.LogWarning(
                "Assign Bullet Prefab and Aim on PlayerShoot.",
                this
            );
            return;
        }

        Instantiate(bulletPrefab, Aim.position, Aim.rotation);

        CurrentAmmo--;
        UpdateAmmoUI();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Ammo"))
        {
            return;
        }

        // Ignore a pickup that has already been collected.
        if (!other.gameObject.activeSelf)
        {
            return;
        }

        // Leave the pickup on the ground if ammo is full.
        if (CurrentAmmo >= maxAmmo || ammoPerPickup <= 0)
        {
            return;
        }

        CurrentAmmo = Mathf.Min(
            CurrentAmmo + ammoPerPickup,
            maxAmmo
        );

        UpdateAmmoUI();

        // Disable immediately to prevent collecting it twice.
        other.gameObject.SetActive(false);
        Destroy(other.gameObject);
    }

    private void UpdateAmmoUI()
    {
        if (ammoTextElement != null)
        {
            ammoTextElement.text = $"Ammo: {CurrentAmmo} / {maxAmmo}";
        }
    }
}