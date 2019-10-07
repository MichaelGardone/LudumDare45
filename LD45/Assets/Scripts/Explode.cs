using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] AudioClip explode;
    private AudioSource AS;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitThenDie());
        AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitThenDie()
    {
        //AS.PlayOneShot(explode);
        yield return new WaitForSeconds(.6f);
        Destroy(gameObject);
    }
}
