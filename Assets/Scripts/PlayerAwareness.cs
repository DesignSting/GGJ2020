using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAwareness : MonoBehaviour
{
    private bool canMove = true;
    private bool canCollect;
    private bool isCollecting;
    private Resource currentResource;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Environment")
        {
            canMove = false;
        }
        if(other.tag == "Resource")
        {
            canMove = false;
            canCollect = true;
            currentResource = other.GetComponent<Resource>();
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Environment")
        {
            canMove = true;
        }
        if (other.tag == "Resource")
        {
            canMove = true;
            canCollect = false;
            currentResource = null;
        }
    }

    public bool ReturnCanMove()
    {
        return canMove;
    }

    public bool ReturnCanCollect()
    {
        return canCollect;
    }

    public void CollectResource()
    {
        if(currentResource.ReturnCanUse())
        {
            StartCoroutine(Collect());
        }
    }

    IEnumerator Collect()
    {
        isCollecting = true;
        float timer = 0.0f;
        float timeToCollect = currentResource.timeToHarvest;

        while( timer < timeToCollect)
        {
            timer += Time.deltaTime;


            yield return null;
        }


        isCollecting = false;
    }
}
