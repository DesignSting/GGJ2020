using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    public Transform startingPos;
    public List<Resource> resourceList = new List<Resource>();

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

    private void OnEnable()
    {
        GameObject playerObject = GameObject.Find("Player");
        playerObject.transform.position = startingPos.position;
        FindObjectOfType<AreaManager>().SetCurrentArea(this);
        //AreaManager.Instance.SetCurrentArea(this);

       if(north != null)
        {
            northArrowSprite.GetComponent<TransitionBetweenAreas>().AddAreaToSprite(north, Direction.North);
        }
        if (east != null)
        {
            eastArrowSprite.GetComponent<TransitionBetweenAreas>().AddAreaToSprite(east, Direction.East);
        }
        if (south != null)
        {
            southArrowSprite.GetComponent<TransitionBetweenAreas>().AddAreaToSprite(south, Direction.South);
        }
        if (west != null)
        {
            westArrowSprite.GetComponent<TransitionBetweenAreas>().AddAreaToSprite(west, Direction.West);
        }

        foreach(Resource r in resourceList)
        {
            r.ApplyModifer(currentWeatherTesting);
        }
    }

    public void SetActive(Direction d)
    {
        switch (d)
        {
            case Direction.North:
                startingPos = southArrowSprite.transform;
                southArrowSprite.GetComponent<TransitionBetweenAreas>().DisablePoint();
                break;
            case Direction.East:
                startingPos = westArrowSprite.transform;
                westArrowSprite.GetComponent<TransitionBetweenAreas>().DisablePoint();
                break;
            case Direction.South:
                startingPos = northArrowSprite.transform;
                northArrowSprite.GetComponent<TransitionBetweenAreas>().DisablePoint();
                break;
            case Direction.West:
                startingPos = eastArrowSprite.transform;
                eastArrowSprite.GetComponent<TransitionBetweenAreas>().DisablePoint();
                break;
        }
        gameObject.SetActive(this);
    }
}

public enum Direction
{
    North,
    East,
    South,
    West
}
