using UnityEngine;
using TMPro;

public class CardProperties : MonoBehaviour
{
    public int cardValue { get; private set; }

    private TextMeshProUGUI textBox;
    public string cardText;

    void Start()
    {
        textBox = GetComponentInChildren<TextMeshProUGUI>();
        cardText = cardValue.ToString();
        SetText(cardText);
    }

    public void SetRandomValue()
    {
        cardValue = Random.Range(1, 5);
    }

    public void SetText(string newText)
    {
        if (textBox != null)
        {
            textBox.text = "This card is " + newText;
        }
    }
}
