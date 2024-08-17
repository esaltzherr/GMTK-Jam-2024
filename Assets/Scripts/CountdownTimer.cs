using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public float startTime = 120f;  // Countdown time in seconds (2 minutes = 120 seconds)
    public float climbTime = 60f;   // Countdown time for climb phase
    private float currentTime;
    public TextMeshProUGUI timerText;  // Reference to the TextMeshProUGUI component

    void Start()
    {
        // Initialize the timer with the start time
        currentTime = startTime;

        // Start the countdown coroutine
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        while (currentTime > 0)
        {
            // Update the timer text
            UpdateTimerText();

            // Wait for 1 second
            yield return new WaitForSeconds(1f);

            // Decrease the current time by 1 second
            currentTime--;
        }

        // Ensure the timer shows 00:00 when it reaches zero
        UpdateTimerText();

        // Optionally, you can trigger some event here when the countdown ends
        Debug.Log("Time's up!");
        GameEvents.Instance.OnTimerEnd();
    }

    public IEnumerator ClimbCountdown()
    {
        // Initialize the timer with the climb time
        currentTime = climbTime;

        while (currentTime > 0)
        {
            // Update the timer text
            UpdateTimerText();

            // Wait for 1 second
            yield return new WaitForSeconds(1f);

            // Decrease the current time by 1 second
            currentTime--;
        }

        // Ensure the timer shows 00:00 when it reaches zero
        UpdateTimerText();

        // Optionally, you can trigger some event here when the climb time ends
        Debug.Log("Climb time's up!");
        // GameEvents.Instance.OnClimbTimerEnd();
    }

    void UpdateTimerText()
    {
        // Calculate minutes and seconds
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        // Update the TMP text component
        timerText.text = string.Format("{0}:{1:00}", minutes, seconds);
    }
}
