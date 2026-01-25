using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class IntroTextManager : MonoBehaviour
{
    public List<IntroTextItem> IntroTexts;
    public TextMeshProUGUI TextElement;
    public GameObject SpaceToContinueText;
    public double CharsPerSec;
    public double CommaTimeMult;
    public double PeriodTimeMult;

    private int curText = -1;

    private Typewriter typewriter;
    private InputAction forwardAction;

    void NextText()
    {
        curText++;
        if (curText == IntroTexts.Count)
        {
            SceneManager.LoadScene("MAIN SCENE");
            return;
        }

        typewriter = new Typewriter(IntroTexts[curText].Text, CharsPerSec, CommaTimeMult, PeriodTimeMult);
    }

    void Start()
    {
        //so... apparently unity will automatically enable all input actions
        //when entering a scene from play mode, but won't if you enter a scene
        //from loading it in. we have to do this .Enable() call otherwise the
        //intro just wont work on a game over. FIXME and find a better solution
        //than just putting this line of code here if possible
        InputSystem.actions.Enable();
        forwardAction = InputSystem.actions.FindAction("Jump");

        NextText();
    }

    void Update()
    {
        typewriter.IncrementTime(Time.deltaTime);

        if (forwardAction.WasPressedThisFrame())
        {
            if (typewriter.Finished) NextText();
            else typewriter.ForwardAction();
        }

        TextElement.text = typewriter.Text;
        SpaceToContinueText.SetActive(typewriter.Finished);
    }
}
