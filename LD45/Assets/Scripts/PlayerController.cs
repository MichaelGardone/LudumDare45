using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerFacing
{
    NORTH,
    SOUTH,
    EAST,
    WEST
}

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    [SerializeField]
    PlayerFacing facing = PlayerFacing.EAST;

    [SerializeField]
    float blinkDist = 20.0f;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Space))
        {
            // Screen pos of player
            Vector2 playerScreenPos = Camera.main.WorldToViewportPoint(transform.position);

            // Screen pos of mouse
            Vector2 mouseScreenPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            // Angle between two positions
            float angle = AngleBetweenTwoPoints(mouseScreenPos, playerScreenPos);
            
            // East
            if (angle >= -45.001f && angle <= 45.0f)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + blinkDist, gameObject.transform.position.y, 0);
            }
            // North
            else if (angle >= 45.001f && angle <= 135.0f)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + blinkDist, 0);
            }
            // West
            else if (angle >= 135.001f || angle <= -135.001f)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x - blinkDist, gameObject.transform.position.y, 0);
            }
            // South
            else if (angle >= -135.0f && angle <= -45.0f)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - blinkDist, 0);
            }
        }
        //else
        //{
            if(Input.GetKeyDown(KeyCode.W))
            {
                
            }
            if (Input.GetKeyDown(KeyCode.A))
            {

            }
            if (Input.GetKeyDown(KeyCode.S))
            {

            }
            if (Input.GetKeyDown(KeyCode.D))
            {

            }
        //}

    }
    
    float AngleBetweenTwoPoints(Vector3 left, Vector3 right)
    {
        return Mathf.Atan2(left.y - right.y, left.x - right.x) * Mathf.Rad2Deg;
    }
    
}
