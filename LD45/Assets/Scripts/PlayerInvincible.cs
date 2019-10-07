using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvincible : MonoBehaviour
{
    public bool player_hit = false;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator Blink()
    {
        while (player_hit)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(.5f);
            GetComponent<SpriteRenderer>().enabled = true;
        }
        
    }
    
    public IEnumerator WaitTilCanGetHit()
    {
        player_hit = true;
        StartCoroutine(Blink());
        yield return new WaitForSeconds(3);
        player_hit = false;
    }
}
