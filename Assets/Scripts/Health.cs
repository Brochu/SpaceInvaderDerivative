using UnityEngine;
using System.Collections;

/// <summary>
/// This component will be attached to any game object that needs to take damage and keep track of HP.
/// </summary>
public class Health : MonoBehaviour {

    public int maxHealth;

    private int currentHealth;

    // The component will start at max health
    void Start () {
        currentHealth = maxHealth;
    }

    // When a bullet hits, il will call this method and pass in its damage value
    public void takeDamage(int amount)
    {
        Debug.Log(gameObject.ToString() + "Took " + amount + "damage.");
        currentHealth -= amount;
    }
}
