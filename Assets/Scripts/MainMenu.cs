using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private TMP_Text header;

    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject levelPickPanel;

    private LevelManager levelManager;
    
    [SerializeField] private float headerBlinkSpeed = 2f;
    private float elapsedTime;
    
    private bool goUp = true;

    void Start()
    {
        menuPanel.SetActive(true);
        levelPickPanel.SetActive(false);
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }
    
    void FixedUpdate()
    {
        if (goUp)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / headerBlinkSpeed;
            header.fontSize = Mathf.Lerp(84, 100, Mathf.SmoothStep(0, 1, percentageComplete));
        }
        else
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / headerBlinkSpeed;
            header.fontSize = Mathf.Lerp(100, 84, Mathf.SmoothStep(0, 1, percentageComplete));
        }
        if (header.fontSize == 100)
        {
            goUp = false;
            elapsedTime = 0;
        }

        if (header.fontSize == 84)
        {
            goUp = true;
            elapsedTime = 0;
        }
    }

    public void MenuSwitchButton()
    {
        if (menuPanel.activeSelf)
        {
            menuPanel.SetActive(false);
            levelPickPanel.SetActive(true);
        }
        else
        {
            menuPanel.SetActive(true);
            levelPickPanel.SetActive(false);
        }
    }
    
    public void QuitButton() => Application.Quit();

    public void LevelButton(int levelId) => levelManager.LevelChange(levelId);
}
