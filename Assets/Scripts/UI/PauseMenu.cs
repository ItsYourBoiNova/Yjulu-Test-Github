using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    bool paused;
    [SerializeField] GameObject pauseMenu;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
                ResumeGame();
            else
                PauseGame();
        }
    }
    public void PauseGame()
    {
        paused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }
    public void ResumeGame()
    {
        paused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
        Time.timeScale = 1;
    }
}
