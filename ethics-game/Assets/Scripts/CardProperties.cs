using UnityEngine;
using TMPro;

public class CardProperties : MonoBehaviour
{
    // Remove single cardValue and add arrays for left and right swipe changes
    public int[] leftScoreChanges;
    public int[] rightScoreChanges;

    private TextMeshProUGUI textBox;
    public string cardText;

    void Start()
    {
        textBox = GetComponentInChildren<TextMeshProUGUI>();
        // Optionally display the score arrays on the card
        /*cardText = "Left: [" + string.Join(", ", leftScoreChanges) + "] Right: [" + string.Join(", ", rightScoreChanges) + "]";*/
        SetText(cardText);
    }

    /*public void SetRandomValue()
    {
        // Create arrays for 4 score values each
        leftScoreChanges = new int[4];
        rightScoreChanges = new int[4];
        for (int i = 0; i < 4; i++)
        {
            // For left swipes, random negative values (or zero)
            leftScoreChanges[i] = Random.Range(-10, 1);
            // For right swipes, random positive values (or zero)
            rightScoreChanges[i] = Random.Range(0, 11);
        }
    }*/

    public void SetCardScenario(CardScenario scenario)
    {
        cardText = scenario.scenarioText;
        leftScoreChanges = scenario.leftScoreChanges;
        rightScoreChanges = scenario.rightScoreChanges;
        SetText(cardText);
    }

    public void SetText(string newText)
    {
        if (textBox != null)
        {
            textBox.text = "This card has:\n" + newText;
        }
    }
}
