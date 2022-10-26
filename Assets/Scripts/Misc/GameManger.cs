using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    public static GameManger instance;
    [SerializeField] TextMeshProUGUI distanceText, scoreText,finalScoreDisplay,addingScoreText;
    float score, distance;
    const float scoreAddingMilestone = 10;
    float scoreAddingMilestoneCounter;
    bool paused,gameOver;
    [SerializeField] GameObject pauseMenu,loseMenu;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver)
        {
            if (paused)
                ResumeGame();
            else
                PauseGame();
        }
        CheckIfShouldAddToScore();
        DistanceCounter();
        DisplayScores();
    }
    private void CheckIfShouldAddToScore()
    {
        scoreAddingMilestoneCounter += Time.deltaTime;
        if (scoreAddingMilestoneCounter >= scoreAddingMilestone)
        {
            AddScore(500);
            scoreAddingMilestoneCounter = 0;
        }
    }
    private void DistanceCounter()
    {
        distance += Time.deltaTime;
    }
    private void DisplayScores()
    {
        distanceText.text = distance.ToString("f0");
        scoreText.text = score.ToString("f0");
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

    public void GameOver()
    {
        gameOver = true;
        loseMenu.SetActive(true);
        if(PlayerPrefs.GetFloat("HighScore",0) > score)
        {
            finalScoreDisplay.text = "Final Score:" + score + "\nHigh Score:" + PlayerPrefs.GetFloat("HighScore", 0);
        }
        else
        {
            finalScoreDisplay.text = "NEW HIGH SCORE:" + score;
            PlayerPrefs.SetFloat("HighScore", score);
        }    
        Time.timeScale = 0;
    }
    public void AddScore(float scoreToAdd)
    {
        score += scoreToAdd;
        StopAllCoroutines();
        StartCoroutine(ShowScoreAdding(scoreToAdd));
    }
    private IEnumerator ShowScoreAdding(float scoreToShow)
    {
        addingScoreText.gameObject.SetActive(true);
        addingScoreText.text = "Score+" + scoreToShow.ToString("f0");
        yield return new WaitForSeconds(1);
        addingScoreText.gameObject.SetActive(false);


    }
}
