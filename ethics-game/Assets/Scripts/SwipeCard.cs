using UnityEngine;
using System.Collections;
using TMPro;

public class SwipeCard : MonoBehaviour
{
    [Header("Dragging")]
    private Vector2 startPos;
    private Vector2 mousePos;
    private bool isDragging = false;
    public float threshold = 2f;
    public float moveSpeed = 10f;

    [Header("Fade Out")]
    public float fadeSpeed = 0.5f;

    [Header("Swipe Texts")]
    private TextMeshProUGUI leftSwipeText;
    private TextMeshProUGUI rightSwipeText;
    private TextMeshProUGUI[] changeTexts; // Change this line to private

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    public delegate void SwipeAction(string direction);
    public static event SwipeAction OnSwipe;

    private GameObject canvas;
    private CardProperties cardScript;
    private UpdateScores updateScript;
    public bool shutUp = false;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        canvas = GameObject.FindWithTag("UI");
        cardScript = GetComponent<CardProperties>();
        updateScript = canvas.GetComponent<UpdateScores>();

        leftSwipeText = GameObject.Find("left")?.GetComponent<TextMeshProUGUI>();
        rightSwipeText = GameObject.Find("right")?.GetComponent<TextMeshProUGUI>();

        if (leftSwipeText == null)
        {
            Debug.Log("Left swipe text object not found. Replacing with 'unimportant'.");
            leftSwipeText = GameObject.Find("unimportant")?.GetComponent<TextMeshProUGUI>();
        }

        if (rightSwipeText == null)
        {
            Debug.Log("Right swipe text object not found. Replacing with 'unimportant'.");
            rightSwipeText = GameObject.Find("unimportant")?.GetComponent<TextMeshProUGUI>();
        }

        if (cardScript == null)
        {
            Debug.Log("CardProperties component not found on the card.");
        }

        if (updateScript == null)
        {
            Debug.Log("UpdateScores component not found on the canvas.");
        }

        if (leftSwipeText != null)
        {
            leftSwipeText.color = new Color(leftSwipeText.color.r, leftSwipeText.color.g, leftSwipeText.color.b, 0);
            HideText(leftSwipeText);
        }

        if (rightSwipeText != null)
        {
            rightSwipeText.color = new Color(rightSwipeText.color.r, rightSwipeText.color.g, rightSwipeText.color.b, 0);
            HideText(rightSwipeText);
        }

        changeTexts = new TextMeshProUGUI[4]; // Initialize the array
        changeTexts[0] = GameObject.Find("c1")?.GetComponent<TextMeshProUGUI>();
        changeTexts[1] = GameObject.Find("c2")?.GetComponent<TextMeshProUGUI>();
        changeTexts[2] = GameObject.Find("c3")?.GetComponent<TextMeshProUGUI>();
        changeTexts[3] = GameObject.Find("c4")?.GetComponent<TextMeshProUGUI>();

        for (int i = 0; i < changeTexts.Length; i++)
        {
            if (changeTexts[i] == null)
            {
                Debug.Log($"Change text object c{i + 1} not found. Replacing with 'unimportant'.");
                changeTexts[i] = GameObject.Find("unimportant")?.GetComponent<TextMeshProUGUI>();
            }
        }

        if (changeTexts != null && changeTexts.Length == 4)
        {
            foreach (var text in changeTexts)
            {
                if (text != null)
                {
                    text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
                }
            }
        }
    }

    void Update()
    {
        if (shutUp) {
            return;
        }
        else if (isDragging)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, startPos.y, transform.position.z);

            float swipeDistance = transform.position.x - startPos.x;
            if (swipeDistance > 0.5f)
            {   
                /*Debug.Log("attempting to show swipe values right");*/
                ShowSwipeValues("right");
            }
            else if (swipeDistance < -0.5f)
            {
                /*Debug.Log("attempting to show swipe values left");*/
                ShowSwipeValues("left");
            }
            else
            {
                /*Debug.Log("resetting swipe texts");*/
                ResetSwipeTexts();
            }
        }
        else
        {
            /*Debug.Log("resetting swipe texts, not dragging");*/   
            ResetSwipeTexts(); // Ensure texts are reset when not dragging
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
            /*Debug.Log("Resetting card position, mouse up");*/
            transform.position = startPos;
            ResetSwipeTexts();
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
            DisplayChanges(changes);
            Debug.Log($"Swiped {direction} with changes: {string.Join(", ", changes)}");
        }

        Destroy(gameObject);
        OnSwipe?.Invoke(direction);
    }

    private void ShowText(TextMeshProUGUI text)
    {
        Color color = text.color;
        color.a = 1;
        text.color = color;
    }

    private void HideText(TextMeshProUGUI text)
    {
        /*Debug.Log("Hiding " + text.name);*/
        Color color = text.color;
        color.a = 0;
        text.color = color;
    }

    private void ShowSwipeValues(string direction)
    {
        if (cardScript != null)
        {
            int[] values = direction == "right" ? cardScript.rightScoreChanges : cardScript.leftScoreChanges;
            string decisionName = direction == "right" ? cardScript.rightDecisionName : cardScript.leftDecisionName;
            string cardText = cardScript.cardText;

            // Provide placeholders if any variable is null
            values = values ?? new int[4];
            decisionName = direction == "right" ? (decisionName ?? "Disagree") : (decisionName ?? "Agree");
            cardText = cardText ?? "Card Text";

            string newText = $"{decisionName}";

            if (direction == "right")
            {
                rightSwipeText.text = newText;
                ShowText(rightSwipeText);
                HideText(leftSwipeText);
            }
            else
            {
                leftSwipeText.text = newText;
                ShowText(leftSwipeText);
                HideText(rightSwipeText);
            }

            DisplayChanges(values); // Add this line to display changes
        }
        else
        {
            HideText(leftSwipeText);
            HideText(rightSwipeText);
        }
    }

    private void DisplayChanges(int[] changes) // Add this method to display changes
    {
        if (changeTexts != null && changeTexts.Length == 4)
        {
            for (int i = 0; i < changes.Length; i++)
            {
                if (changeTexts[i] != null)
                {
                    changeTexts[i].text = changes[i] > 0 ? $"+{changes[i]}" : $"{changes[i]}";
                    ShowText(changeTexts[i]);
                }
            }
        }
    }

    private void ResetSwipeTexts()
    {
        /*Debug.Log("Resetting swipe texts");*/
        if (leftSwipeText != null)
        {
            HideText(leftSwipeText);
        }
        if (rightSwipeText != null)
        {
            HideText(rightSwipeText);
        }
        if (changeTexts != null && changeTexts.Length == 4)
        {
            foreach (var text in changeTexts)
            {
                if (text != null)
                {
                    HideText(text);
                }
            }
        }
    }

    private void OnDestroy()
    {
        shutUp = true;
    }
}