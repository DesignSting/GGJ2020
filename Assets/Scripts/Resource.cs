using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public int amount;
    public float timeToHarvest;
    public Sprite currentWeather;
    [SerializeField] private bool canUse;
    
    public typeResource resource;

    [Space(15)]
    public Sprite sunSprite;
    public Sprite wetSprite;
    public Sprite snowSprite;

    [Space(10)]
    public float wetModifer;
    public float snowModifer;
    public float baseTime;

    public void ApplyModifer(Weather w)
    {
        if(w == Weather.Snow)
        {
            timeToHarvest = baseTime * snowModifer;
        }
        else if (w == Weather.Wet)
        {
            timeToHarvest = baseTime * wetModifer;
        }
        else
        {
            timeToHarvest = baseTime;
        }

    }

    public bool ReturnCanUse()
    {
        return canUse;
    }
}

public enum typeResource
{
    Wood, Berries, Mud
}
