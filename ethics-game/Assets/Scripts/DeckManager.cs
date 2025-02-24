using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform spawnPoint;
    public List<CardScenario> cardScenarios;
    public UpdateScores updateScript;
    private void Start()
    {
        SwipeCard.OnSwipe += HandleCardSwipe;
        LoadScenarios(); // Add this line to load scenarios from the folder
        if (cardScenarios.Count > 0)
        {
            SpawnNewCard();
        }
        else
        {
            Debug.LogError("No card scenarios available to spawn.");
        }
    }

    private void HandleCardSwipe(string direction)
    {
        Debug.Log($"Card swiped {direction}");
        SpawnNewCard();
    }

    private void LoadScenarios() // Add this method to load scenarios from the folder
    {
        cardScenarios = new List<CardScenario>(Resources.LoadAll<CardScenario>("Scenarios"));
    }

    public void SpawnNewCard(bool forceSpawn = false) // Add an optional parameter to force spawn a new card
    {
        if (cardScenarios.Count == 0)
        {
            Debug.LogError("No card scenarios available to spawn.");
            return;
        }

        if (!forceSpawn && updateScript.shutUp)
        {
            Debug.Log("Game over. No new cards will be spawned.");
            return;
        }

        GameObject newCard = Instantiate(cardPrefab, spawnPoint.position, Quaternion.identity);
        newCard.transform.SetParent(transform);
        CardProperties cardScript = newCard.GetComponent<CardProperties>();

        // Choose a random scenario
        CardScenario randomScenario = cardScenarios[Random.Range(0, cardScenarios.Count)];
        Debug.Log("Selected Scenario: " + randomScenario.name);

        // Assign random shade of dark brown
        float randomValue = Random.Range(0.2f, 0.4f); // Dark brown shades
        Color randomDarkBrown = new Color(randomValue * 0.5f, randomValue * 0.25f, randomValue * 0.1f);
        newCard.GetComponent<SpriteRenderer>().color = randomDarkBrown;

        if (cardScript != null)
        {
            cardScript.SetCardScenario(randomScenario);
        }
    }

    private void OnDestroy()
    {
        SwipeCard.OnSwipe -= HandleCardSwipe;
    }
}
