using System.Collections;
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
    private GameObject explosion;
    
    [SerializeField]
    EntityFacing facing = EntityFacing.EAST;

    [SerializeField]
    float blinkDist = 20.0f;

    [SerializeField]
    GameObject[] guns;

    int currWep = 3;

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

    bool canDash = true;

    [SerializeField]
    float dashMax = 1.0f;

    [SerializeField]
    float dashCoolDown = 0.0f;
    
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

        if (Input.GetKeyDown(KeyCode.Space) && dashOn == false && canDash)
        {
            switch (facing)
            {
                case EntityFacing.EAST:
                    targetDash = new Vector3(gameObject.transform.position.x + blinkDist, gameObject.transform.position.y, -1);
                    animControl.SetInteger("x_vel", 1);
                    break;
                case EntityFacing.NORTH:
                    targetDash = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + blinkDist, -1);
                    animControl.SetInteger("y_vel", 1);
                    break;
                case EntityFacing.SOUTH:
                    targetDash = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - blinkDist, -1);
                    animControl.SetInteger("y_vel", -1);
                    break;
                case EntityFacing.WEST:
                    targetDash = new Vector3(gameObject.transform.position.x - blinkDist, gameObject.transform.position.y, -1);
                    animControl.SetInteger("x_vel", -1);
                    break;
            }

            canDash = false;
            dashOn = true;
            shadowControl.SetBool("dashing", true);
        }

        float xVel = 0, yVel = 0;

        if (dashOn)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetDash, dashSpeed * Time.deltaTime);

            if (transform.position == targetDash)
            {
                dashOn = false;
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
        
        if(Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            if(guns[currWep].GetComponent<Gun>() && availableKeys[guns[currWep].name])
                guns[currWep].GetComponent<Gun>().Shoot();
        }

        if(Input.GetAxis("Mouse ScrollWheel") > 0f && availableKeys["SCROLL"])
        {
            guns[currWep].GetComponent<SpriteRenderer>().enabled = false;
            currWep++;
            if (currWep >= guns.Length)
                currWep = 0;
            while (availableKeys[guns[currWep].name])
            {
                currWep++;
                if (currWep >= guns.Length)
                    currWep = 0;
            }
            guns[currWep].GetComponent<SpriteRenderer>().enabled = true;
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0f && availableKeys["SCROLL"])
        {
            guns[currWep].GetComponent<SpriteRenderer>().enabled = false;
            currWep--;
            if (currWep < 0)
                currWep = guns.Length - 1;
            while (availableKeys[guns[currWep].name])
            {
                currWep--;
                if (currWep < 0)
                    currWep = guns.Length - 1;
            }
            guns[currWep].GetComponent<SpriteRenderer>().enabled = true;
        }

        if (keys.Count > 1)
            animControl.SetBool("no_keys", false);
        else
            animControl.SetBool("no_keys", true);

        if(canDash == false && dashCoolDown > 0)
            dashCoolDown += Time.deltaTime;

        if (canDash == false && dashCoolDown >= dashMax)
        {
            dashCoolDown = 0.0f;
            canDash = true;
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
            if (s == "PISTOL")
            {
                guns[currWep].GetComponent<SpriteRenderer>().enabled = false;
                while (availableKeys[guns[currWep].name])
                {
                    currWep++;
                    if (currWep >= guns.Length)
                        currWep = 0;
                }
                guns[currWep].GetComponent<SpriteRenderer>().enabled = true;
                gunsCollected++;
            }
            lostKey.Invoke(s);
        }

        if(keys.Count == 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            GetComponent<SpriteRenderer>().enabled = false;
            Debug.Log("## PLAYER IS DEAD!! ##");
            UIManager.instance.GameOver();
        }
            
    }

    int gunsCollected = 0;

    public void AddHealth(string s)
    {
        keys.Enqueue(s);
        if(gunsCollected == 0)
        {
            if(s == "PISTOL")
            {
                guns[currWep].GetComponent<SpriteRenderer>().enabled = false;
                currWep = 0;
                guns[currWep].GetComponent<SpriteRenderer>().enabled = true;
                gunsCollected++;
            }
        }
        availableKeys[s] = true;
    }
    
}
