using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitThenDie());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitThenDie()
    {
        yield return new WaitForSeconds(1.05f);
        Destroy(gameObject);
    }
}
