using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractionPromptUI : MonoBehaviour
{
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private TextMeshProUGUI promptText;

    // hide on start
    //note: this was originally start, which was causing a bug where the first activation would lead to start firing
    //which would just disable the prompt. awake runs regardless of whether a component is disabled
    private void Awake()
    {
        uiPanel.SetActive(false);
    }

    // helper method in PlayerInteraction
    public void Setup(string text)
    {
        promptText.text = text;
        uiPanel.SetActive(true);
    }

    // close it
    public void Close()
    {
        uiPanel.SetActive(false);
    }
}