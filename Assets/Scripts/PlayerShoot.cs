using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform Aim;
    public float fireRate = 0.5f;

    private float nextFireTime = 0f;

    void Update()
    {
        // Check for input (Left Mouse Click or Spacebar)
       if (Mouse.current.leftButton.isPressed)
        {
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void Shoot()
    {
        // Instantiate the bullet at the firePoint's position and rotation
        Instantiate(bulletPrefab, Aim.position, Aim.rotation);
    }
}
