using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDeath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ArretEffect());
    }

    IEnumerator ArretEffect()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
}
