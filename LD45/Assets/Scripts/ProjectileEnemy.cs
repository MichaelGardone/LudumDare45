using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileEnemy : EnemyMaster
{

    [SerializeField]
    private float shooting_range;
    
    // Start is called before the first frame update
    void Start()
    {
        health_bar = gameObject.GetComponent<Slider>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
