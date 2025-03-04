using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propulseur : MonoBehaviour
{

    private Transform player;
    [SerializeField] private float charge = 9000f;
    [SerializeField] private float speed = 3.5f;
    [SerializeField] private float range = 10f;
    [SerializeField] private GameObject _effect;
    private float temps = 0;
    private float distance;
    private Rigidbody _rb;
    private bool active = false;




    void Start()
    {
        player = GameObject.Find("Player").transform;
        temps = Time.time;
        _rb = GameObject.Find("Player").GetComponent<Rigidbody>();
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        distance = Vector3.Distance(transform.position, player.position);



        if (distance <= range)
        {
            Vector3 direction = player.position - transform.position;
            direction.Normalize();



            if (temps <= Time.time)
            {
                temps += speed;
                StartCoroutine(Pousser());
            }



            if (active == true)
            {
                //F=Kq/r^2
                _rb.AddForce(direction * (charge / (Mathf.Pow(distance, 2))), ForceMode.Force);
            }
        }



    }
    IEnumerator Pousser()
    {
        active = true;
        _effect.SetActive(true);
        yield return new WaitForSeconds(0.5f * speed);
        active = false;
        _effect.SetActive(false);
    }
    

}
