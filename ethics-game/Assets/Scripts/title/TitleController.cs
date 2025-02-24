using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    public GameObject instructions;
    public Button instructionsOpenButton;
    public Button instructionsCloseButton;
    public Button startButton;

    void Start()
    {
        instructions.SetActive(false);

        instructionsOpenButton.onClick.AddListener(OpenInstructions);
        instructionsCloseButton.onClick.AddListener(CloseInstructions);
        startButton.onClick.AddListener(StartGame);
    }

    void OpenInstructions()
    {
        instructions.SetActive(true);
    }

    void CloseInstructions()
    {
        instructions.SetActive(false);
    }

    void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}