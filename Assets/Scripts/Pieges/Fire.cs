using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(fireSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator fireSpawn()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
