using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class StagingArea : MonoBehaviour
{
    public TMP_InputField inputField_01;
    public TMP_InputField inputField_02;
    public Button startButton;
    public Button upgradeButton;

    private bool firstRollGood;
    private bool secondRollGood;

    public Canvas diceRollCanvas;
    public Canvas damCanvas;

    public List<UpgradeDam> upgradeDamList = new List<UpgradeDam>();
    private UpgradeDam currentUpgradeDam;

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
                inputField_01.interactable = false;
                inputField_02.interactable = true;
                inputField_02.Select();
            }
            if (inputField == inputField_02)
            {
                GameManager.Instance.SetSecondDiceRoll(dice);
                inputField_02.interactable = false;
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

    public void WorkButton()
    {
        damCanvas.enabled = false;
        diceRollCanvas.enabled = true;
        startButton.interactable = false;
        inputField_01.Select();
        inputField_02.interactable = false;

        List<UpgradeStat> temp = new List<UpgradeStat>();
        foreach(UpgradeDam ud in upgradeDamList)
        {
            temp.Add(new UpgradeStat(ud.GetComponent<Button>().interactable, ud.isLocked));
        }
        GameManager.Instance.SetUpgradeStatList(temp);
    }

    public void SetUpgrades(List<UpgradeStat> upgradeStats)
    {
        int i = 0;
        foreach(UpgradeDam ud in upgradeDamList)
        {
            ud.GetComponent<Button>().interactable = upgradeStats[i].isInteractable;
            ud.isLocked = upgradeStats[i].isUnlocked;
            i++;
        }
    }

    public void StartDay()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void LoadRequirements(UpgradeDam ud)
    {
        currentUpgradeDam = ud;
        bool b = FindObjectOfType<UIManager>().CheckIfCanUpgrade(ud);
        Debug.Log(b);

        if (b)
            upgradeButton.interactable = true;
        else
            upgradeButton.interactable = false;
    }

    public void UpgradeButton()
    {
        FindObjectOfType<UIManager>().UpgradeDam(currentUpgradeDam);
        currentUpgradeDam.UnlockNextButton();
        upgradeButton.interactable = false;

    }

    private void CheckUpgradeStatus()
    {
        foreach(UpgradeDam ud in upgradeDamList)
        {
            ud.CheckUpgradeStatus();
        }
    }

    private void Start()
    {

        //diceRollCanvas.gameObject.SetActive(false);
        //damCanvas.enabled = true;

        List<UpgradeStat> temp = new List<UpgradeStat>();
        temp = GameManager.Instance.ReturnUpgradeStatList();
        if (temp.Count > 0)
        {
            SetUpgrades(temp);
        }

        damCanvas.enabled = true;
        diceRollCanvas.enabled = false;
        List<UpgradeDam> tempDam = new List<UpgradeDam>();
        foreach(UpgradeDam ud in upgradeDamList)
        {
            if(ud.isLocked)
            {
                ud.gameObject.SetActive(false);
            }
            else
            {
                tempDam.Add(ud);
            }
        }
        foreach(UpgradeDam ud in tempDam)
        {
            ud.nextUnlock.gameObject.SetActive(true);
        }
    }
}

[Serializable]
public class UpgradeStat
{
    public bool isInteractable;
    public bool isUnlocked;

    public UpgradeStat(bool i, bool u)
    {
        isInteractable = i;
        isUnlocked = u;
    }
}
