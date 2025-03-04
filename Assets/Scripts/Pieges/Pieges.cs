using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pieges : MonoBehaviour
{

    [SerializeField] private int _piege = 1;
    [SerializeField] private GameObject _flechePrefab = default;
    [SerializeField] private GameObject _firePrefab = default;
    [SerializeField] private float _cadenceTir = 3f;

    private float _canFire = -1.0f;
   



    // Update is called once per frame
    void FixedUpdate()
    {
       
        //Arrows
        if (_piege == 1)
        {
            if (Time.time > _canFire)
            {
                Instantiate(_flechePrefab, transform.position, transform.rotation);
                _canFire = Time.time + _cadenceTir;
            }
            //StartCoroutine(Fleches());

        }


        //Fire
        else if (_piege == 3)
        {
            if (Time.time > _canFire)
            {
                Instantiate(_firePrefab, transform.position, transform.rotation);
                _canFire = Time.time + _cadenceTir;
            }
        }

        //scie 1
        else if (_piege == 4)
        {
            transform.Rotate(10f, 0f, 0f, Space.Self);
        }


    }


    IEnumerator Fleches()
    {
        yield return new WaitForSeconds(5f);
        Instantiate(_flechePrefab, transform.position, Quaternion.identity);
    }

   
}
