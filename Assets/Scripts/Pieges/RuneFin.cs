using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneFin : MonoBehaviour
{

    [SerializeField] private GameObject _effect = default;
    [SerializeField] private int number = 0;
    private Player _Player;



    private void Awake()
    {
        _Player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _effect.SetActive(true);

        if (number == 1)
        {
            if (other.tag == "Player")
            {
                _Player.rune1 = true;
            }
        }
        if (number == 2)
        {
            if (other.tag == "Player")
            {
                _Player.rune2 = true;              
            }
        }
        if (number == 3)
        {
            if (other.tag == "Player")
            {
                _Player.rune3 = true;
            }
        }
        if (number == 4)
        {
            if (other.tag == "Player")
            {
                _Player.rune4 = true;
            }
        }
    }
}
