﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    public static AreaManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    public List<Area> areaList = new List<Area>();
    public List<Area> useableAreas = new List<Area>();
    private Weather currentWeather;
    private int theFirstDiceRoll;
    private int theSecondDiceRoll;
    [SerializeField] private Area previousArea;
    [SerializeField] private Area currentArea;

    [Space(15)]
    public Sprite sunBackground;
    public Sprite wetBackground;
    public Sprite snowBackground;

    [Space(15)]
    public AudioClip sunnyMusic;
    public AudioClip wetMusic;
    public AudioClip snowMusic;

    public void RecieveNewDay(Weather weather, int diceRoll)
    {
        currentWeather = weather;
        Debug.Log("Weather: " + weather + "\nDice Roll: " + diceRoll);
        
    }

    private void PopulateList(int i)
    {
        Debug.Log(i);
        useableAreas.Clear();
        foreach(Area a in areaList)
        {
            a.gameObject.SetActive(true);
            if(a.northArrowSprite != null)
                a.northArrowSprite.gameObject.SetActive(false);
            if (a.eastArrowSprite != null)
                a.eastArrowSprite.gameObject.SetActive(false);
            if (a.southArrowSprite != null)
                a.southArrowSprite.gameObject.SetActive(false);
            if (a.westArrowSprite != null)
                a.westArrowSprite.gameObject.SetActive(false);
            a.isUsed = false;
            a.ResetResources();
            a.gameObject.SetActive(false);
        }

        if(i == 20 || i == 10 || i == 13|| i == 8 || i == 7)
        {
            Debug.Log("First: 20, 10, 13, 8, 7");
            useableAreas.Add(areaList[2]);
            useableAreas.Add(areaList[6]);
            useableAreas.Add(areaList[10]);
            useableAreas.Add(areaList[14]);
            useableAreas.Add(areaList[15]);
            useableAreas.Add(areaList[1]);
            useableAreas.Add(areaList[5]);
            useableAreas.Add(areaList[4]);
            useableAreas.Add(areaList[8]);
        }
        else if(i == 19 || i == 5 || i == 2 || i == 1 || i == 6)
        {
            Debug.Log("Second: 19, 5, 2, 4, 6");
            useableAreas.Add(areaList[7]);
            useableAreas.Add(areaList[3]);
            useableAreas.Add(areaList[2]);
            useableAreas.Add(areaList[1]);
            useableAreas.Add(areaList[0]);
            useableAreas.Add(areaList[11]);
            useableAreas.Add(areaList[10]);
            useableAreas.Add(areaList[9]);
            useableAreas.Add(areaList[13]);
        }
        else if (i == 11 || i == 17 || i == 4 || i == 9 || i == 16)
        {
            Debug.Log("Third: 11, 17, 4, 9, 16");
            useableAreas.Add(areaList[13]);
            useableAreas.Add(areaList[12]);
            useableAreas.Add(areaList[8]);
            useableAreas.Add(areaList[9]);
            useableAreas.Add(areaList[5]);
            useableAreas.Add(areaList[14]);
            useableAreas.Add(areaList[10]);
            useableAreas.Add(areaList[11]);
            useableAreas.Add(areaList[15]);
        }
        else
        {
            Debug.Log("Last: 3, 12, 14, 15, 18");
            useableAreas.Add(areaList[8]);
            useableAreas.Add(areaList[9]);
            useableAreas.Add(areaList[5]);
            useableAreas.Add(areaList[4]);
            useableAreas.Add(areaList[0]);
            useableAreas.Add(areaList[10]);
            useableAreas.Add(areaList[6]);
            useableAreas.Add(areaList[7]);
            useableAreas.Add(areaList[3]);
        }
        
        foreach (Area ua in useableAreas)
        {
            ua.isUsed = true;

        }
        foreach(Area ua in useableAreas)
        {
            foreach (Area area in areaList)
            {
                ua.CheckActiveDirections(area);
            }
        }
        FindObjectOfType<PlayerMovement>().GetComponent<SpriteRenderer>().enabled = true;
        FindObjectOfType<PlayerMovement>().SetPlayerLocked(false);
        useableAreas[0].gameObject.SetActive(true);
        FindObjectOfType<UIManager>().StartRound();
    }

    public void ChangeToNewArea(Area a, Direction d)
    {
        Debug.Log("I am changing to Area: " + a.name);
        previousArea = currentArea;
        currentArea.gameObject.SetActive(false);
        Debug.Log(d);
        a.SetActiveArea(d);
    }

    public void SetCurrentArea(Area a)
    {
        currentArea = a;
    }

    public Weather ReturnCurrentWeather()
    {
        return currentWeather;
    }

    public Sprite ReturnBackground()
    {
        Sprite toSend = null;
        switch (currentWeather)
        {
            case Weather.Sun:
                toSend =  sunBackground;
                break;
            case Weather.Wet:
                toSend =  wetBackground;
                break;
            case Weather.Snow:
                toSend = snowBackground;
                break;
        }
        return toSend;
    }

    public void PlayBackgroundMusic()
    {
        switch (currentWeather)
        {
            case Weather.Sun:
                GetComponent<AudioSource>().clip = sunnyMusic;
                break;
            case Weather.Wet:
                GetComponent<AudioSource>().clip = wetMusic;
                break;
            case Weather.Snow:
                GetComponent<AudioSource>().clip = snowMusic;
                break;
        }
        GetComponent<AudioSource>().Play();
    }

    private void Start()
    {
        currentWeather = GameManager.Instance.ReturnWeather();
        PlayBackgroundMusic();
        theFirstDiceRoll = GameManager.Instance.ReturnFirstDiceRoll();
        theSecondDiceRoll = GameManager.Instance.ReturnSecondDiceRoll();
        PopulateList(theFirstDiceRoll);
    }
}
