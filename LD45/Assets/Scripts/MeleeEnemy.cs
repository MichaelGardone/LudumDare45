﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeEnemy : EnemyMaster
{

    [SerializeField]
    float movement_speed;

    [SerializeField]
    CircleCollider2D attack_radius;

    [SerializeField]
    GameObject spark_particle;

    MeleeAttack melee_attack;

    Animator anim;
    //timers*************************
    [SerializeField]
    float time_until_attack;
    float time_until_attack_timer = 0;
    [SerializeField]
    float attack_duration;
    float attack_duration_timer = 0;

    [SerializeField]
    float time_stunned;
    float time_stunned_timer = 0;

    [SerializeField]
    float attack_cooldown_duration;

    [SerializeField]
    EntityFacing facing = EntityFacing.EAST;
    float attack_cooldown_timer = 0;
    //Bools******************************
    bool is_preparing_attack = false;
    bool should_move = false;
    bool in_attack_phase = false;
    bool is_cooldown_phase = false;


    // Start is called before the first frame update
    void Start()
    {
        health_bar = gameObject.GetComponent<Slider>();
        target = GameObject.FindGameObjectWithTag("Player");
        attack_radius = GetComponentInChildren<CircleCollider2D>();
        melee_attack = GetComponentInChildren<MeleeAttack>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        spark_particle.transform.position = attack_radius.transform.position;
        should_move = !is_preparing_attack && !is_stunned && !is_attacking && !is_dead;
        in_attack_phase = is_preparing_attack || is_attacking || is_cooldown_phase;
        Follow_Player();
        Prepare_Attack();
        Check_For_Attack();
        Check_For_Stun();
        GetAnims();
        if (is_cooldown_phase)
        {
            attack_cooldown_timer += Time.deltaTime;
            if(attack_cooldown_timer > attack_cooldown_duration)
            {
                //print("Done With Cooldown");
                attack_cooldown_timer = 0;
                is_cooldown_phase = false;
                
            }
        }
        if (is_dead)
            Die();
    }

    void Follow_Player()
    {
        if(Mathf.Abs(Vector2.Distance(transform.position,target.transform.position)) > distance_til_stop && should_move)
        {
            anim.SetBool("move", true);
            transform.position += (target.transform.position - transform.position).normalized * movement_speed * Time.deltaTime;
        }
        else
        {
            anim.SetBool("move", false);
            if (!in_attack_phase && !is_dead)
            {
                //print("preparing attack");
                melee_attack.hitPlayer = false;
                is_preparing_attack = true;
                attack_radius.transform.position = transform.position;
                attack_radius.transform.position = attack_radius.transform.position+ (target.transform.position - transform.position).normalized*.5f;
            }
                
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
            facing = EntityFacing.EAST;
            anim.SetBool("east", true);
            anim.SetBool("north", false);
            anim.SetBool("south", false);
            anim.SetBool("west", false);
        }
        // North
        else if (angle >= 45.001f && angle <= 135.0f)
        {
            facing = EntityFacing.NORTH;
            anim.SetBool("east", false);
            anim.SetBool("north", true);
            anim.SetBool("south", false);
            anim.SetBool("west", false);
        }
        // West
        else if (angle >= 135.001f || angle <= -135.001f)
        {
            facing = EntityFacing.WEST;
            anim.SetBool("east", false);
            anim.SetBool("north", false);
            anim.SetBool("south", false);
            anim.SetBool("west", true);
        }
        // South
        else if (angle >= -135.0f && angle <= -45.0f)
        {
            facing = EntityFacing.SOUTH;
            anim.SetBool("east", false);
            anim.SetBool("north", false);
            anim.SetBool("south", true);
            anim.SetBool("west", false);
        }
    }

    void Prepare_Attack()
    {
        if (is_preparing_attack)
        {
            time_until_attack_timer += Time.deltaTime;
            
            if (time_until_attack_timer >= time_until_attack)
            {
                time_until_attack_timer = 0;
                is_preparing_attack = false;
                Attack_Player();
            }
        }
        
    }
    void Attack_Player()
    {
        //print("ATTACK");
        is_attacking = true;
        attack_radius.enabled = true;
        

    }

    /// <summary>
    /// Checks for attack, if it is attacking turn attack_duration_timer on
    /// </summary>
    void Check_For_Attack()
    {
        if (is_attacking)
        {
            attack_duration_timer += Time.deltaTime;
            if (attack_duration_timer > attack_duration)
            {
                //print("done attacking");
                attack_radius.enabled = false;
                is_attacking = false;
                attack_duration_timer = 0;
                is_cooldown_phase = true;

            }
        }
    }

    /// <summary>
    /// Checks for stun, if it is stunned turn time_stunned_timer on
    /// </summary>
    void Check_For_Stun()
    {
        if (is_stunned)
        {
            Get_Stunned();
            time_stunned_timer += Time.deltaTime;
            if (time_stunned_timer > time_stunned)
            {
                is_stunned = false;
                time_stunned_timer = 0;
            }
        }
    }

    void Get_Stunned()
    {
        attack_duration_timer = 0;
        time_until_attack_timer = 0;
        is_preparing_attack = false;
        is_attacking = false;
    }
}
