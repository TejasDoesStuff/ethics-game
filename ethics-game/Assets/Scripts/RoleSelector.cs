using TMPro;
using UnityEngine;
using UnityEngine.UI; // Add this line to include the UI namespace
using CodeMonkey.HealthSystemCM;

public class RoleSelector : MonoBehaviour
{
    public enum Role
    {
        Kant,
        Mill,
        Glaucon,
        TheJudge,
        Freebie
    }

    public Role selectedRole;
    public GameObject[] healthBars; // Array of health bars
    public TextMeshProUGUI[] textObjects; // Array of text objects

    void Start()
    {
        SelectRole(Role.Freebie);
        ApplyRoleAttributes();
    }

    void SelectRole()
    {
        // Randomly select a role
        selectedRole = (Role)Random.Range(0, System.Enum.GetValues(typeof(Role)).Length);
        Debug.Log("Selected Role: " + selectedRole);
        ApplyRoleAttributes();
    }

    // New method to select a role with a parameter
    public void SelectRole(Role role)
    {
        selectedRole = role;
        Debug.Log("Selected Role: " + selectedRole);
        ApplyRoleAttributes();
    }

    void ApplyRoleAttributes()
    {
        // Deactivate all health bars and text objects initially
        foreach (var healthBar in healthBars)
        {
            healthBar.SetActive(true);
        }
        foreach (var textObject in textObjects)
        {
            textObject.gameObject.SetActive(true);
        }

        switch (selectedRole)
        {
            case Role.Kant:
                // Virtue ethics: see personal virtue, not general happiness, "luckier"
                healthBars[1].SetActive(true); // Personal virtue
                healthBars[2].SetActive(false); // Overall happiness
                healthBars[3].SetActive(true); // Control
                textObjects[1].gameObject.SetActive(true); // Personal virtue text
                // Implement additional logic for "luckier"
                healthBars[0].SetActive(true); // Justice
                textObjects[2].gameObject.SetActive(false); // Overall happiness text
                textObjects[0].gameObject.SetActive(false); // Fairness text
                textObjects[3].gameObject.SetActive(false); // Justice and control text
                break;
            case Role.Mill:
                // Utilitarianism: see overall happiness, not own virtue
                healthBars[2].SetActive(true); // Overall happiness
                textObjects[2].gameObject.SetActive(true); // Overall happiness text
                healthBars[1].SetActive(false); // Personal virtue
                textObjects[1].gameObject.SetActive(false); // Personal virtue text
                textObjects[0].gameObject.SetActive(false); // Fairness text
                textObjects[3].gameObject.SetActive(false); // Justice and control text
                break;
            case Role.Glaucon:
                // Justice ethics: see control, nothing else
                healthBars[3].SetActive(true); // Control
                textObjects[3].gameObject.SetActive(true); // Control text
                healthBars[0].SetActive(false); // Justice
                textObjects[0].gameObject.SetActive(false); // Justice text
                healthBars[1].SetActive(false); // Personal virtue
                textObjects[1].gameObject.SetActive(false); // Personal virtue text
                healthBars[2].SetActive(false); // Overall happiness
                textObjects[2].gameObject.SetActive(false); // Overall happiness text
                break;
            case Role.TheJudge:
                // Legalism: see justice, not personal happiness
                healthBars[0].SetActive(true); // Justice
                healthBars[1].SetActive(false); // Personal virtue
                textObjects[0].gameObject.SetActive(true); // Justice 
                textObjects[1].gameObject.SetActive(false); // Personal virtue text
                textObjects[2].gameObject.SetActive(false); // Overall happiness text
                textObjects[3].gameObject.SetActive(false); // Control text

                break;
            case Role.Freebie:
                // give all
                foreach (var healthBar in healthBars)
                {
                    healthBar.SetActive(true);
                }
                break;
        }
    }
}
