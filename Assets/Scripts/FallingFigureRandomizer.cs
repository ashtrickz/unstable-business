using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FallingFigureRandomizer : MonoBehaviour
{

    [SerializeField] private Material material;

    void Start()
    {
        transform.rotation = Random.rotation;
        material.color = new Color(
            Random.Range(0f, 1f), 
            Random.Range(0f, 1f), 
            Random.Range(0f, 1f)
        );
        Destroy(gameObject, 3f);
    }

}
