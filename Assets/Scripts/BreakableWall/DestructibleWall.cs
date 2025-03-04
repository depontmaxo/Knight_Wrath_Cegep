using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleWall : MonoBehaviour
{

    [SerializeField] private GameObject _effect;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {


        if (other.tag == "Attack")
        {
            StartCoroutine(destructWall());
        }
    }
    IEnumerator destructWall()
    {
        _effect.SetActive(true);
        yield return new WaitForSeconds(4f);
        Destroy(this.gameObject);
    }

}


