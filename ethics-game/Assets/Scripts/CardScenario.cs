using UnityEngine;

[CreateAssetMenu(fileName = "CardScenario", menuName = "Scriptable Objects/CardScenario")]
public class CardScenario : ScriptableObject
{
    public string scenarioText;
    public int[] leftScoreChanges = new int[4];
    public int[] rightScoreChanges = new int[4];
    public string leftDecisionName;
    public string rightDecisionName;

    public static CardScenario[] GetDefaultScenarios()
    {
        return new CardScenario[]
        {
            new CardScenario
            {
                scenarioText = "Your friend Tejas Panja lied to you about what he did last night...",
                leftScoreChanges = new int[] { -10, 10, 0, 0 },
                rightScoreChanges = new int[] { 10, 0, 0, 10 },
                leftDecisionName = "Forgive",
                rightDecisionName = "Punish"
            },
            new CardScenario
            {
                scenarioText = "You see some people are making fun of a homeless man, you can choose to stand up to them, but it could put you in danger.",
                leftScoreChanges = new int[] { 0, 0, 10, 10 },
                rightScoreChanges = new int[] { -10, 0, 0, 0 },
                leftDecisionName = "Stand Up",
                rightDecisionName = "Ignore"
            },
            new CardScenario
            {
                scenarioText = "A local charity requires some financial aid; you can donate to this cause but it will take out a part of your salary.",
                leftScoreChanges = new int[] { 0, 10, -10, 0 },
                rightScoreChanges = new int[] { -10, 0, 0, 0 },
                leftDecisionName = "Donate",
                rightDecisionName = "Decline"
            },
            new CardScenario
            {
                scenarioText = "You see a homeless man looking for a job, while he had done some bad actions in the past (gambling) he says has become better and wishes for a better chance. You can choose to hire him or deny this request.",
                leftScoreChanges = new int[] { 20, 10, 0, 0 },
                rightScoreChanges = new int[] { 0, 0, 10, 0 },
                leftDecisionName = "Hire",
                rightDecisionName = "Deny"
            },
            new CardScenario
            {
                scenarioText = "A foreign country's airplane is malfunctioning in your airspace. They are requesting to land on a nearby military airstrip.",
                leftScoreChanges = new int[] { 10, 0, -10, 0 },
                rightScoreChanges = new int[] { -10, 0, 10, 0 },
                leftDecisionName = "Allow",
                rightDecisionName = "Deny"
            },
            new CardScenario
            {
                scenarioText = "A homeless man steals a loaf of bread to feed his family, will you decide to turn punish him for his crimes or let him go.",
                leftScoreChanges = new int[] { 0, 0, -20, 10 },
                rightScoreChanges = new int[] { 10, 10, 0, 0 },
                leftDecisionName = "Punish",
                rightDecisionName = "Forgive"
            },
            new CardScenario
            {
                scenarioText = "A person creates graffiti on a famous statue of Tejas Panja, he seems sorry about the action, but the damage is done, and it will cost the city and in turn taxpayers.",
                leftScoreChanges = new int[] { 0, 10, 0, 10 },
                rightScoreChanges = new int[] { -10, -10, 0, 0 },
                leftDecisionName = "Punish",
                rightDecisionName = "Forgive"
            },
            new CardScenario
            {
                scenarioText = "A homeless family can’t pay their rent and say 'please speed I need this my mom's kind homeless.'",
                leftScoreChanges = new int[] { 0, 0, 0, 0 },
                rightScoreChanges = new int[] { 0, 10, 0, 0 },
                leftDecisionName = "Help",
                rightDecisionName = "Ignore"
            },
            new CardScenario
            {
                scenarioText = "Inesh, a very powerful police officer, breaks the law by not recycling his water bottle, Inesh is trying to use his power to get away with the crime.",
                leftScoreChanges = new int[] { 10, 0, 0, 10 },
                rightScoreChanges = new int[] { -10, -10, 0, 0 },
                leftDecisionName = "Punish",
                rightDecisionName = "Ignore"
            },
            new CardScenario
            {
                scenarioText = "You have the power to create a factory to mass produce fondu! This will increase the jobs for many people but will pollute the local environment.",
                leftScoreChanges = new int[] { -10, 0, -10, 10 },
                rightScoreChanges = new int[] { 10, 0, 0, 0 },
                leftDecisionName = "Create",
                rightDecisionName = "Decline"
            },
            new CardScenario
            {
                scenarioText = "You can create free food for all, but it would cut into the rich/middle classes income but would satisfy a larger number of individuals.",
                leftScoreChanges = new int[] { 10, 0, -10, 0 },
                rightScoreChanges = new int[] { -10, 0, 10, 0 },
                leftDecisionName = "Create",
                rightDecisionName = "Decline"
            },
            new CardScenario
            {
                scenarioText = "A farmer named Tonald Dump has a chicken farm, he is one of the larger chicken producers within your city, but his farm has contracted the famous bird flu.",
                leftScoreChanges = new int[] { -10, 0, 0, 0 },
                rightScoreChanges = new int[] { 10, 0, 0, 0 },
                leftDecisionName = "Help",
                rightDecisionName = "Ignore"
            },
            new CardScenario
            {
                scenarioText = "A rich man who has contributed much to your government is making it so that other individuals such as the middle or lower class are having to pay a much higher fee to survive.",
                leftScoreChanges = new int[] { 10, 10, -10, 0 },
                rightScoreChanges = new int[] { -10, -10, 0, 10 },
                leftDecisionName = "Punish",
                rightDecisionName = "Ignore"
            },
        };
    }
}
