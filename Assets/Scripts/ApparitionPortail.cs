using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApparitionPortail : MonoBehaviour
{
    [SerializeField] private GameObject _portail;
    
    void FixedUpdate()
    {
      
        if (GameObject.Find("Boss") == null)

        {

            _portail.SetActive(true);

        }

    }
}
