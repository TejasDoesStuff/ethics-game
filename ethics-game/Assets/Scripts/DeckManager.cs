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
        SpawnNewCard();
    }

    private void HandleCardSwipe(string direction)
    {
        Debug.Log($"Card swiped {direction}");
        SpawnNewCard();
    }

    private void SpawnNewCard()
    {
        GameObject newCard = Instantiate(cardPrefab, spawnPoint.position, Quaternion.identity);
        newCard.transform.SetParent(transform);
        CardProperties cardScript = newCard.GetComponent<CardProperties>();

        // Choose a random scenario
        CardScenario randomScenario = cardScenarios[Random.Range(0, cardScenarios.Count)];
        Debug.Log("Selected Scenario: " + randomScenario.name);

        // Assign random color
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        newCard.GetComponent<SpriteRenderer>().color = randomColor;

        // Assign random value
        /*        CardProperties cardScript = newCard.GetComponent<CardProperties>();
                if (cardScript != null)
                {
                    cardScript.SetRandomValue();
                }*/

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
