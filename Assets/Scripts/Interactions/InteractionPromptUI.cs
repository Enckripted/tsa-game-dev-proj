using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractionPromptUI : MonoBehaviour
{
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private Image progressBar;
    [SerializeField] private TextMeshProUGUI promptText;

    // hide on start
    private void Start()
    {
        uiPanel.SetActive(false);
    }

    // helper method in PlayerInteraction
    public void Setup(string text)
    {
        promptText.text = text;
        uiPanel.SetActive(true);
        progressBar.fillAmount = 0;
    }

    // update that bih
    public void UpdateProgress(float progress)
    {
        progressBar.fillAmount = progress;
    }

    // close it
    public void Close()
    {
        uiPanel.SetActive(false);
    }
}