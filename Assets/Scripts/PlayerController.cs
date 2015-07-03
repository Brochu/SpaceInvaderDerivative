using UnityEngine;
using System;

/// <summary>
/// In this class, we get the inputs the player gives to the game and react to them.
/// We also get the controls to tweak how the ship responds to the inputs
/// </summary>
public class PlayerController : MonoBehaviour {

    public static event Action onShoot;

    // This can be used to cancel player inputs for a small cutscene for example or after a level is done
    public bool ignorePlayerInput = false;

    // This will be used to tune the speed at which the spaceship will move
    public float horizontalSpeed = 1.0f;
    public float verticalSpeed = 1.0f;

    public float minHorizontalPos = -10;
    public float maxHorizontalPos = 10;
    public float minVerticalPos = 0;
    public float maxVerticalPos = 15;

    private Vector3 initialPos = Vector3.zero;
    private Vector3 move = Vector3.zero;

    private bool alreadyShot = false;

    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update ()
    {
        // Here we modify the move vector with the inputs, taking the speed factor into accout
        move.x = Mathf.Clamp(move.x + (getHorizontalMovement() * horizontalSpeed), minHorizontalPos, maxHorizontalPos);
        move.y = Mathf.Clamp(move.y + (getVerticalMovement() * verticalSpeed), minVerticalPos, maxVerticalPos);

        transform.position = initialPos + move;

        // Check if the player shot
        if (Input.GetKeyDown(KeyCode.Space) && !alreadyShot)
        {
            // So we shoot only once per press
            alreadyShot = true;
            if (onShoot != null)
            {
                onShoot();
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            // Reset the flag to let player shoot again
            alreadyShot = false;
        }
    }

    // This will return weather or not we need to move the ship horizontally.
    // A positive value will move the ship to the right, a negative value will move the ship to the left
    private float getHorizontalMovement()
    {
        return (!ignorePlayerInput) ? Input.GetAxis("Horizontal") : 0;
    }

    // This will return weather or not we need to move the ship vertically.
    // A positive value will move the ship down, a negative value will move the ship up
    private float getVerticalMovement()
    {
        return (!ignorePlayerInput) ? Input.GetAxis("Vertical") : 0;
    }
}