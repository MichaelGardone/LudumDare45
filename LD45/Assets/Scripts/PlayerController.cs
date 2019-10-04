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
            float angle = AngleBetween(mouseScreenPos, playerScreenPos);
            
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


        float xVel = 0, yVel = 0;

        if (Input.GetKey(KeyCode.W))
            yVel = Time.deltaTime * 10.0f * 20.0f;
        else if(Input.GetKey(KeyCode.S))
            yVel = -Time.deltaTime * 10.0f * 20.0f;
        else
            yVel = 0;

        if (Input.GetKey(KeyCode.A))
            xVel = -Time.deltaTime * 10.0f * 20.0f;
        else if (Input.GetKey(KeyCode.D))
            xVel = Time.deltaTime * 10.0f * 20.0f;
        else
            xVel = 0;
        
        rb.velocity = new Vector2(xVel, yVel);
    }
    
    float AngleBetween(Vector3 left, Vector3 right)
    {
        return Mathf.Atan2(left.y - right.y, left.x - right.x) * Mathf.Rad2Deg;
    }
    
}
