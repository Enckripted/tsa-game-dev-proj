using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialManagerUi : MonoBehaviour
{
    [SerializeField] private int CharsPerSec;
    [SerializeField] private TextMeshProUGUI messageText;
    private InputAction continueAction;

    private string currentMessage;
    private Action currentCallback;
    private bool runningTutorial = false;

    private int curChar = 0;
    private float timeUntilNextChar = 0;

    void IncrementText()
    {
        if (!runningTutorial) return;

        timeUntilNextChar -= Time.deltaTime;
        if (timeUntilNextChar <= 0)
        {
            if (curChar < currentMessage.Length) curChar++;
            timeUntilNextChar = 1f / CharsPerSec;
        }
    }

    void ForwardText()
    {
        curChar = currentMessage.Length;
    }

    void FinishText()
    {
        GameState.GamePaused = false;
        runningTutorial = false;
        currentCallback();
    }

    public void DoTutorialMessage(string message, Action callback)
    {
        //something has gone bad if we made it here
        if (runningTutorial) throw new Exception("Attempted to run a tutorial while another was running");

        GameState.GamePaused = true;
        runningTutorial = true;
        currentMessage = message;
        currentCallback = callback;
        curChar = 0;
        timeUntilNextChar = 1f / CharsPerSec;
    }

    void Update()
    {
        if (!runningTutorial) return;

        if (continueAction.WasPressedThisFrame())
        {
            if (curChar < currentMessage.Length) ForwardText();
            else FinishText();
        }
        IncrementText();

        messageText.text = currentMessage.Substring(0, curChar + 1) + new string(' ', currentMessage.Length - curChar - 1);
    }

    void Awake()
    {
        continueAction = InputSystem.actions.FindAction("Jump");
    }
}
