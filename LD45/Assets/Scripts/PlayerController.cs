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
    PlayerFacing facing2 = PlayerFacing.EAST;

    [SerializeField]
    float blinkDist = 20.0f;

    [SerializeField]
    float initialBulletVel = 20.0f;

    [SerializeField]
    GameObject bulletPref;

    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
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
            if ((angle >= -30.0f && angle <= 30.0f) || (angle >= 150.0f || angle <= -150.0f))
                facing = PlayerFacing.EAST;
            else if (angle > 30.0f && angle < 150.0f)
                facing = PlayerFacing.NORTH;
            else if (angle < -30.0f && angle > -150.0f)
                facing = PlayerFacing.SOUTH;

            facing2 = PlayerFacing.EAST;
        }
        // North
        else if (angle >= 45.001f && angle <= 135.0f)
        {
            if (angle >= -30.0f && angle <= 30.0f)
                facing = PlayerFacing.EAST;
            else if (angle >= 150.0f || angle <= -150.0f)
                facing = PlayerFacing.WEST;
            else if ((angle > 30.0f && angle < 150.0f) || (angle < -30.0f && angle > -150.0f))
                facing = PlayerFacing.NORTH;

            facing2 = PlayerFacing.NORTH;
        }
        // West
        else if (angle >= 135.001f || angle <= -135.001f)
        {
            if ((angle >= -30.0f && angle <= 30.0f) || (angle >= 150.0f || angle <= -150.0f))
                facing = PlayerFacing.WEST;
            else if (angle > 30.0f && angle < 150.0f)
                facing = PlayerFacing.NORTH;
            else if (angle < -30.0f && angle > -150.0f)
                facing = PlayerFacing.SOUTH;

            facing2 = PlayerFacing.WEST;
        }
        // South
        else if (angle >= -135.0f && angle <= -45.0f)
        {
            if (angle >= -30.0f && angle <= 30.0f)
                facing = PlayerFacing.EAST;
            else if (angle >= 150.0f || angle <= -150.0f)
                facing = PlayerFacing.WEST;
            else if ((angle > 30.0f && angle < 150.0f) || (angle < -30.0f && angle > -150.0f))
                facing = PlayerFacing.SOUTH;

            facing2 = PlayerFacing.SOUTH;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (facing2)
            {
                case PlayerFacing.EAST:
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x + blinkDist, gameObject.transform.position.y, 0);
                    break;
                case PlayerFacing.NORTH:
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + blinkDist, 0);
                    break;
                case PlayerFacing.SOUTH:
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - blinkDist, 0);
                    break;
                case PlayerFacing.WEST:
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x - blinkDist, gameObject.transform.position.y, 0);
                    break;
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

        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir = mouseWorldPos - new Vector2(transform.transform.position.x, transform.transform.position.y);
            dir.Normalize();

            GameObject g = Instantiate(bulletPref, transform.position, Quaternion.identity);
            
            g.GetComponent<Rigidbody2D>().velocity = dir * initialBulletVel;
        }

    }
    
    float AngleBetween(Vector3 left, Vector3 right)
    {
        return Mathf.Atan2(left.y - right.y, left.x - right.x) * Mathf.Rad2Deg;
    }
    
}
