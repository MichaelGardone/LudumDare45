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
    float initialBulletVel = 20.0f;

    [SerializeField]
    GameObject bulletPref;

    Rigidbody2D rb;

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
        // Screen pos of player
        Vector2 playerScreenPos = Camera.main.WorldToViewportPoint(transform.position);

        // Screen pos of mouse
        Vector2 mouseScreenPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        // Angle between two positions
        float angle = AngleBetween(mouseScreenPos, playerScreenPos);

        // East
        if (angle >= -45.001f && angle <= 45.0f)
            facing = EntityFacing.EAST;
        // North
        else if (angle >= 45.001f && angle <= 135.0f)
            facing = EntityFacing.NORTH;
        // West
        else if (angle >= 135.001f || angle <= -135.001f)
            facing = EntityFacing.WEST;
        // South
        else if (angle >= -135.0f && angle <= -45.0f)
            facing = EntityFacing.SOUTH;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (facing)
            {
                case EntityFacing.EAST:
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x + blinkDist, gameObject.transform.position.y, 0);
                    break;
                case EntityFacing.NORTH:
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + blinkDist, 0);
                    break;
                case EntityFacing.SOUTH:
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - blinkDist, 0);
                    break;
                case EntityFacing.WEST:
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x - blinkDist, gameObject.transform.position.y, 0);
                    break;
            }
        }
        
        float xVel = 0, yVel = 0;

        if (Input.GetKey(KeyCode.W) && availableKeys["W"])
            yVel = Time.deltaTime * 10.0f * 20.0f;
        else if(Input.GetKey(KeyCode.S) && availableKeys["S"])
            yVel = -Time.deltaTime * 10.0f * 20.0f;
        else
            yVel = 0;

        if (Input.GetKey(KeyCode.A) && availableKeys["A"])
            xVel = -Time.deltaTime * 10.0f * 20.0f;
        else if (Input.GetKey(KeyCode.D) && availableKeys["D"])
            xVel = Time.deltaTime * 10.0f * 20.0f;
        else
            xVel = 0;
        
        rb.velocity = new Vector2(xVel, yVel);

        if((Input.GetMouseButtonDown(0) && availableKeys["LMB"]) || (Input.GetMouseButtonDown(1) && availableKeys["RMB"]))
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
