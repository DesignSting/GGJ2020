﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StagingArea : MonoBehaviour
{
    public TMP_InputField inputField_01;
    public TMP_InputField inputField_02;
    public Button startButton;

    private bool firstRollGood;
    private bool secondRollGood;

    public Canvas diceRollCanvas;
    public Canvas damCanvas;

    public void CheckDiceInput(TMP_InputField inputField)
    {
        string s = inputField.text;
        int dice = int.Parse(s);
        if (dice > 0 && dice <= 20)
        {
            if (inputField == inputField_01)
            {
                GameManager.Instance.SetFirstDiceRoll(dice);
                firstRollGood = true;
                inputField_02.interactable = true;
                inputField_02.Select();
            }
            if (inputField == inputField_02)
            {
                GameManager.Instance.SetSecondDiceRoll(dice);
                secondRollGood = true;
                startButton.Select();
            }
            inputField.text = "SUBMITTED";
            inputField.interactable = false;

        }
        else
        {
            inputField.text = "INVALID";
        }
        CheckStartButton();
    }

    public void CheckStartButton()
    {
        if (firstRollGood && secondRollGood)
        {
            startButton.interactable = true;
        }
        else
        {
            startButton.interactable = false;
        }
    }

    public void StartDay()
    {
        SceneManager.LoadScene("MainScene");
    }

    private void Start()
    {

        //diceRollCanvas.gameObject.SetActive(false);
        //damCanvas.enabled = true;
    }
}