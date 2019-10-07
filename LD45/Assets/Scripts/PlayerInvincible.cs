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
            if (GetComponent<PlayerController>().keys.Count > 0)
                GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
            yield return new WaitForSeconds(.1f);
        }
        
    }
    
    public IEnumerator WaitTilCanGetHit()
    {
        player_hit = true;
        StartCoroutine(Blink());
        yield return new WaitForSeconds(3);
        GetComponent<SpriteRenderer>().enabled = true;
        player_hit = false;
    }
}
