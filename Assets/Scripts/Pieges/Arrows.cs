using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrows : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;

    private Player _Player;

    private Rigidbody _rb;
  

    private void Awake()
    {
        _Player = FindObjectOfType<Player>();
        _rb = this.GetComponent<Rigidbody>();
    }
    private void Start()
    {
        Vector3 direction = new Vector3(3, 0.3f, 0);
        _rb.AddForce(direction*10, ForceMode.Impulse);
    }


    void FixedUpdate()
    {
        MoveArrow();
        _rb.AddForce(Vector2.down * 9.8f, ForceMode.Force);
    }


    /**************************************
    * Rôle: Gestion du mouvement de la flèche
    **************************************/
    private void MoveArrow()
    {
        //transform.Translate(Vector3.forward * Time.deltaTime * _speed);
    }

    /******************************************************
    * Rôle: Gestion du contact entre ennemies et la flèche
    ******************************************************/
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _Player.Dommage(50);
            Destroy(this.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Spawn"|| other.tag == "Death")
        {
            Destroy(gameObject);
        }

    }
}
