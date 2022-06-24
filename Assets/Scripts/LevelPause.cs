using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPause : MonoBehaviour
{
    
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject pauseButton;
    void Start()
    {
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void TogglePause()
    {
        if (pausePanel.activeSelf)
        {
            pausePanel.SetActive(false);
            pauseButton.SetActive(true);
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            pauseButton.SetActive(false);
        }
    }
    
}
