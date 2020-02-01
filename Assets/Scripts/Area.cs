using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    public Transform startingPos;
    public List<Resource> resourceList = new List<Resource>();
    public bool isUsed;

    [Space(15)]
    public Area north;
    public Area east;
    public Area south;
    public Area west;

    [Space(15)]
    public SpriteRenderer northArrowSprite;
    public SpriteRenderer eastArrowSprite;
    public SpriteRenderer southArrowSprite;
    public SpriteRenderer westArrowSprite;

    [Space(15)]
    public Sprite boulderSprite;
    public Sprite fallenTreeSprite;


    public Weather currentWeatherTesting = Weather.Snow;
    public Weather currentWeather;

    private void OnEnable()
    {
        GameObject playerObject = GameObject.Find("Player");
        if(startingPos != null)
            playerObject.transform.position = startingPos.position;
        FindObjectOfType<AreaManager>().SetCurrentArea(this);
        Resource[] resArray = GetComponentsInChildren<Resource>();
        resourceList = new List<Resource>(resArray);

       if(north != null)
        {
            northArrowSprite.GetComponentInChildren<TransitionBetweenAreas>().AddAreaToSprite(north, Direction.North);
        }
        if (east != null)
        {
            eastArrowSprite.GetComponentInChildren<TransitionBetweenAreas>().AddAreaToSprite(east, Direction.East);
        }
        if (south != null)
        {
            southArrowSprite.GetComponentInChildren<TransitionBetweenAreas>().AddAreaToSprite(south, Direction.South);
        }
        if (west != null)
        {
            westArrowSprite.GetComponentInChildren<TransitionBetweenAreas>().AddAreaToSprite(west, Direction.West);
        }

        currentWeather = AreaManager.Instance.ReturnCurrentWeather();

        SpriteRenderer temp = GameObject.Find("Background").GetComponent<SpriteRenderer>();
        //SpriteRenderer sr = GameObject.Find(ua.name + "/Background").GetComponent<SpriteRenderer>();
        temp.sprite = AreaManager.Instance.ReturnBackground();

        foreach (Resource r in resourceList)
        {
            r.ApplyModifer(currentWeather);
        }
    }

    public void CheckActiveDirections(Area a)
    {
        if (north != null)
        {
            if (north.isUsed)
            {
                northArrowSprite.gameObject.SetActive(true);
            }
        }
        if (east != null)
        {
            if (east.isUsed)
            {
                eastArrowSprite.gameObject.SetActive(true);
            }
        }
        if (south != null)
        {
            if (south.isUsed)
            {
                southArrowSprite.gameObject.SetActive(true);
            }
        }
        if (west != null)
        {
            if (west.isUsed)
            {
                westArrowSprite.gameObject.SetActive(true);
            }
        }
    }

    public void SetActiveArea(Direction d)
    {
        Debug.Log("Area Direction: " + d);
        switch (d)
        {
            case Direction.North:
                startingPos = southArrowSprite.transform;
                Debug.Log("Starting Pos: " + startingPos);
                break;
            case Direction.East:
                startingPos = westArrowSprite.transform;
                Debug.Log("Starting Pos: " + startingPos);
                break;
            case Direction.South:
                startingPos = northArrowSprite.transform;
                Debug.Log("Starting Pos: " + startingPos);
                break;
            case Direction.West:
                startingPos = eastArrowSprite.transform;
                Debug.Log("Starting Pos: " + startingPos);
                break;
        }
        gameObject.SetActive(this);
        FindObjectOfType<PlayerMovement>().SetPlayerLocked(false);
    }

    public void ResetResources()
    {
        foreach(Resource r in resourceList)
        {
            r.ResetCanUse();
        }
    }
}

public enum Direction
{
    North,
    East,
    South,
    West
}
