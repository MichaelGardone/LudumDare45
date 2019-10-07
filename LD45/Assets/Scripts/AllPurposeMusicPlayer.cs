using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPurposeMusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource AS;
    [SerializeField] float MaxVolume;
    [SerializeField] float FadeRate;
    bool isFading;
    void Start()
    {
        AS = GetComponent<AudioSource>();
        AS.volume = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFading && AS.volume < MaxVolume)
        {
            AS.volume += FadeRate * .1f * Time.deltaTime;
        }
    }
}
