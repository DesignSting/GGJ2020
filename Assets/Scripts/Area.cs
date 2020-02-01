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
        //AreaManager.Instance.SetCurrentArea(this);

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

        foreach(Resource r in resourceList)
        {
            switch(currentWeather)
            {

            }
            r.ApplyModifer(currentWeatherTesting);
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
                southArrowSprite.GetComponent<TransitionBetweenAreas>().DisablePoint();
                break;
            case Direction.East:
                startingPos = westArrowSprite.transform;
                Debug.Log("Starting Pos: " + startingPos);
                westArrowSprite.GetComponent<TransitionBetweenAreas>().DisablePoint();
                break;
            case Direction.South:
                startingPos = northArrowSprite.transform;
                Debug.Log("Starting Pos: " + startingPos);
                northArrowSprite.GetComponent<TransitionBetweenAreas>().DisablePoint();
                break;
            case Direction.West:
                startingPos = eastArrowSprite.transform;
                Debug.Log("Starting Pos: " + startingPos);
                eastArrowSprite.GetComponent<TransitionBetweenAreas>().DisablePoint();
                break;
        }
        gameObject.SetActive(this);
        FindObjectOfType<PlayerMovement>().SetPlayerLocked(false);
    }
}

public enum Direction
{
    North,
    East,
    South,
    West
}
