using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform fireballSpawnPoint;
    public float fireballSpeed = 10f;
    public float fireballLifetime = 2f;
    public float cooldown = 1f;
    private bool canShoot = true;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (canShoot && Input.GetButtonDown("Fire1"))
        {
            ShootFireball();
        }
    }

    void ShootFireball()
    {
        // Set canShoot to false to start the cooldown
        canShoot = false;

        // Instantiate the fireball prefab at the fireball spawn point
        GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, Quaternion.identity);

        // Calculate the direction to shoot the fireball based on the mouse position
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.nearClipPlane;
        Vector3 direction = mainCamera.ScreenToWorldPoint(mousePosition) - fireballSpawnPoint.position;
        direction.Normalize();

        // Rotate the fireball to face the direction it will be moving
        fireball.transform.rotation = Quaternion.LookRotation(direction);

        // Add force to the fireball in the calculated direction
        fireball.GetComponent<Rigidbody>().AddForce(direction * fireballSpeed, ForceMode.VelocityChange);

        // Destroy the fireball after the specified lifetime
        Destroy(fireball, fireballLifetime);
        // Start the cooldown coroutine
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);

        // Set canShoot to true to allow shooting again
        canShoot = true;
    }
}
