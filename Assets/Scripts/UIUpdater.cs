using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// This class is attached to the UI in-game and will pickup events of when to refresh the stats shown to the user.
/// </summary>
public class UIUpdater : MonoBehaviour {

    // Actual text element in the canvas that will hold the score variable
    public Text scoreTxt = null;

    private int lastScore = 0;
    private int desiredScore = 0;

    void Start ()
    {
        // Register the event that signals a change in stats
        GameManager.onUpdateUI += onUpdateUI;
        scoreTxt.text = "0";
    }

    void onUpdateUI(int newScore)
    {
        // When we receive a change in the score we stop the current change to upate with the newest information
        desiredScore = newScore;
        StopCoroutine(refreshScore());
        StartCoroutine(refreshScore());
    }

    IEnumerator refreshScore()
    {
        // Progressively adding the score to mark progress
        int diff = desiredScore - lastScore;
        int step = diff / 10;

        for (int i = 0; i < 10; ++i)
        {
            scoreTxt.text = (lastScore + (step * i)).ToString();
            yield return null;
        }
        scoreTxt.text = desiredScore.ToString();

        lastScore = desiredScore;
    }
}