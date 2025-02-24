using UnityEngine;
using TMPro;

public class CardProperties : MonoBehaviour
{
    public int[] leftScoreChanges;
    public int[] rightScoreChanges;
    public string leftDecisionName;
    public string rightDecisionName;

    private TextMeshProUGUI textBox;
    public string cardText;

    private SpriteRenderer spriteRenderer;

    public Sprite virtueSprite;
    public Sprite justiceSprite;
    public Sprite psychopathSprite;
    public Sprite utilitarianismSprite;

    void Start()
    {
        textBox = GetComponentInChildren<TextMeshProUGUI>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("CardProperties: SpriteRenderer is NULL on GameObject: " + gameObject.name);
        }
        else
        {
            Debug.Log("CardProperties: SpriteRenderer found on " + gameObject.name);
        }

        SetText(cardText);
    }


    public void SetCardScenario(CardScenario scenario)
    {
        if (scenario == null)
        {
            Debug.LogError("SetCardScenario: Scenario is NULL!");
            return;
        }

        Debug.Log("SetCardScenario called for: " + scenario.name);

        cardText = scenario.scenarioText;
        leftScoreChanges = scenario.leftScoreChanges;
        rightScoreChanges = scenario.rightScoreChanges;
        leftDecisionName = scenario.leftDecisionName;
        rightDecisionName = scenario.rightDecisionName;
        SetText(cardText);

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                Debug.LogError("SetCardScenario: spriteRenderer is STILL NULL after reassigning!");
                return;
            }
        }

        Debug.Log("Setting sprite for card type: " + scenario.cardType);
        SetCardSprite(scenario.cardType);
    }


    private void SetCardSprite(string cardType)
    {
        if (spriteRenderer == null)
        {
            Debug.LogError("SetCardSprite: SpriteRenderer is null!");
            return;
        }

        if (string.IsNullOrEmpty(cardType))
        {
            Debug.LogWarning("SetCardSprite: Card type is null or empty.");
            return;
        }

        switch (cardType)
        {
            case "Virtue":
                spriteRenderer.sprite = virtueSprite;
                break;
            case "Justice":
                spriteRenderer.sprite = justiceSprite;
                break;
            case "Psychopath":
                spriteRenderer.sprite = psychopathSprite;
                break;
            case "Utilitarianism":
                spriteRenderer.sprite = utilitarianismSprite;
                break;
            default:
                Debug.LogWarning($"SetCardSprite: No matching sprite found for card type {cardType}.");
                break;
        }
    }

    public void SetText(string newText)
    {
        if (textBox != null)
        {
            textBox.text = newText;
        }
    }
}
