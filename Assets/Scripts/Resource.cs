using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public int amount;
    public float timeToHarvest;
    public Sprite currentWeather;
    [SerializeField] private bool canUse;
    [SerializeField] private bool harvested;
    
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
        if (harvested)
        {
            GetComponent<SpriteRenderer>().sprite = harvestedSprite;
            return;
        }
        if (w == Weather.Snow)
        {
            if (resource == TypeResource.Mud)
                canUse = false;
            else
                timeToHarvest = baseTime * snowModifer;

            GetComponent<SpriteRenderer>().sprite = snowSprite;
        }
        else if (w == Weather.Wet)
        {
            if (resource == TypeResource.Berries)
                canUse = false;
            else
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
        harvested = true;
        if(resource == TypeResource.Berries)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void ResetCanUse()
    {
        canUse = true;
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
