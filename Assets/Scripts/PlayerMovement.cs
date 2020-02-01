using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Direction currentDir;
    private Vector2 input;
    private bool isMoving;
    private Vector3 startPos;
    private Vector3 endPos;
    private float t;
    [SerializeField] private bool canMove;
    [SerializeField] private bool canCollect;

    [Space(15)]
    public Sprite northSprite;
    public Sprite eastSprite;
    public Sprite southSprite;
    public Sprite westSprite;
    public float walkSpeed = 3f;

    [Space(15)]
    public PlayerAwareness northCollider;
    public PlayerAwareness eastCollider;
    public PlayerAwareness southCollider;
    public PlayerAwareness westCollider;

    
    public IEnumerator Move(Transform entity)
    {
        isMoving = true;
        startPos = entity.position;
        t = 0;

        endPos = new Vector3(startPos.x + System.Math.Sign(input.x), startPos.y + System.Math.Sign(input.y), startPos.z);

        while(t < 1.0f)
        {
            t += Time.deltaTime * walkSpeed;
            entity.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        isMoving = false;
        yield return 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isMoving)
        {
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            {
                input.y = 0;
            }
            else
                input.x = 0;

            if (input != Vector2.zero)
            {
                if(input.x < 0)
                {
                    currentDir = Direction.West;
                }
                if(input.x > 0)
                {
                    currentDir = Direction.East;
                }
                if(input.y < 0)
                {
                    currentDir = Direction.South;
                }
                if(input.y > 0)
                {
                    currentDir = Direction.North;
                }

                switch (currentDir)
                {
                    case Direction.North:
                        GetComponent<SpriteRenderer>().sprite = northSprite;
                        canMove = northCollider.ReturnCanMove();
                        canCollect = northCollider.ReturnCanCollect();
                        break;
                    case Direction.East:
                        GetComponent<SpriteRenderer>().sprite = eastSprite;
                        canMove = eastCollider.ReturnCanMove();
                        canCollect = eastCollider.ReturnCanCollect();
                        break;
                    case Direction.South:
                        GetComponent<SpriteRenderer>().sprite = southSprite;
                        canMove = southCollider.ReturnCanMove();
                        canCollect = southCollider.ReturnCanCollect();
                        break;
                    case Direction.West:
                        GetComponent<SpriteRenderer>().sprite = westSprite;
                        canMove = westCollider.ReturnCanMove();
                        canCollect = westCollider.ReturnCanCollect();
                        break;
                }
                if (canMove)
                    StartCoroutine(Move(transform));
                else
                    canMove = true;
            }
        }
        if(canCollect)
        {
            if(Input.GetKeyUp(KeyCode.Space))
            {
                Debug.Log("I am pressing the space button on this");
            }
        }
    }
}
