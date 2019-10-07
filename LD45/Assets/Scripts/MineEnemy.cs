using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineEnemy : EnemyMaster
{
    [SerializeField]
    MineType type;

    [SerializeField]
    float movement_speed;

    [SerializeField]
    float bullet_speed;
    [SerializeField]
    GameObject bullet_pref;

    [SerializeField]
    float seconds_til_explosion;

    bool detonate = false;
    Animator anim;

    
    // Start is called before the first frame update
    void Start()
    {
        health_bar = gameObject.GetComponent<Slider>();
        target = GameObject.FindGameObjectWithTag("Player");
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("is_following", false);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        Follow_Player();
        if (is_dead)
            StartCoroutine(Wait_Til_Explosion());
    }

    void Follow_Player()
    {
        if (Mathf.Abs(Vector2.Distance(transform.position, target.transform.position)) > distance_til_stop && !detonate)
        {
            transform.position += (target.transform.position - transform.position).normalized * movement_speed;
        }
        else
        {
            detonate = true;
            if (!is_attacking)
                is_dead = true;
        }
    }
    IEnumerator Wait_Til_Explosion()
    {
        anim.SetBool("is_following", true);
        yield return new WaitForSeconds(seconds_til_explosion);
        Shoot_Bullets();
        
    }
    void Shoot_Bullets()
    {
        is_attacking = true;
        if(type == MineType.GREY)
        {
            bullet_pref = Instantiate(bullet_pref, transform.position, Quaternion.identity);
            bullet_pref.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1f) * bullet_speed;
            bullet_pref = Instantiate(bullet_pref, transform.position, Quaternion.identity);
            bullet_pref.GetComponent<Rigidbody2D>().velocity = new Vector2(1f, 0) * bullet_speed;
            bullet_pref = Instantiate(bullet_pref, transform.position, Quaternion.identity);
            bullet_pref.GetComponent<Rigidbody2D>().velocity = new Vector2(-1f, 0) * bullet_speed;
            bullet_pref = Instantiate(bullet_pref, transform.position, Quaternion.identity);
            bullet_pref.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1f) * bullet_speed;
        }
        else if(type == MineType.GREEN)
        {
            bullet_pref = Instantiate(bullet_pref, transform.position, Quaternion.identity);
            bullet_pref.GetComponent<Rigidbody2D>().velocity = new Vector2(1f, 1f) * bullet_speed;
            bullet_pref = Instantiate(bullet_pref, transform.position, Quaternion.identity);
            bullet_pref.GetComponent<Rigidbody2D>().velocity = new Vector2(1f, -1f) * bullet_speed;
            bullet_pref = Instantiate(bullet_pref, transform.position, Quaternion.identity);
            bullet_pref.GetComponent<Rigidbody2D>().velocity = new Vector2(-1f, -1f) * bullet_speed;
            bullet_pref = Instantiate(bullet_pref, transform.position, Quaternion.identity);
            bullet_pref.GetComponent<Rigidbody2D>().velocity = new Vector2(-1f, 1f) * bullet_speed;
        }
        else if(type == MineType.RED)
        {
            bullet_pref = Instantiate(bullet_pref, transform.position, Quaternion.identity);
            bullet_pref.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1f) * bullet_speed;
            bullet_pref = Instantiate(bullet_pref, transform.position, Quaternion.identity);
            bullet_pref.GetComponent<Rigidbody2D>().velocity = new Vector2(1f, 0) * bullet_speed;
            bullet_pref = Instantiate(bullet_pref, transform.position, Quaternion.identity);
            bullet_pref.GetComponent<Rigidbody2D>().velocity = new Vector2(-1f, 0) * bullet_speed;
            bullet_pref = Instantiate(bullet_pref, transform.position, Quaternion.identity);
            bullet_pref.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1f) * bullet_speed;
            bullet_pref = Instantiate(bullet_pref, transform.position, Quaternion.identity);
            bullet_pref.GetComponent<Rigidbody2D>().velocity = new Vector2(1f, 1f) * bullet_speed;
            bullet_pref = Instantiate(bullet_pref, transform.position, Quaternion.identity);
            bullet_pref.GetComponent<Rigidbody2D>().velocity = new Vector2(1f, -1f) * bullet_speed;
            bullet_pref = Instantiate(bullet_pref, transform.position, Quaternion.identity);
            bullet_pref.GetComponent<Rigidbody2D>().velocity = new Vector2(-1f, -1f) * bullet_speed;
            bullet_pref = Instantiate(bullet_pref, transform.position, Quaternion.identity);
            bullet_pref.GetComponent<Rigidbody2D>().velocity = new Vector2(-1f, 1f) * bullet_speed;
        }

        Die();

    }

    
}
