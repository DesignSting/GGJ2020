using System.Collections;
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
        DontDestroyOnLoad(gameObject);
    }


    public List<Area> areaList = new List<Area>();
    private Weather theWeather;
    [SerializeField] private Area previousArea;
    [SerializeField] private Area currentArea;

    public void RecieveNewDay(Weather weather, int diceRoll)
    {
        theWeather = weather;
        Debug.Log("Weather: " + weather + "\nDice Roll: " + diceRoll);
    }

    public void ChangeToNewArea(Area a, Direction d)
    {
        Debug.Log("I am changing to Area: " + a.name);
        previousArea = currentArea;
        currentArea.gameObject.SetActive(false);
        a.SetActive(d);
    }

    public void SetCurrentArea(Area a)
    {
        currentArea = a;
    }
}
