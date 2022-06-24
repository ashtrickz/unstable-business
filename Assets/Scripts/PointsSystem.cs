using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsSystem : MonoBehaviour
{

    [SerializeField] private int requiredPoints = 3;
    [SerializeField] private int acquiredPoints = 0;

    [SerializeField] private TMP_Text text;

    [SerializeField] private GameObject winMenu;
    
    public void GetPoints()
    {
        acquiredPoints++;
        ChangePoints();
    }

    public void LosePoints()
    {
        acquiredPoints--;
        ChangePoints();
    }

    private void ChangePoints()
    {
        text.text = "Points: " + acquiredPoints + "/" + requiredPoints;
        if (acquiredPoints == requiredPoints)
        {
            winMenu.SetActive(true);
            Time.timeScale = 0;
        }
            
    }

}
