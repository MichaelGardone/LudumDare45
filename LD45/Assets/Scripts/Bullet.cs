using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField]
    float maxTimeUntilDisappear = 5.0f;

    [SerializeField]
    float currTime;

    void LateUpdate()
    {
        currTime += Time.deltaTime;

        if (currTime >= maxTimeUntilDisappear)
            Destroy(gameObject);
    }
}
