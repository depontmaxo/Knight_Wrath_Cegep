using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldDoor : MonoBehaviour
{

    [SerializeField] private GameObject door1;
    [SerializeField] private GameObject door2;
    [SerializeField] private GameObject door3;

    private Player _Player;

    private int bestScene = 0;
    private string highScene = "highScene";

    // Start is called before the first frame update
    void Start()
    {
        _Player = FindObjectOfType<Player>();

        bestScene = _Player.Getint(highScene);
        
        if (bestScene > 2)
        {
            door1.SetActive(false);
        }
        if (bestScene > 3)
        {
            door2.SetActive(false);
        }
        if (bestScene > 4)
        {
            door3.SetActive(false);
        }

    }

    
}
