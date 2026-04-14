using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject gameCanvas;
    [SerializeField] GameObject dialogueCanvas;
    void Start()
    {
        gameCanvas.SetActive(false);
    }
    public void OnStartButton()
    {
        gameCanvas.SetActive(true);
        dialogueCanvas.SetActive(true);
        DialogueManager.Instance.TriggerWhim($"Intro");
        InfectionMeter.Instance.PauseInfection(true);
        gameObject.SetActive(false);
    }
}
