using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeDam : MonoBehaviour
{
    public int woodRequired;
    public int berriesRequired;
    public int mudRequired;

    public bool isLocked;
    public Button nextUnlock;
    private Button thisButton;
    public Image builtImage;
    public int quadrent;
    

    private void Start()
    {
        thisButton = GetComponent<Button>();
        CheckUpgradeStatus();
        builtImage.sprite = GetComponent<Image>().sprite;
    }

    public void UnlockNextButton()
    {
        if (nextUnlock != null)
        {
            GetComponent<Image>().enabled = false;
            builtImage.enabled = true;
            nextUnlock.GetComponent<UpgradeDam>().ActivatingNextButton();
            nextUnlock.GetComponent<UpgradeDam>().isLocked = false;
            nextUnlock.interactable = true;
            thisButton.interactable = false;
        }
        else
        {
            GameManager.Instance.FinishQuadrant(quadrent);
        }
    }

    public void ActivatingNextButton()
    {
        nextUnlock.gameObject.SetActive(true);
    }

    public void DisplayUpgrade()
    {
        FindObjectOfType<StagingArea>().LoadRequirements(this);
    }

    public void CheckUpgradeStatus()
    {
        if (isLocked)
        {
            thisButton.interactable = false;
        }
    }
}
