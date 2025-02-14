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
    

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        canvas = GameObject.FindWithTag("UI");
    }

    void Update()
    {
        if (isDragging)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, startPos.y, transform.position.z);
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

        // if card has moved passed the 'threshold' and the mouse is let go the card moves
        float swipeDistance = transform.position.x - startPos.x;
        if (Mathf.Abs(swipeDistance) > threshold)
        {
            string direction = swipeDistance > 0 ? "right" : "left";
            StartCoroutine(SwipeAndFadeOut(direction));
        }
        else
        {
            transform.position = startPos;
        }
    }

    private IEnumerator SwipeAndFadeOut(string direction)
    {
        float targetX = direction == "right" ? 20f : -20f;

        // move card
        while (Mathf.Abs(transform.position.x - targetX) > 0.1f)
        {
            transform.position = new Vector3(
                Mathf.MoveTowards(transform.position.x, targetX, Time.deltaTime * moveSpeed),
                transform.position.y,
                transform.position.z
            );
            yield return null;
        }

        // fade out
        float alpha = originalColor.a;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Clamp01(alpha));
            yield return null;
        }

        CardProperties cardScript = GetComponent<CardProperties>();
        UpdateScores updateScript = canvas.GetComponent<UpdateScores>();
        if (cardScript != null)
        {
            if (cardScript.cardValue == 1)
            {
                updateScript.updateText(1, direction);
                Debug.Log("1");
            }
            else if (cardScript.cardValue == 2)
            {
                updateScript.updateText(2, direction);
                Debug.Log("2");
            }
            else if (cardScript.cardValue == 3)
            {
                updateScript.updateText(3, direction);
                Debug.Log("3");
            }
            else if (cardScript.cardValue == 4)
            {
                updateScript.updateText(4, direction);
                Debug.Log("4");
            }
        }

        Destroy(gameObject);

        OnSwipe?.Invoke(direction);
    }
}
