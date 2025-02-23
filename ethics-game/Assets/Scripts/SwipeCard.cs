using UnityEngine;
using System.Collections;

public class SwipeCard : MonoBehaviour
{
    [Header("Dragging")]
    private Vector2 startPos;
    private Vector2 mousePos;
    private bool isDragging = false;
    public float threshold = 2f;
    public float moveSpeed = 10f;

    [Header("Fade Out")]
    public float fadeSpeed = 1f;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    public delegate void SwipeAction(string direction);
    public static event SwipeAction OnSwipe;

    private GameObject canvas;
    private CardProperties cardScript;
    private UpdateScores updateScript;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        canvas = GameObject.FindWithTag("UI");
        cardScript = GetComponent<CardProperties>();
        updateScript = canvas.GetComponent<UpdateScores>();

        if (cardScript == null)
        {
            Debug.LogError("CardProperties component not found on the card.");
        }

        if (updateScript == null)
        {
            Debug.LogError("UpdateScores component not found on the canvas.");
        }
    }

    void Update()
    {
        if (isDragging)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, startPos.y, transform.position.z);

            float swipeDistance = transform.position.x - startPos.x;
            if (swipeDistance > 0.5f)
            {
                ShowSwipeValues("right");
            }
            else if (swipeDistance < -0.5f)
            {
                ShowSwipeValues("left");
            }
            else
            {
                ResetCardText();
            }
        }
    }

    private void OnMouseDown()
    {
        startPos = transform.position;
        isDragging = true;
    }

    private void OnMouseUp()
    {
        if (!isDragging) return;
        isDragging = false;

        float swipeDistance = transform.position.x - startPos.x;
        if (Mathf.Abs(swipeDistance) > threshold)
        {
            string direction = swipeDistance > 0 ? "right" : "left";
            StartCoroutine(SwipeAndFadeOut(direction));
        }
        else
        {
            transform.position = startPos;
            ResetCardText();
        }
    }

    private IEnumerator SwipeAndFadeOut(string direction)
    {
        float targetX = direction == "right" ? 20f : -20f;

        while (Mathf.Abs(transform.position.x - targetX) > 0.1f)
        {
            transform.position = new Vector3(
                Mathf.MoveTowards(transform.position.x, targetX, Time.deltaTime * moveSpeed),
                transform.position.y,
                transform.position.z
            );
            yield return null;
        }

        float alpha = originalColor.a;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Clamp01(alpha));
            yield return null;
        }

        if (cardScript != null)
        {
            int[] changes = direction == "right" ? cardScript.rightScoreChanges : cardScript.leftScoreChanges;
            updateScript.updateMultiple(changes);
            Debug.Log($"Swiped {direction} with changes: {string.Join(", ", changes)}");
        }

        Destroy(gameObject);
        OnSwipe?.Invoke(direction);
    }

    private void ShowSwipeValues(string direction)
    {
        if (cardScript != null && !updateScript.ShouldHideValues())
        {
            int[] values = direction == "right" ? cardScript.rightScoreChanges : cardScript.leftScoreChanges;
            string decisionName = direction == "right" ? cardScript.rightDecisionName : cardScript.leftDecisionName;
            string cardText = cardScript.cardText;

            // Provide placeholders if any variable is null
            values = values ?? new int[4];
            decisionName = direction == "right" ? (decisionName ?? "disagree") : (decisionName ?? "agree");
            cardText = cardText ?? "Card Text";

            string newText = $"{cardText}\n{decisionName.ToUpper()} Changes: [{string.Join(", ", values)}]";
            cardScript.SetText(newText);
        }
    }

    private void ResetCardText()
    {
        if (cardScript != null)
        {
            cardScript.SetText(cardScript.cardText);
        }
    }
}