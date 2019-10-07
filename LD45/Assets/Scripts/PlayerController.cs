﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    public delegate void LoseHealth(string key);
    public LoseHealth lostKey;

    Dictionary<string, bool> availableKeys = new Dictionary<string, bool>();

    [SerializeField]
    public Queue<string> keys = new Queue<string>();
    
    [SerializeField]
    EntityFacing facing = EntityFacing.EAST;

    [SerializeField]
    float blinkDist = 20.0f;

    public float BlinkDist
    {
        get
        {
            return blinkDist;
        }
        protected set
        {
            blinkDist = value;
        }
    }

    [SerializeField]
    float dashSpeed = 10f;

    [SerializeField]
    float initialBulletVel = 20.0f;

    [SerializeField]
    GameObject bulletPref;

    [SerializeField]
    Animator animControl;

    [SerializeField]
    Animator shadowControl;

    Rigidbody2D rb;
    
    Vector3 targetDash;

    bool dashOn = false;

    [SerializeField]
    float dashMax = 1.0f;

    float dashCoolDown = 0.0f;
    
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        TextAsset txtAsset = (TextAsset)Resources.Load("keys", typeof(TextAsset));
        string[] keyFile = txtAsset.text.Split(new char[] { '\n' });
        
        availableKeys.Add("Space", true);
        keys.Enqueue("Space");

        foreach (string s in keyFile)
            availableKeys.Add(s.Trim(), false);
        
    }
    
    void Update()
    {

        if(dashOn == false)
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
                facing = EntityFacing.EAST;
                animControl.SetFloat("east_west", 1);
                animControl.SetFloat("north_south", 0);
            }
            // North
            else if (angle >= 45.001f && angle <= 135.0f)
            {
                facing = EntityFacing.NORTH;
                animControl.SetFloat("east_west", 0);
                animControl.SetFloat("north_south", 1);
            }
            // West
            else if (angle >= 135.001f || angle <= -135.001f)
            {
                facing = EntityFacing.WEST;
                animControl.SetFloat("east_west", -1);
                animControl.SetFloat("north_south", 0);
            }
            // South
            else if (angle >= -135.0f && angle <= -45.0f)
            {
                facing = EntityFacing.SOUTH;
                animControl.SetFloat("east_west", 0);
                animControl.SetFloat("north_south", -1);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && dashOn == false)
        {
            switch (facing)
            {
                case EntityFacing.EAST:
                    targetDash = new Vector3(gameObject.transform.position.x + blinkDist, gameObject.transform.position.y, 0);
                    animControl.SetInteger("x_vel", 1);
                    break;
                case EntityFacing.NORTH:
                    targetDash = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + blinkDist, 0);
                    animControl.SetInteger("y_vel", 1);
                    break;
                case EntityFacing.SOUTH:
                    targetDash = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - blinkDist, 0);
                    animControl.SetInteger("y_vel", -1);
                    break;
                case EntityFacing.WEST:
                    targetDash = new Vector3(gameObject.transform.position.x - blinkDist, gameObject.transform.position.y, 0);
                    animControl.SetInteger("x_vel", -1);
                    break;
            }

            dashOn = true;
            shadowControl.SetBool("dashing", true);
        }

        float xVel = 0, yVel = 0;

        if (dashOn)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetDash, dashSpeed * Time.deltaTime);

            if (transform.position == targetDash)
            {
                dashCoolDown += Time.deltaTime;
                animControl.SetInteger("x_vel", 0);
                animControl.SetInteger("y_vel", 0);
                shadowControl.SetBool("dashing", false);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.W) && availableKeys["W"])
            {
                yVel = Time.deltaTime * 10.0f * 20.0f;
                animControl.SetInteger("y_vel", 1);
            }
            else if(Input.GetKey(KeyCode.S) && availableKeys["S"])
            {
                yVel = -Time.deltaTime * 10.0f * 20.0f;
                animControl.SetInteger("y_vel", -1);
            }
            else
            {
                yVel = 0;
                animControl.SetInteger("y_vel", 0);
            }

            if (Input.GetKey(KeyCode.A) && availableKeys["A"])
            {
                xVel = -Time.deltaTime * 10.0f * 20.0f;
                animControl.SetInteger("x_vel", -1);
            }
            else if (Input.GetKey(KeyCode.D) && availableKeys["D"])
            {
                xVel = Time.deltaTime * 10.0f * 20.0f;
                animControl.SetInteger("x_vel", 1);
            }
            else
            {
                xVel = 0;
                animControl.SetInteger("x_vel", 0);
            }
        }

        rb.velocity = new Vector2(xVel, yVel);
        
        if((Input.GetMouseButtonDown(0) && availableKeys["LMB"]) || (Input.GetMouseButtonDown(1) && availableKeys["RMB"]))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir = mouseWorldPos - new Vector2(transform.transform.position.x, transform.transform.position.y);
            dir.Normalize();

            GameObject g = Instantiate(bulletPref, transform.position, Quaternion.identity);
            
            g.GetComponent<Rigidbody2D>().velocity = dir * initialBulletVel;
        }

        if (keys.Count > 1)
            animControl.SetBool("no_keys", false);
        else
            animControl.SetBool("no_keys", true);

        if(dashOn == false && dashCoolDown > 0)
        {
            dashCoolDown += Time.deltaTime;
        }
        else if (dashOn == false && dashCoolDown >= dashMax)
        {
            dashCoolDown = 0.0f;
            dashOn = true;
        }
        
    }
    
    float AngleBetween(Vector3 left, Vector3 right)
    {
        return Mathf.Atan2(left.y - right.y, left.x - right.x) * Mathf.Rad2Deg;
    }

    public void TakeDamage(int damage)
    {
        for(int i = 0; i < damage && i < keys.Count; i++)
        {
            string s = keys.Dequeue();
            availableKeys[s] = false;
            lostKey.Invoke(s);
        }

        if(keys.Count == 0)
            Debug.Log("## PLAYER IS DEAD!! ##");
    }

    public void AddHealth(string s)
    {
        keys.Enqueue(s);
        availableKeys[s] = true;
    }
    
}
