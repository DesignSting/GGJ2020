using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionBetweenAreas : MonoBehaviour
{
    [SerializeField] private Area nextArea;
    private bool withinRange;
    [SerializeField] private Direction direction;

    [SerializeField] private float timer;

    public void AddAreaToSprite(Area a, Direction d)
    {
        nextArea = a;
        direction = d;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            withinRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            withinRange = false;
            timer = 0;
        }
    }

    private void Update()
    {
        if(withinRange)
        {
            timer += Time.deltaTime;

            if (timer > 0.5f)
            {
                FindObjectOfType<PlayerMovement>().SetPlayerLocked(true);
                AreaManager.Instance.ChangeToNewArea(nextArea, direction);
                timer = 0;
            }
        }
    }
}
