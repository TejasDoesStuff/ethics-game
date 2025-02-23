using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform spawnPoint;
    public List<CardScenario> cardScenarios;

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

    private void SpawnNewCard()
    {
        if (cardScenarios.Count == 0)
        {
            Debug.LogError("No card scenarios available to spawn.");
            return;
        }
        GameObject newCard = Instantiate(cardPrefab, spawnPoint.position, Quaternion.identity);
        newCard.transform.SetParent(transform);
        CardProperties cardScript = newCard.GetComponent<CardProperties>();

        // Choose a random scenario
        CardScenario randomScenario = cardScenarios[Random.Range(0, cardScenarios.Count)];
        Debug.Log("Selected Scenario: " + randomScenario.name);

        // Assign random color
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        newCard.GetComponent<SpriteRenderer>().color = randomColor;

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
