using TMPro;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    private TMP_Text[] textItems;

    int score1 = 50;
    int score2 = 50;
    int score3 = 50;
    int score4 = 50;

    void Awake()
    {
        textItems = GetComponentsInChildren<TMP_Text>();
        textItems[0].text = "Justice: " + score1;
        textItems[1].text = "Virtue: " + score2;
        textItems[2].text = "Happiness: " + score3;
        textItems[3].text = "Control: " + score4;
        //add some popup text stuff here later
    }
}
