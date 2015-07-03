using UnityEngine;
using System.Collections;

/// <summary>
/// This component represent the spaceship's weapon. It is responsible for the creation of bullets when player shoots.
/// It is also setup in a way that provides an easy way to change the type of projectile that it spawns in the scene
/// </summary>
public class Cannon : MonoBehaviour {

    // The prefab to instantiate when player shoots
    public GameObject bulletPrefab = null;

    // Register the shoot event
    void Start () {
        PlayerController.onShoot += onShoot;
    }

    void onShoot()
    {
        if (bulletPrefab == null)
        {
            // Error if we didn't specify what bullet to spawn
            Debug.LogError("Could not find a bullet prefab to shoot");
            return;
        }

        // Create instance of a bullet on the player position
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;
    }
    
    void OnDestroy()
    {
        // To make sure we don't register this event twice
        PlayerController.onShoot -= onShoot;
    }
}