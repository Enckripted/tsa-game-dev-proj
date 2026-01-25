using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialManagerUi : MonoBehaviour
{
    [SerializeField] private int CharsPerSec;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private TextMeshProUGUI spaceContinueText;
    [SerializeField] private GameObject containerObject;
    [SerializeField] private GameObject fadeObject;

    private InputAction continueAction;

    private static int charsPerSec;
    private static bool runningTutorial = false;

    private static List<string> currentMessages;
    private static Action currentCallback;
    private static Typewriter typewriter;

    static void FinishText()
    {
        currentMessages.RemoveAt(0);
        if (currentMessages.Count > 0)
        {
            ExecuteTutorialMessage();
            return;
        }

        GameState.GamePaused = false;
        runningTutorial = false;
        if (currentCallback != null) currentCallback();
    }

    //this is a very bad way of doing this, it would be much better if we could
    //use something like the fluent interface pattern
    //https://dotnettutorials.net/lesson/fluent-interface-design-pattern/ but
    //that is a thing for not 48 hours before the submission deadline
    private static void ExecuteTutorialMessage()
    {
        GameState.GamePaused = true;
        runningTutorial = true;
        typewriter = new Typewriter(currentMessages[0], charsPerSec);
        Debug.Log(runningTutorial);
    }

    public static void DoTutorialMessage(string message, Action callback = null)
    {
        //something has gone bad if we made it here
        if (runningTutorial) throw new Exception("Attempted to run a tutorial while another was running");
        currentMessages = new List<string>() { message };
        currentCallback = callback;
        ExecuteTutorialMessage();
    }

    public static void DoTutorialMessages(List<string> messages, Action callback = null)
    {
        if (runningTutorial) throw new Exception("Attempted to run a tutorial while another was running");
        currentMessages = messages;
        currentCallback = callback;
        ExecuteTutorialMessage();
    }

    void Update()
    {
        containerObject.SetActive(runningTutorial);
        fadeObject.SetActive(runningTutorial);
        //Debug.Log(runningTutorial);
        if (!runningTutorial) return;

        if (continueAction.WasPressedThisFrame())
        {
            if (typewriter.Finished) FinishText();
            else typewriter.ForwardAction();
        }
        typewriter.IncrementTime(Time.deltaTime);

        messageText.text = typewriter.Text;
        spaceContinueText.color = typewriter.Finished ? new Color(0, 0, 0, 1) : new Color(0, 0, 0, 0);
    }

    void Awake()
    {
        //dirty!
        charsPerSec = CharsPerSec;
        continueAction = InputSystem.actions.FindAction("Jump");
    }
}
