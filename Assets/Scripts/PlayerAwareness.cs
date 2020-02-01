using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAwareness : MonoBehaviour
{
    private bool canMove = true;
    [SerializeField] private bool canCollect;
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
        float timeToCollect = currentResource.timeToHarvest;
        Debug.Log("Collect: " + currentResource.name + " Time: " + timeToCollect + " Pos: " + currentResource.transform.position );
        GameManager.Instance.CollectResource(timeToCollect, currentResource);
    }
}
