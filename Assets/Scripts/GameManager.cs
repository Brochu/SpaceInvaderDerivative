using UnityEngine;
using System;

/// <summary>
/// This class is where stats should be found. We could also keep player progress, keep level progress
/// For now it is really simple, only keeping track of a single score stat, but we could extend the functionality
/// (for example, add bullet limit on screen)
/// </summary>
public class GameManager : MonoBehaviour {

    public static event Action<int> onUpdateUI;

    private int currentScore = 0;

    void Start ()
    {
        // Register to the event when the player shoots
        PlayerController.onShoot += addScore;
    }

    private void addScore()
    {
        // Very simple reaction to a player's shot, could be changed with variable score additions
        // or keeping the total number of bullets on screen to a max value
        currentScore += 10;
        onUpdateUI(currentScore);
    }

    void OnDestroy()
    {
        PlayerController.onShoot -= addScore;
    }
}