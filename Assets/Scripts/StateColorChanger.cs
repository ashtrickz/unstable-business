using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class StateColorChanger : MonoBehaviour
{

    [SerializeField] private Material figureMaterial;
    private Color previousColor;

    void Start() =>
        figureMaterial.color = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f)
        );

    public void ChangeStateColor(int stateId)
    {
        switch (stateId)
        {
            case 0: //On Let
                figureMaterial.color = previousColor;
                break;
            case 1://On Grab
                previousColor = figureMaterial.color;
                figureMaterial.color = Color.red;
                break;
            case 2://On Land
                previousColor = figureMaterial.color;
                figureMaterial.color = Color.blue;
                break;            
            case 3://On Succeed
                previousColor = figureMaterial.color;
                figureMaterial.color = Color.green;
                break;
        }
    }
}
