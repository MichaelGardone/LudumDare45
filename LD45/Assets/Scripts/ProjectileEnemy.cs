using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileEnemy : EnemyMaster
{

    [SerializeField]
    float movement_speed;

    [SerializeField]
    float seconds_until_next_shot;

    [SerializeField]
    float bullet_speed;

    [SerializeField]
    GameObject bullet_pref;

    [SerializeField]
    float seconds_until_fire;
    Animator anim;
    bool hover_down = false;
    bool hold = false;

    bool on_cooldown = false;

    int courtines_that_exist = 0;
    // Start is called before the first frame update
    void Start()
    {
        health_bar = gameObject.GetComponent<Slider>();
        target = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //print(hover_down);
        FollowPlayer();
        if (is_dead)
            Die();
        GetAnims();
    }
    void FollowPlayer()
    {
        if (Mathf.Abs(Vector2.Distance(transform.position, target.transform.position)) > distance_til_stop)
        {
            is_attacking = false;
            transform.position += (target.transform.position - transform.position).normalized * movement_speed;
            
        }
        else
        {
            if (courtines_that_exist == 0)
                StartCoroutine(StartFiring());
        }
    }

    float AngleBetween(Vector3 left, Vector3 right)
    {
        return Mathf.Atan2(left.y - right.y, left.x - right.x) * Mathf.Rad2Deg;
    }
    void GetAnims()
    {

        float angle = AngleBetween(target.transform.position, transform.position);
        // East
        if (angle >= -45.001f && angle <= 45.0f)
        {
            anim.SetBool("east", true);
            anim.SetBool("north", false);
            anim.SetBool("south", false);
            anim.SetBool("west", false);
        }
        // North
        else if (angle >= 45.001f && angle <= 135.0f)
        {
            anim.SetBool("east", false);
            anim.SetBool("north", true);
            anim.SetBool("south", false);
            anim.SetBool("west", false);
        }
        // West
        else if (angle >= 135.001f || angle <= -135.001f)
        {
            anim.SetBool("east", false);
            anim.SetBool("north", false);
            anim.SetBool("south", false);
            anim.SetBool("west", true);
        }
        // South
        else if (angle >= -135.0f && angle <= -45.0f)
        {
            anim.SetBool("east", false);
            anim.SetBool("north", false);
            anim.SetBool("south", true);
            anim.SetBool("west", false);
        }
    }
    /*
    IEnumerator Hover()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.58f);
            break;
        }
        if (!hover_down)
            StartCoroutine(Wait());
        else
        {
            hover_down = false;
            StartCoroutine(Hover());
        }
            
        
    }
    IEnumerator Wait()
    {
        hold = true;
        while (true)
        {
            yield return new WaitForSeconds(.91f);
            print("Hover Down");
            hold = false;
            hover_down = true;
            break;
        }
        StartCoroutine(Hover());
    }
    */

    IEnumerator WaitToFire()
    {
        
        yield return new WaitForSeconds(seconds_until_fire);
        is_attacking = false;
        
    }
    IEnumerator StartFiring()
    {
        is_attacking = true;
        courtines_that_exist = 1;
        while (true)
        {
            
            if (!is_attacking)
                break;
            Vector2 direction = (target.transform.position - transform.position).normalized;
            GameObject bullet = Instantiate(bullet_pref, transform.position + (Vector3)direction, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bullet_speed;
            yield return new WaitForSeconds(seconds_until_next_shot);
            if (!is_attacking)
                break;
        }
        courtines_that_exist = 0;

    }
}
