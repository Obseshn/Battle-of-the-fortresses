using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float liveTime = 10f;
    [SerializeField] private float Xrotation;

    private void OnAwake()
    {
        StartCoroutine(LiveTimeCounter(liveTime));
    }
    void Update()
    {
        Xrotation -= 35 * Time.deltaTime;
        transform.rotation = Quaternion.Euler(Xrotation, 0 ,0);
    }

    IEnumerator LiveTimeCounter(float lifeTimeInSec)
    {
        yield return new WaitForSeconds(lifeTimeInSec);
        Destroy(gameObject);
    }
}
