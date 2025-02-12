using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform spawnPoint;

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

        Color randomColor = new Color(Random.value, Random.value, Random.value);
        newCard.GetComponent<SpriteRenderer>().color = randomColor;
    }

    private void OnDestroy()
    {
        SwipeCard.OnSwipe -= HandleCardSwipe;
    }
}
