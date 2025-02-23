using UnityEngine;

[CreateAssetMenu(fileName = "CardScenario", menuName = "Scriptable Objects/CardScenario")]
public class CardScenario : ScriptableObject
{
    public string scenarioText;
    public int[] leftScoreChanges = new int[4];
    public int[] rightScoreChanges = new int[4];
}
