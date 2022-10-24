using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public static GameManger instance;
    [SerializeField] TextMeshProUGUI distanceText, scoreText;
    public float distance { get; set; }
    public float score { get; set; }
    const float scoreAddingMilestone = 250;
    float scoreAddingMilestoneCounter;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfShouldAddToScore();
        DistanceCounter();
        DisplayScores();
    }
    private void CheckIfShouldAddToScore()
    {
        scoreAddingMilestoneCounter += Time.deltaTime;
        if (scoreAddingMilestoneCounter >= scoreAddingMilestone)
        {
            score += 1000;
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
}
