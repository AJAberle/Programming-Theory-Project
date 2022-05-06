using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExplosion : MonoBehaviour
{
    private float destroyDelay = 1;

    private void Start()
    {
        StartCoroutine(WaitToDestroy());
    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject); 
    }
}
