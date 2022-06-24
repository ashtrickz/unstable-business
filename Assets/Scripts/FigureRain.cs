using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureRain : MonoBehaviour
{

    [SerializeField] private GameObject[] fallingFigures = new GameObject[3];

    void Start() => StartCoroutine(StartRain());
    
    IEnumerator StartRain()
    {
        int figureId = Random.Range(0, 3);
        Vector3 spawnPosition = new Vector3(Random.Range(-4f, 4f), 10f, -1);
        Instantiate(fallingFigures[figureId], spawnPosition, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(StartRain());
    }
    
}
