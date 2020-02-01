using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAwareness : MonoBehaviour
{
    private bool canMove = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Environment")
        {
            canMove = false;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Environment")
        {
            canMove = true;
        }
    }

    public bool ReturnCanMove()
    {
        return canMove;
    }
}
