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

    private List<string> words = new List<string>();
    private int curWord = 0;
    private int curChar = 0;

    private string writtenText = "";
    private string ShownText
    {
        get
        {
            return writtenText + (curWord < words.Count ? words[curWord].Substring(0, curChar + 1) + new string(' ', words[curWord].Length - curChar - 1) : "");
        }
    }

    private double timeTillNextChar = 0;

    private InputAction forwardAction;

    void NextChar()
    {
        if (curWord >= words.Count) return;
        curChar++;
        if (curChar == words[curWord].Length)
        {
            NextWord();
            return;
        }

        timeTillNextChar = 1 / CharsPerSec;
        if (words[curWord][curChar] == ',') timeTillNextChar *= CommaTimeMult;
        else if (words[curWord][curChar] == '.') timeTillNextChar *= PeriodTimeMult;
    }

    void NextWord()
    {
        if (curWord != -1) writtenText += words[curWord] + " ";
        curWord++;
        if (curWord >= words.Count) return;

        curChar = 0;
        timeTillNextChar = 1 / CharsPerSec;
    }

    void NextText()
    {
        curText++;
        if (curText == IntroTexts.Count)
        {
            SceneManager.LoadScene("MAIN SCENE");
            return;
        }

        //all the setup (ugly)
        words = new List<string>(IntroTexts[curText].Text.Split(" "));
        writtenText = "";
        curWord = -1;

        NextWord();
    }

    void ForwardText()
    {
        while (curWord != words.Count)
            NextWord();
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
        timeTillNextChar = 0;
        NextText();
    }

    void Update()
    {
        if (timeTillNextChar <= 0) NextChar();
        else timeTillNextChar -= Time.deltaTime;

        SpaceToContinueText.SetActive(curWord == words.Count);
        if (forwardAction.WasPressedThisFrame())
        {
            if (curWord != words.Count) ForwardText();
            else NextText();
        }
        TextElement.text = ShownText;
    }
}
