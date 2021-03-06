﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text woodAmountText;
    public TMP_Text mudAmountText;
    public TMP_Text berriesAmountText;
    public TMP_Text woodReqText;
    public TMP_Text berriesReqText;
    public TMP_Text mudReqText;
    public Image UIBanner;
    [SerializeField] private int woodAmount;
    [SerializeField] private int mudAmount;
    [SerializeField] private int berriesAmount;

    public TMP_Text timerText;
    public int totalTimeAllowed;
    public float timer;
    public bool startTimer;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private Vector3 playerPos;
    [SerializeField] float dist;
    [SerializeField] bool isTop;
    public AudioClip tick;
    public AudioClip horn;
    private AudioSource audioSource;
    private float tickTimer;
    private int tickCounter;

    public GameObject pausePanel;
    private bool isPaused;
    public GameObject endState;

    private void Update()
    {
        if (startTimer)
        {
            timer -= Time.deltaTime;
            DisplayTimeRemaining(timer);
            if(timer < 0)
            {
                GameManager.Instance.EndDay();
                startTimer = false;
            }

            playerPos = Camera.main.WorldToScreenPoint( new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z) );

            dist = playerPos.y / Screen.height;
            if(isTop)
            {
                if(dist > 0.7f)
                {
                    FlipUI();
                    isTop = false;
                }
            }
            else
            {
                if(dist < 0.3f)
                {
                    FlipUI();
                    isTop = true;
                }
            }
            if(timer < 60 && timer > 57.5)
            {
                if(tickCounter < 5)
                {
                    tickTimer += Time.deltaTime;
                    if(tickTimer > 0.5)
                    {
                        audioSource.Play();
                        tickTimer = 0;
                        tickCounter++;
                    }
                    if (tickCounter == 5 && timer < 57.5)
                    {
                        tickCounter = 0;
                        tickTimer = 0;
                    }
                }
            }

            if(timer < 11)
            {
                tickTimer += Time.deltaTime;
                if (tickTimer > 1)
                {
                    audioSource.Play();
                    tickTimer = 0;
                }
            }
        }
        
    }

    public void FlipUI()
    {
        float newPos = 1 - (UIBanner.transform.position.y / Screen.height);
        UIBanner.transform.position = new Vector3(UIBanner.transform.position.x, newPos * Screen.height, UIBanner.transform.position.z);
        newPos = 1 - (woodAmountText.transform.position.y / Screen.height);
        woodAmountText.transform.position = new Vector3(woodAmountText.transform.position.x, newPos * Screen.height, woodAmountText.transform.position.z);
        UIBanner.transform.Rotate(0, 0, 180);
    }

    public void StartRound()
    {
        timer = totalTimeAllowed;
        startTimer = true;
        tickCounter = 0;
        GameManager.Instance.StartRound();
    }

    public void EndRound()
    {
        if(!isTop)
        {
            FlipUI();
        }
        audioSource.clip = horn;
        audioSource.Play();
    }

    public void DisplayUpgrade(int wood, int berries, int mud)
    {
        woodReqText.text = "-" + wood.ToString();
        berriesReqText.text = "-" + berries.ToString();
        mudReqText.text = "-" + mud.ToString();
        woodReqText.gameObject.SetActive(true);
        berriesReqText.gameObject.SetActive(true);
        mudReqText.gameObject.SetActive(true);
    }

    public void HideUpgrade()
    {
        woodReqText.gameObject.SetActive(false);
        berriesReqText.gameObject.SetActive(false);
        mudReqText.gameObject.SetActive(false);
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

    public void UpgradeDam(UpgradeDam ud)
    {
        woodAmount -= ud.woodRequired;
        berriesAmount -= ud.berriesRequired;
        mudAmount -= ud.mudRequired;
        HideUpgrade();
        UpdateResources();
    }
    public bool CheckIfCanUpgrade(UpgradeDam ud)
    {
        DisplayUpgrade(ud.woodRequired, ud.berriesRequired, ud.mudRequired);
        bool b = true;
        if (ud.woodRequired > woodAmount)
            b = false;
        if (ud.berriesRequired > berriesAmount)
            b = false;
        if (ud.mudRequired > mudAmount)
            b = false;
        return b;
    }

    public void PauseGame()
    {

        if(!isPaused)
        {
            pausePanel.SetActive(true);
            isPaused = true;
            if (GameManager.Instance.inLevel)
            {
                startTimer = false;
            }
        }
        else
        {
            pausePanel.SetActive(false);
            isPaused = false;
            if (GameManager.Instance.inLevel)
            {
                startTimer = true;
            }
        }
    }

    public void WinState()
    {
        endState.SetActive(true);
        endState.GetComponentInChildren<TMP_Text>().text = "You Win!!";
    }

    public void LoseState()
    {
        endState.SetActive(true);
        endState.GetComponentInChildren<TMP_Text>().text = "You Lose";
    }

    private void Start()
    {
        UpdateResources();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        isTop = true;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = tick;
    }
}
