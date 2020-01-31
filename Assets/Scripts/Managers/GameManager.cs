using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public int woodResource;
    public int mudResource;
    public int berriesResource;
    public TMP_InputField inputField;

    [Space(20)]
    [SerializeField] private float timer;
    [SerializeField] private int diceRoll;
    private bool newDay;

    

    private Weather DecideWeather()
    {
        int i = Random.Range(0, 20);
        if(i >= 0 && i < 8)
        {
            return Weather.Sun;
        }
        else if (i >= 8 && i < 14)
        {
            return Weather.Snow;
        }
        else
        {
            return Weather.Wet;
        }
    }

    public void CheckDiceInput(TMP_InputField inputField)
    {
        string s = inputField.text;
        int dice = int.Parse(s);
        if (dice > 0 && dice <= 20)
        {
            diceRoll = dice;
            StartDay(dice);
        }
        else
        {
            inputField.Select();
            inputField.text = "INVALID NUMBER";
        }
    }

    private void StartDay(int diceRoll)
    {
        Weather w = DecideWeather();
        AreaManager.Instance.RecieveNewDay(w, diceRoll);
    }
    
}

public enum Weather
{
    Sun,
    Wet,
    Snow
}
