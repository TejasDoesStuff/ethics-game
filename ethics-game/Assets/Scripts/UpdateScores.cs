using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class UpdateScores : MonoBehaviour
{
    private TMP_Text[] textItems;

    int score1 = 0;
    int score2 = 0;
    int score3 = 0;
    int score4 = 0;

    void Awake()
    {
        textItems = GetComponentsInChildren<TMP_Text>();
    }

    public void updateText(int num, string dir)
    {
        int change = (dir == "right") ? 1 : (dir == "left") ? -1 : 0;

        if (num == 1)
        {
            score1 += change;
            textItems[0].text = "Item 1: " + score1;
        }
        else if (num == 2)
        {
            score2 += change;
            textItems[1].text = "Item 2: " + score2;
        }
        else if (num == 3)
        {
            score3 += change;
            textItems[2].text = "Item 3: " + score3;
        }
        else if (num == 4)
        {
            score4 += change;
            textItems[3].text = "Item 4: " + score4;
        }
    }
}
