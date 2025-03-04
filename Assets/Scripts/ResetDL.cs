using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetDL : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        DirectionalLight.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);
    }

    [SerializeField] private GameObject DirectionalLight;



}