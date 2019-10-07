using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GunType
{
    PISTOL,
    SHOTGUN,
    AK47,
}

public class Gun : MonoBehaviour
{
    [SerializeField]
    float outerRadius = 2.5f;
    
    [SerializeField]
    Transform droog;

    [SerializeField]
    GunType type;

    [SerializeField]
    float initialBulletVel = 20.0f;

    [SerializeField]
    GameObject bulletPref;

    [SerializeField]
    float maxTimer = 5f;

    [SerializeField]
    float currTime;

    bool onCoolDown = false;

    [SerializeField]
    Transform start;

    float angle;


    [SerializeField] AudioClip PistolSound;
    [SerializeField] AudioClip ShottySound;
    [SerializeField] AudioClip AKSound;
    private AudioSource AS;
    private float PitchDeviation;
    // Update is called once per frame
    private void Start()
    {
        PitchDeviation = Random.Range(-.1f, .1f);
        AS = GetComponent<AudioSource>();
    }
    void Update()
    {
        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Vector3 final = Vector3.zero;
        final.z = 0;

        if (mousePos.x > droog.position.x + outerRadius)
            final.x = droog.position.x + outerRadius;
        else if (mousePos.x < droog.position.x - outerRadius)
            final.x = droog.position.x - outerRadius;
        else
            final.x = mousePos.x;

        if (mousePos.y > droog.position.y + outerRadius)
            final.y = droog.position.y + outerRadius;
        else if(mousePos.y < droog.position.y - outerRadius)
            final.y = droog.position.y - outerRadius;
        else
            final.y = mousePos.y;

        transform.position = final;

        // Screen pos of gun
        Vector2 objScreenPos = Camera.main.WorldToViewportPoint(droog.position);

        // Screen pos of mouse
        Vector2 mouseScreenPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        // Angle between two positions
        angle = AngleBetween(mouseScreenPos, objScreenPos);
        
        if(angle <= 145f && angle >= -145f)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(180f, 0, angle));
        }

        if(onCoolDown)
            currTime += Time.deltaTime;

        if(currTime >= maxTimer)
        {
            currTime = 0f;
            onCoolDown = false;
        }

    }

    float AngleBetween(Vector3 left, Vector3 right)
    {
        return Mathf.Atan2(left.y - right.y, left.x - right.x) * Mathf.Rad2Deg;
    }

    public void Shoot()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = mouseWorldPos - new Vector2(transform.position.x, transform.position.y);
        Vector2 unnormDir = dir;
        dir.Normalize();
        
        if (!onCoolDown)
        {
            if (type == GunType.PISTOL)
            {
                GameObject g = Instantiate(bulletPref, start.position, Quaternion.Euler(0,0,angle));
                g.GetComponent<Bullet>().damage = 5;
                g.GetComponent<Rigidbody2D>().velocity = dir * initialBulletVel;
                // Play pistol sound

                AS.PlayOneShot(PistolSound);
                PitchDeviation = Random.Range(-.1f, .1f);
                AS.pitch = 1 + PitchDeviation;

                onCoolDown = true;
            }
            else if(type == GunType.SHOTGUN)
            {
                GameObject g = Instantiate(bulletPref, start.position, Quaternion.Euler(0,0,angle));
                g.GetComponent<Bullet>().damage = 7;
                g.GetComponent<Rigidbody2D>().velocity = dir * initialBulletVel;

                g = Instantiate(bulletPref, start.position, Quaternion.Euler(0,0,angle));
                g.GetComponent<Bullet>().damage = 7;
                Vector2 dir2 = new Vector2(unnormDir.x + Random.Range(-1f, 1f), unnormDir.y + Random.Range(-1f, 1f));
                dir2.Normalize();
                g.GetComponent<Rigidbody2D>().velocity = dir2 * initialBulletVel;

                g = Instantiate(bulletPref, start.position, Quaternion.Euler(0,0,angle));
                g.GetComponent<Bullet>().damage = 7;
                dir2 = new Vector2(unnormDir.x + Random.Range(-1f, 1f), unnormDir.y + Random.Range(-1f, 1f));
                dir2.Normalize();
                g.GetComponent<Rigidbody2D>().velocity = dir * initialBulletVel;

                g = Instantiate(bulletPref, start.position, Quaternion.Euler(0,0,angle));
                g.GetComponent<Bullet>().damage = 7;
                dir2 = new Vector2(unnormDir.x + Random.Range(-1f, 1f), unnormDir.y + Random.Range(-0.5f, 0.25f));
                dir2.Normalize();
                g.GetComponent<Rigidbody2D>().velocity = dir * initialBulletVel;

                g = Instantiate(bulletPref, start.position, Quaternion.Euler(0,0,angle));
                g.GetComponent<Bullet>().damage = 7;
                dir2 = new Vector2(unnormDir.x + Random.Range(-1f, 1f), unnormDir.y + Random.Range(-1f, 1f));
                dir2.Normalize();
                g.GetComponent<Rigidbody2D>().velocity = dir * initialBulletVel;

                // Play shotgun sound
                AS.PlayOneShot(ShottySound);
                PitchDeviation = Random.Range(-.1f, .1f);
                AS.pitch = 1 + PitchDeviation;

                onCoolDown = true;
            }
            else if(type == GunType.AK47)
            {
                GameObject g = Instantiate(bulletPref, start.position, Quaternion.Euler(0,0,angle));
                g.GetComponent<Bullet>().damage = 8;
                g.GetComponent<Rigidbody2D>().velocity = dir * initialBulletVel;
                // Play Ak47 sound

                AS.PlayOneShot(AKSound);
                PitchDeviation = Random.Range(-.1f, .1f);
                AS.pitch = 1 + PitchDeviation;

                onCoolDown = true;
            }
        }
    }
    

}
