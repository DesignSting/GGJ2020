using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public int amount;
    public float timeToHarvest;
    public Sprite currentWeather;
    [SerializeField] private bool canUse;
    
    public TypeResource resource;

    [Space(15)]
    public Sprite sunSprite;
    public Sprite wetSprite;
    public Sprite snowSprite;
    public Sprite harvestedSprite;

    [Space(10)]
    public float wetModifer;
    public float snowModifer;
    public float sunModifer;
    public float baseTime;

    [Space(10)]
    public bool notInSun;
    public bool notInWet;
    public bool notInSnow;

    public void ApplyModifer(Weather w)
    {
        if(w == Weather.Snow)
        {
            timeToHarvest = baseTime * snowModifer;
            GetComponent<SpriteRenderer>().sprite = snowSprite;
        }
        else if (w == Weather.Wet)
        {
            timeToHarvest = baseTime * wetModifer;
            GetComponent<SpriteRenderer>().sprite = wetSprite;
        }
        else
        {
            timeToHarvest = baseTime * sunModifer;
            GetComponent<SpriteRenderer>().sprite = sunSprite;
        }

    }

    public void Harvested()
    {
        GetComponent<SpriteRenderer>().sprite = harvestedSprite;
        canUse = false;
    }

    public bool ReturnCanUse()
    {
        return canUse;
    }
}

public enum TypeResource
{
    Wood, Berries, Mud
}
