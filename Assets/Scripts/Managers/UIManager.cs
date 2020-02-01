using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public TMP_Text woodAmountText;
    public TMP_Text mudAmountText;
    public TMP_Text berriesAmountText;
    [SerializeField] private int woodAmount;
    [SerializeField] private int mudAmount;
    [SerializeField] private int berriesAmount;

    public TMP_Text timerText;
    public int totalTimeAllowed;
    public float timer;
    public bool startTimer;

    private void Update()
    {
        if (startTimer)
        {
            timer -= Time.deltaTime;
            DisplayTimeRemaining(timer);
        }
    }

    public void StartRound()
    {
        timer = totalTimeAllowed;
        startTimer = true;
    }

    private void DisplayTimeRemaining(float timer)
    {
        float temp = timer / 60;
        int minutes = Mathf.FloorToInt(temp);
        temp = timer - (60 * minutes);
        int seconds = Mathf.FloorToInt(temp);
        string time = minutes.ToString() + ":" + seconds.ToString("00");
        timerText.text = time;
    }

    public void UpdateResources(Resource r)
    {
        switch (r.resource)
        {
            case TypeResource.Wood:
                woodAmount += r.amount;
                break;
            case TypeResource.Berries:
                berriesAmount += r.amount;
                break;
            case TypeResource.Mud:
                mudAmount += r.amount;
                break;
        }
        UpdateResources();
    }

    public void UpdateResources(int wood, int berries, int mud)
    {
        woodAmount = wood;
        berriesAmount = berries;
        mudAmount = mud;
        woodAmountText.text = woodAmount.ToString("000");
        berriesAmountText.text = berriesAmount.ToString("000");
        mudAmountText.text = mudAmount.ToString("000");
    }

    public void UpdateResources()
    {
        woodAmountText.text = woodAmount.ToString("000");
        berriesAmountText.text = berriesAmount.ToString("000");
        mudAmountText.text = mudAmount.ToString("000");
    }

    private void Start()
    {
        UpdateResources();
    }
}
