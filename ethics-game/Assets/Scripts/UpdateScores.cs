using TMPro;
using UnityEngine;
using CodeMonkey.HealthSystemCM;

public class UpdateScores : MonoBehaviour
{
    private TMP_Text[] textItems;
    public HealthBarUI[] healthBars; // Add this line to hold HealthBarUI references

    public int score1 = 50;
    public int score2 = 50;
    public int score3 = 50;
    public int score4 = 50;

    public int maxThreshold = 80;

    void Awake()
    {
        textItems = GetComponentsInChildren<TMP_Text>();
        UpdateTextItems(); // Add this line to initialize text items
        UpdateHealthBars(); // Add this line to initialize health bars
    }

    public void updateMultiple(int[] changes)
    {
        score1 += changes[0];
        score2 += changes[1];
        score3 += changes[2];
        score4 += changes[3];

        score1 = Mathf.Clamp(score1, 0, 100);
        score2 = Mathf.Clamp(score2, 0, 100);
        score3 = Mathf.Clamp(score3, 0, 100);
        score4 = Mathf.Clamp(score4, 0, 100);

        UpdateTextItems(); // Add this line to update text items
        UpdateHealthBars(); // Add this line to update health bars
        CheckGameOver();
    }

    private void UpdateTextItems() // Add this method to update text items
    {
        bool anyAboveThreshold = score1 >= maxThreshold || score2 >= maxThreshold || score3 >= maxThreshold || score4 >= maxThreshold;

        textItems[0].text = anyAboveThreshold && score1 < maxThreshold ? "" : "Justice: " + score1;
        textItems[1].text = anyAboveThreshold && score2 < maxThreshold ? "" : "Virtue: " + score2;
        textItems[2].text = anyAboveThreshold && score3 < maxThreshold ? "" : "Happiness: " + score3;
        textItems[3].text = anyAboveThreshold && score4 < maxThreshold ? "" : "Control: " + score4;
    }

    private void UpdateHealthBars() // Add this method to update health bars
    {
        if (healthBars != null && healthBars.Length == 4) {
            healthBars[0].value = score1;
            healthBars[1].value = score2;
            healthBars[2].value = score3;
            healthBars[3].value = score4;
        }
    }

    public bool ShouldHideValues()
    {
        return score1 >= maxThreshold || score2 >= maxThreshold || score3 >= maxThreshold || score4 >= maxThreshold;
    }

    private void CheckGameOver()
    {
        int[] scores = { score1, score2, score3, score4 };
        string[] scoreNames = { "Justice", "Virtue", "Happiness", "Control" };

        for (int i = 0; i < scores.Length; i++)
        {
            if (scores[i] == 100)
            {
                Debug.Log($"Game Over: {scoreNames[i]} reached 100!");
            }
            else if (scores[i] == 0)
            {
                Debug.Log($"Game Over: {scoreNames[i]} reached 0!");
            }
        }
    }
}
