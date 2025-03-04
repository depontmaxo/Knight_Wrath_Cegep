using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeplacementScene : MonoBehaviour
{
    [SerializeField] private int _scene = 1;
    private Player _Player;

    private void Awake()
    {
        _Player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _Player.endScene();
            SceneManager.LoadScene(_scene);

        }
    }
}
