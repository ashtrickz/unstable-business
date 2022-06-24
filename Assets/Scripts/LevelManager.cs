using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private AudioSource audioSource;
    private string currentLevel;

    void Awake() => DontDestroyOnLoad(this.gameObject);

    public void LevelChange(int levelId)
    {
        Time.timeScale = 1;
        switch (levelId)
        {
            case 0:
                SceneManager.LoadScene("MainMenu");
                Destroy(gameObject);
                break;
            case 1:
                SceneManager.LoadScene("Level1");
                break;
            case 2:
                SceneManager.LoadScene("Level2");
                break;
            case 3:
                SceneManager.LoadScene("Level3");
                break;
        }
    }

    public void ToggleMusic()
    {
        if (audioSource.mute == false)
            audioSource.mute = true;
        else audioSource.mute = false;
    }
}
