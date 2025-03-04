using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private int degat = 50;

    private void OnTriggerEnter(Collider other)
    {

        
        if (other.tag == "Ennemy")
        {
            other.GetComponent<Ennemy>().PrendreDegats(degat);
        }        
    }


}
