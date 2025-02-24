using TMPro;
using UnityEngine;
using UnityEngine.UI; // Add this line to include the UI namespace
using CodeMonkey.HealthSystemCM;

public class UpdateScores : MonoBehaviour
{
    private TMP_Text[] textItems;
    public HealthBarUI[] healthBars; // Add this line to hold HealthBarUI references
    public GameObject gameOverObject; // Add this line to hold the GameOver object reference
    public TMP_Text daysText; // Add this line to hold the TMP_Text object for displaying days and reason
    public Button resetButton; // Change this line to hold the Button object for the reset button
    public DeckManager deckManager; // Add this line to hold the DeckManager reference

    public int score1 = 50;
    public int score2 = 50;
    public int score3 = 50;
    public int score4 = 50;
    public int days = 0; // Add this line to hold the days attribute

    public int maxThreshold = 80;

    void Awake()
    {
        textItems = GetComponentsInChildren<TMP_Text>();
        UpdateHealthBars(); // Add this line to initialize health bars
        if (resetButton != null)
        {
            resetButton.onClick.AddListener(ResetGame); // Update this line to add a listener to the reset button
        }
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

        days += Random.Range(0, 6); // Add this line to increment days by 0-5

        UpdateHealthBars(); // Add this line to update health bars
        CheckGameOver();
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
                string reason = $"{scoreNames[i]} reached 100!";
                Debug.Log($"Game Over: {reason}");
                DisplayDaysAndReason(reason);
                ActivateGameOverObject();
            }
            else if (scores[i] == 0)
            {
                string reason = $"{scoreNames[i]} reached 0!";
                Debug.Log($"Game Over: {reason}");
                DisplayDaysAndReason(reason);
                ActivateGameOverObject();
            }
        }
    }

    private void ActivateGameOverObject()
    {
        if (gameOverObject != null)
        {
            gameOverObject.SetActive(true);
            DeleteAllCards(); // Add this line to delete all cards
        }
        else
        {
            Debug.LogError("GameOver object not assigned.");
        }
    }

    private void DeleteAllCards() // Add this method to delete all cards
    {
        GameObject[] cards = GameObject.FindGameObjectsWithTag("Card");
        foreach (GameObject card in cards)
        {
            Destroy(card);
        }
    }

    private void DisableCardScripts() // Add this method to disable scripts in the "card" prefab
    {
        GameObject cardPrefab = GameObject.Find("Card(Clone)");
        if (cardPrefab != null)
        {
            MonoBehaviour[] scripts = cardPrefab.GetComponentsInChildren<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = false;
            }
        }
        else
        {
            Debug.LogError("Card prefab not found.");
        }
    }

    private void DisplayDaysAndReason(string reason) // Add this method to display days and reason
    {
        if (daysText != null)
        {
            daysText.text = $"Days: {days}\nReason: {reason}";
        }
        else
        {
            Debug.LogError("DaysText object not assigned.");
        }
    }

    private void ResetGame() // Modify this method to reset the game and respawn a card
    {
        score1 = 50;
        score2 = 50;
        score3 = 50;
        score4 = 50;
        days = 0;
        UpdateHealthBars();
        gameOverObject.SetActive(false);
        if (deckManager != null)
        {
            deckManager.SpawnNewCard(true); // Use the forceSpawn parameter to respawn a card
        }
        Debug.Log("Game has been reset.");
    }
}
