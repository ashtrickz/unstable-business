using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsSystem : MonoBehaviour
{

    [SerializeField] private int requiredPoints = 3;
    [SerializeField] private int aquiredPoints = 0;

    [SerializeField] public TMP_Text text;
    
    public void GetPoints()
    {
        aquiredPoints++;
        ChangePoints();
    }

    public void LosePoints()
    {
        aquiredPoints--;
        ChangePoints();
    }

    private void ChangePoints()
    {
        text.text = "Points: " + aquiredPoints + "/" + requiredPoints;
    }

}
