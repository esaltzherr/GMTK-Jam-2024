using System.Collections;
using TMPro;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    public TextMeshProUGUI pointsText; // Reference to the TextMeshProUGUI component
    public GameObject floatingTextPrefab; // Reference to the floating text prefab
    public Transform subtractEndLocation; // Reference to the transform of the pointsText
    public Transform subtractSpawnLocation;
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
        // Create floating text for subtraction
        CreateFloatingText(-points);

        // Subtract the points after the animation completes (optional delay)
        StartCoroutine(DelayedSubtractPoints(points, 1f)); // Adjust delay as needed
    }

    private IEnumerator DelayedSubtractPoints(int points, float delay)
    {
        yield return new WaitForSeconds(delay);
        currentPoints -= points;
        if (currentPoints < 0)
        {
            currentPoints = 0; // Prevent negative points
        }
        UpdatePointsText();
    }

    public void HeightPoints(int points)
    {
        if (points > 0)
        {
            Debug.Log("Adding " + points);
            AddPoints(points);
        }
        else if (points < 0)
        {
            Debug.Log("Subtracting " + points);
            SubtractPoints(-points);
        }
    }

    // Method to create floating text for point subtraction
    private void CreateFloatingText(int points)
    {
        if (floatingTextPrefab != null && subtractSpawnLocation != null)
        {
            GameObject floatingText = Instantiate(
                floatingTextPrefab,
                subtractSpawnLocation.position,
                Quaternion.identity,
                subtractSpawnLocation.parent
            );
            TextMeshProUGUI text = floatingText.GetComponent<TextMeshProUGUI>();

            if (text != null)
            {
                text.text = points.ToString();
            }

            // Start the animation to move the floating text
            StartCoroutine(AnimateFloatingText(floatingText, subtractEndLocation.position));
        }
    }

    private IEnumerator AnimateFloatingText(GameObject floatingText, Vector3 targetPosition)
    {
        RectTransform rectTransform = floatingText.GetComponent<RectTransform>();
        Vector3 startPosition = rectTransform.position;

        float duration = 1f; // Duration of the animation
        float elapsed = 0f;

        // Option 1: Accelerate into the target position (ease-in)
        // AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        // Option 2: Start with a small backup and then accelerate towards the target (ease-in with overshoot)
        // Uncomment the following curve if you want a small backup before flying towards the end location
        AnimationCurve curve = new AnimationCurve(
            new Keyframe(0, 0),          // Start at 0
            new Keyframe(0.3f, -0.3f),   // Back up slightly (optional)
            new Keyframe(1, 1)           // End at 1 (target position)
        );

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = curve.Evaluate(elapsed / duration); // Get the interpolated value from the curve
            rectTransform.position = Vector3.LerpUnclamped(startPosition, targetPosition, t);
            yield return null;
        }

        Destroy(floatingText);
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
