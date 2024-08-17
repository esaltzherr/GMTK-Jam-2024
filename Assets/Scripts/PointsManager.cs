using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    public TextMeshProUGUI pointsText; // Reference to the TextMeshProUGUI component
    private int currentPoints = 0; // The current score

    void Start()
    {
        // Initialize the points text with the starting points value
        UpdatePointsText();
    }

    // Method to add points
    public void AddPoints(int points)
    {
        currentPoints += points;
        UpdatePointsText();
    }

    // Method to subtract points (if needed)
    public void SubtractPoints(int points)
    {
        currentPoints -= points;
        if (currentPoints < 0)
        {
            currentPoints = 0; // Prevent negative points
        }
        UpdatePointsText();
    }

    public void HeightPoints(int points)
    {
        if (points >= 0)
        {
            AddPoints(points);
        }
        else
        {
            SubtractPoints(points);
        }
    }

    // Method to update the TMP text component with the current points
    private void UpdatePointsText()
    {
        if (pointsText != null)
        {
            pointsText.text = "Points: " + currentPoints.ToString();
        }
    }

    // Optional: Method to reset points
    public void ResetPoints()
    {
        currentPoints = 0;
        UpdatePointsText();
    }
}
