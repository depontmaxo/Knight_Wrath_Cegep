using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEffect : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(wallParticules());
    }

    IEnumerator wallParticules()
    {
        
        yield return new WaitForSeconds(8f);
        Destroy(this.gameObject);
        
    }
}
