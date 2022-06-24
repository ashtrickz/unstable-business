using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerFinder : MonoBehaviour
{
    
    private LevelManager _levelManager;
    
    void Start() =>
        _levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();

    public void ManageLevel(int levelId) => _levelManager.LevelChange(levelId);
}
