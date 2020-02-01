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
    }


    public List<Area> areaList = new List<Area>();
    public List<Area> useableAreas = new List<Area>();
    private Weather theWeather;
    private int theDiceRoll;
    [SerializeField] private Area previousArea;
    [SerializeField] private Area currentArea;

    public void RecieveNewDay(Weather weather, int diceRoll)
    {
        theWeather = weather;
        Debug.Log("Weather: " + weather + "\nDice Roll: " + diceRoll);
        
    }

    private void PopulateList(int i)
    {
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
            a.gameObject.SetActive(false);
        }



        /**
         * Testing Area
         **/

        useableAreas.Add(areaList[7]);
        useableAreas.Add(areaList[6]);
        useableAreas.Add(areaList[10]);
        useableAreas.Add(areaList[11]);
        useableAreas.Add(areaList[9]);



        /**
         * Between here and above is my own list of areas, this will be done vias a switch or if statements
         * 
         * 
         * */

        foreach(Area ua in useableAreas)
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
        useableAreas[0].gameObject.SetActive(true);
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

    private Weather DecideWeather()
    {
        int i = Random.Range(0, 20);
        if (i >= 0 && i < 8)
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

    public Weather ReturnCurrentWeather()
    {
        return theWeather;
    }

    private void Start()
    {
        theWeather = DecideWeather();
        theDiceRoll = GameManager.Instance.ReturnDiceRoll();
        PopulateList(theDiceRoll);
    }
}
