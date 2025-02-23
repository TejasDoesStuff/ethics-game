using TMPro;
using UnityEngine;

public class UpdateScores : MonoBehaviour
{
    private TMP_Text[] textItems;

    public int score1 = 50;
    public int score2 = 50;
    public int score3 = 50;
    public int score4 = 50;

    public int maxThreshold = 80;

    void Awake()
    {
        textItems = GetComponentsInChildren<TMP_Text>();
    }

    public void updateMultiple(int[] changes)
    {
        score1 += changes[0];
        score2 += changes[1];
        score3 += changes[2];
        score4 += changes[3];

        score1 = Mathf.Clamp(score1, 0, 100);
        score2 = Mathf.Clamp(score2, 0, 100);
        score3 = Mathf.Clamp(score3, 0, 100);
        score4 = Mathf.Clamp(score4, 0, 100);

        bool anyAboveThreshold = score1 >= maxThreshold || score2 >= maxThreshold || score3 >= maxThreshold || score4 >= maxThreshold;

        textItems[0].text = anyAboveThreshold && score1 < maxThreshold ? "" : "Justice: " + score1;
        textItems[1].text = anyAboveThreshold && score2 < maxThreshold ? "" : "Virtue: " + score2;
        textItems[2].text = anyAboveThreshold && score3 < maxThreshold ? "" : "Happiness: " + score3;
        textItems[3].text = anyAboveThreshold && score4 < maxThreshold ? "" : "Control: " + score4;

        CheckGameOver();
    }

    public bool ShouldHideValues()
    {
        return score1 >= maxThreshold || score2 >= maxThreshold || score3 >= maxThreshold || score4 >= maxThreshold;
    }

    private void CheckGameOver()
    {
        int[] scores = { score1, score2, score3, score4 };
        string[] scoreNames = { "Justice", "Virtue", "Happiness", "Control" };

        for (int i = 0; i < scores.Length; i++)
        {
            if (scores[i] == 100)
            {
                Debug.Log($"Game Over: {scoreNames[i]} reached 100!");
            }
            else if (scores[i] == 0)
            {
                Debug.Log($"Game Over: {scoreNames[i]} reached 0!");
            }
        }
    }
}
