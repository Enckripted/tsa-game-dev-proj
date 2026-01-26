using System;
using TMPro;
using UnityEngine;

public class DayBeginScren : MonoBehaviour
{
    [SerializeField] private float secondsForFadeIn;
    [SerializeField] private float secondsForMoneyTally;
    [SerializeField] private float secondsForWait;
    [SerializeField] private float secondsForFadeOut;
    private float AnimationTime
    {
        get
        {
            return secondsForFadeIn + secondsForMoneyTally + secondsForWait + secondsForFadeOut;
        }
    }

    private int dayNumber;
    private double quotaAmount;
    private float timeSpent;

    public bool DoingAnimation;

    [SerializeField] private TextMeshProUGUI dayNumberText;
    [SerializeField] private TextMeshProUGUI amountText;
    private CanvasGroup canvasGroup;

    private void UpdateFadeOut()
    {
        if (timeSpent > AnimationTime)
        {
            FinishFadeOut();
            return;
        }
        else if (timeSpent > AnimationTime - secondsForFadeOut)
        {
            canvasGroup.alpha = 1 - (timeSpent - secondsForFadeIn - secondsForMoneyTally - secondsForWait) / secondsForFadeOut;
            amountText.text = $"${quotaAmount:0.00}";
        }
        else if (timeSpent > secondsForFadeIn)
        {
            canvasGroup.alpha = 1;
            amountText.text = $"${(quotaAmount * Math.Min(timeSpent - secondsForFadeIn, secondsForMoneyTally) / secondsForMoneyTally):0.00}";
        }
        else
        {
            canvasGroup.alpha = timeSpent / secondsForFadeIn;
            amountText.text = "$0.00";
        }
    }

    private void FinishFadeOut()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        DoingAnimation = false;

        GameState.GamePaused = false;
    }

    public void DoFadeOutWith(int dayNumber, double quotaAmount)
    {
        this.dayNumber = dayNumber;
        this.quotaAmount = quotaAmount;
        timeSpent = 0;
        DoingAnimation = true;

        dayNumberText.text = "Day " + dayNumber;
        canvasGroup.blocksRaycasts = true;
        GameState.GamePaused = true;
    }

    void Update()
    {
        timeSpent += Time.deltaTime;
        if (DoingAnimation) UpdateFadeOut();
    }

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        DoingAnimation = false;
    }
}
