using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalQuota : MonoBehaviour
{
    [SerializeField] private List<Quota> nonEndlessPayments;
    [SerializeField] private double endlessPaymentMultiplier;
    [SerializeField] private float endlessPaymentTime;
    [SerializeField] private float secondsForRedText;

    [SerializeField] private TextMeshProUGUI paymentAmountText;
    [SerializeField] private TextMeshProUGUI paymentTimeText;
    [SerializeField] private TextMeshProUGUI contextText;

    private double paymentAmount;
    private float paymentTimeRemaining;

    private int paymentIndex = 0;

    void GetNextPayment()
    {
        if (nonEndlessPayments.Count == 0) throw new Exception("A list of quotas need to be added for this script to work");

        if (paymentIndex < nonEndlessPayments.Count)
        {
            paymentAmount = nonEndlessPayments[paymentIndex].Payment;
            paymentTimeRemaining = nonEndlessPayments[paymentIndex].Seconds;
        }
        else
        {
            Quota lastQuota = nonEndlessPayments[nonEndlessPayments.Count - 1];
            paymentAmount = lastQuota.Payment * Math.Pow(endlessPaymentMultiplier, paymentIndex - nonEndlessPayments.Count + 1);
            paymentTimeRemaining = endlessPaymentTime;
        }
        paymentIndex++;
    }

    void Start()
    {
        GetNextPayment();
    }

    void Update()
    {
        if (GameState.GamePaused) return;

        paymentTimeRemaining -= Time.deltaTime;
        if (paymentTimeRemaining <= 0)
        {
            if (!Player.HasMoney(paymentAmount))
            {
                SceneManager.LoadScene("INTRO SCENE");
                return;
            }
            Player.RemoveMoney(paymentAmount);
            GetNextPayment();
        }
        paymentAmountText.text = $"${paymentAmount:0.00}";
        paymentTimeText.text = $"Due in {Math.Floor(paymentTimeRemaining / 60)}:{paymentTimeRemaining % 60:00}";

        contextText.color = paymentTimeRemaining > secondsForRedText ? Color.white : Color.red;
        paymentTimeText.color = paymentTimeRemaining > secondsForRedText ? Color.white : Color.red;
    }
}
