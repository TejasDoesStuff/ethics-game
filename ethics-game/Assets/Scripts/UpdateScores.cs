using TMPro;
using UnityEngine;
using UnityEngine.UI; // Add this line to include the UI namespace
using CodeMonkey.HealthSystemCM;
using System.Collections.Generic; // Add this line to include the Collections.Generic namespace

public class UpdateScores : MonoBehaviour
{
    private TMP_Text[] textItems;
    public HealthBarUI[] healthBars; // Add this line to hold HealthBarUI references
    public GameObject gameOverObject; // Add this line to hold the GameOver object reference
    public TMP_Text daysText; // Add this line to hold the TMP_Text object for displaying days and reason
    public Button resetButton; // Change this line to hold the Button object for the reset button
    public DeckManager deckManager; // Add this line to hold the DeckManager reference
    public RoleSelector roleSelector; // Add this line to hold the RoleSelector reference
    public GameObject roleSelectScreen; // Add this line to hold the role select screen object
    public List<Button> roleButtons; // Add this line to hold the list of role buttons
    public TMP_Text dayCounterText; // Add this line to hold the TMP_Text object for the day counter
    public AudioClip deathSoundEffect; // Add this line for the death sound effect
    public AudioSource deathAudioSource; // Add this line for the death audio source
    public AudioClip selectRoleSoundEffect; // New role selection sound effect
    public AudioSource selectRoleAudioSource; // New audio source for role selection

    public int score1 = 50;
    public int score2 = 50;
    public int score3 = 50;
    public int score4 = 50;
    public int days = 0; // Add this line to hold the days attribute
    public int months = 0; // Add this line to hold the months attribute

    public int maxThreshold = 80;

    public bool shutUp = false;

    void Awake()
    {
        shutUp = false;
        textItems = GetComponentsInChildren<TMP_Text>();
        UpdateHealthBars(); // Add this line to initialize health bars
        if (resetButton != null)
        {
            resetButton.onClick.AddListener(ResetGame); // Update this line to add a listener to the reset button
        }
        roleSelector = GetComponent<RoleSelector>(); // Add this line to get the RoleSelector component
        months = 0; // Initialize months to 0
        foreach (Button button in roleButtons)
        {
            button.onClick.AddListener(() => OnRoleButtonClicked(button)); // Add listeners to role buttons
        }
        roleSelectScreen.SetActive(false); // Hide role select screen initially
        UpdateDayCounter(); // Add this line to initialize the day counter
    }

    public void updateMultiple(int[] changes)
    {
        int[] scores = { score1, score2, score3, score4 };
        score1 += changes[0];
        score2 += changes[1];
        score3 += changes[2];
        score4 += changes[3];

        score1 = Mathf.Clamp(score1, 0, maxThreshold);
        score2 = Mathf.Clamp(score2, 0, maxThreshold);
        score3 = Mathf.Clamp(score3, 0, maxThreshold);
        score4 = Mathf.Clamp(score4, 0, maxThreshold);

        days += Random.Range(0, 16); // Add this line to increment days by 0-5

        if (days >= 30) // Check if days exceed or equal 30
        {
            months++;
            days = 0; // Reset days to 0
            if (!IsGameOver()) // Only show role select screen if the game is not over
            {
                ShowRoleSelectScreen(); // Show role select screen every month
            }
        }

        ApplyRoleEffects(); // Add this line to apply role-specific effects

        UpdateHealthBars(); // Add this line to update health bars
        UpdateDayCounter(); // Add this line to update the day counter
        CheckGameOver();
    }

    private bool IsGameOver() // Add this method to check if the game is over
    {
        int[] scores = { score1, score2, score3, score4 };
        for (int i = 0; i < scores.Length; i++)
        {
            if (scores[i] == 100)
            {
                return true;
            }
            else if (scores[i] == 0)
            {
                return true;
            }
        }
        return false;
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
    private void CheckGameOver()
    {
        int[] scores = { score1, score2, score3, score4 };
        // Custom death reasons for: Justice, Morality, Happiness, and Control respectively.
        string[] customDeathReasons = {
            "Justice: Lawyers around the city have had enough of you being unfair to the citizens of Ethika, and stage a successful insurrection on your government. It’s important to keep things just in your society.",
            "Morality: The people around you begin to notice you aren't really that good of a guy, often making decisions that seem plain wrong. The vice president has had enough of your nonsense, and poisons you in your sleep.",
            "Happiness: Due to your people getting sick of your reign, they rebel against you; in a violent rage you are sold to the enemy nation as a slave.",
            "Control: Due to a lack of control in the government, a terrorist organization takes over Ethika, leaving your head on a pole for everyone to see."
        };

        for (int i = 0; i < scores.Length; i++)
        {
            if (scores[i] == 100 || scores[i] == 0)
            {
                string reason = customDeathReasons[i];
                Debug.Log($"Game Over: {reason}");
                DisplayDaysAndReason(reason);
                ActivateGameOverObject();
            }
        }
    }

    private void ActivateGameOverObject()
    {
        shutUp = true;
        // Play death audio if assigned
        if (deathAudioSource != null && deathSoundEffect != null)
        {
            deathAudioSource.PlayOneShot(deathSoundEffect);
        }
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
            daysText.text = $"Months: {months}\nDays: {days}\n{reason}"; // Display months and days
        }
        else
        {
            Debug.LogError("DaysText object not assigned.");
        }
    }

    private void ResetGame() // Modify ResetGame to show all roles again and reset role to "freebie"
    {
        score1 = 30;
        score2 = 30;
        score3 = 30;
        score4 = 30;
        days = 0;
        months = 0; // Reset months to 0
        UpdateHealthBars();
        gameOverObject.SetActive(false);

        // Re-enable all role buttons
        foreach (var button in roleButtons)
        {
            button.gameObject.SetActive(true);
        }
        // Reset the role to "freebie". Assumes RoleSelector.Role.Freebie exists.
        if (roleSelector != null)
        {
            roleSelector.SelectRole(RoleSelector.Role.Freebie);
        }
        
        if (deckManager != null)
        {
            deckManager.SpawnNewCard(true); // Use the forceSpawn parameter to respawn a card
        }
        shutUp = false;
        Debug.Log("Game has been reset.");
        UpdateDayCounter(); // Update the day counter after resetting the game
    }

    private void ApplyRoleEffects() // Add this method to apply role-specific effects
    {
        if (roleSelector != null)
        {
            switch (roleSelector.selectedRole)
            {
                case RoleSelector.Role.Kant:
                    // Implement the logic to apply Kant's effects
                    break;
                case RoleSelector.Role.Mill:
                    // Implement the logic to apply Mill's effects
                    break;
                case RoleSelector.Role.Glaucon:
                    // Implement the logic to apply Glaucon's effects
                    break;
                case RoleSelector.Role.TheJudge:
                    // Implement the logic to apply The Judge's effects
                    break;
            }
        }
    }

    private void ShowRoleSelectScreen() // Add this method to show the role select screen
    {
        shutUp = true;
                DeleteAllCards(); // Hide cards by disabling their scripts
        roleSelectScreen.SetActive(true);
    }

    private void OnRoleButtonClicked(Button clickedButton) // Update role button click to disable selected button and check for all inactive
    {
        int index = roleButtons.IndexOf(clickedButton);
        if (index >= 0 && index < roleButtons.Count)
        {
            // Play role selection sound if assigned
            if (selectRoleAudioSource != null && selectRoleSoundEffect != null)
            {
                selectRoleAudioSource.PlayOneShot(selectRoleSoundEffect);
            }
            roleSelector.SelectRole((RoleSelector.Role)index); // Set the selected role
            clickedButton.gameObject.SetActive(false); // Disable the selected button
            
            // Check if all role buttons are inactive; if so, re-enable all buttons
            bool allInactive = true;
            foreach (var button in roleButtons)
            {
                if (button.gameObject.activeSelf)
                {
                    allInactive = false;
                    break;
                }
            }
            if (allInactive)
            {
                foreach (var button in roleButtons)
                {
                    button.gameObject.SetActive(true);
                }
            }
            
            roleSelectScreen.SetActive(false); // Hide role select screen
            if (deckManager != null)
            {
                deckManager.SpawnNewCard(true); // Spawn a new card
            }
        }
        shutUp = false;
        UpdateDayCounter(); // Update the day counter after selecting a role
    }

    private void UpdateDayCounter() // Modify this method to update the day counter with the role name
    {
        if (dayCounterText != null)
        {
            string roleName = roleSelector != null ? roleSelector.selectedRole.ToString() : "Unknown";
            dayCounterText.text = $"Role: {roleName}     Months: {months}     Days: {days}";
        }
        else
        {
            Debug.LogError("DayCounterText object not assigned.");
        }
    }
}
