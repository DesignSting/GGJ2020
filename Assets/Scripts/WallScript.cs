using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    public Sprite sunnyWall;
    public Sprite wetWall;
    public Sprite snowWall;

    public void ChangeWeather(Weather w)
    {
        switch (w)
        {
            case Weather.Sun:
                GetComponent<SpriteRenderer>().sprite = sunnyWall;
                break;
            case Weather.Wet:
                GetComponent<SpriteRenderer>().sprite = wetWall;
                break;
            case Weather.Snow:
                GetComponent<SpriteRenderer>().sprite = snowWall;
                break;
        }
    }
}
