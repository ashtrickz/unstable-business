using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerFinder : MonoBehaviour
{
    
    private LevelManager levelManager;
    
    void Start() =>
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();

    public void ManageLevel(int levelId) => levelManager.LevelChange(levelId);
}
