using UnityEngine;
using System.Collections;

/// <summary>
/// This is a base class of a bullet that can be spawn when the player inputs the fire command.
/// This should be done so it can be inherited be other classes to change the basic behaviour (maybe add the tracking bullet)
/// </summary>
public class BulletBase : MonoBehaviour {

    // Tweakable values to change bullet's behaviour
    public int TTL = 100;
    public float speed = 1.0f;
    public int damage = 1;

    // The particle effect system to spawn when bullet hits something
    public GameObject explosionEffect = null;

    // Deactivate the bullet while letting coroutines execute
    protected bool active = true;

    private GameObject currentExplosionEffect = null;

    void Update()
    {
        if (!active)
            return;

        // each time the bullets moves, the TTL is reduced by 1 until 0
        if (TTL <= 0)
        {
            StartCoroutine(destroyBullet());
        }
        TTL--;

        moveBullet();
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if we hit a game object that can take damage
        Health otherHealth = other.gameObject.GetComponent<Health>();
        if (otherHealth != null)
            otherHealth.takeDamage(damage);

        // The bullet explodes if it comes into contact with something
        explode();
    }

    IEnumerator destroyBullet()
    {
        // If nothing is hit, then we destroy the bullet without any effects
        active = false;
        Destroy(gameObject);

        yield return null;
    }

    protected void moveBullet()
    {
        Vector3 currentPos = transform.position;
        currentPos.y += speed;

        transform.position = currentPos;
    }

    protected void explode()
    {
        // Deactivate and remove the bullet from the screen
        active = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        // Create the explosion effect
        currentExplosionEffect = Instantiate(explosionEffect);
        currentExplosionEffect.transform.position = transform.position;
        StartCoroutine(cleanEffect());
    }

    IEnumerator cleanEffect()
    {
        // Clean up the effect with it's done as well as the bullet
        yield return new WaitForSeconds(0.5f);
        Destroy(currentExplosionEffect);
        Destroy(gameObject);
    }
}