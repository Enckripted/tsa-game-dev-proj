using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialManagerUi : MonoBehaviour
{
    [SerializeField] private int CharsPerSec;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private GameObject containerObject;
    private InputAction continueAction;

    private static int charsPerSec;
    private static Action currentCallback;
    private static bool runningTutorial = false;

    private static Typewriter typewriter;

    static void FinishText()
    {
        GameState.GamePaused = false;
        runningTutorial = false;
        if (currentCallback != null) currentCallback();
    }

    //this is a very bad way of doing this, it would be much better if we could
    //use something like the fluent interface pattern
    //https://dotnettutorials.net/lesson/fluent-interface-design-pattern/ but
    //that is a thing for not 48 hours before the submission deadline
    public static void DoTutorialMessage(string message, Action callback = null)
    {
        //something has gone bad if we made it here
        if (runningTutorial) throw new Exception("Attempted to run a tutorial while another was running");

        GameState.GamePaused = true;
        runningTutorial = true;
        currentCallback = callback;
        typewriter = new Typewriter(message, charsPerSec);
    }

    void Update()
    {
        containerObject.SetActive(runningTutorial);
        if (!runningTutorial) return;

        if (continueAction.WasPressedThisFrame())
        {
            if (typewriter.Finished) FinishText();
            else typewriter.ForwardAction();
        }
        typewriter.IncrementTime(Time.deltaTime);

        messageText.text = typewriter.Text;
    }

    void Awake()
    {
        //dirty!
        charsPerSec = CharsPerSec;
        continueAction = InputSystem.actions.FindAction("Jump");
    }
}
