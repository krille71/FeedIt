﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{

    private float timer = 0;
    private int scoreFromDishes = 0;
    private int score = 0;
    private TextMeshProUGUI scoreDisplay;
    private TextMeshProUGUI addDishScore;
    private InGameMenu menu;
    private float timeToShowdish = 0;

    private string scoreFill = "";

    // Start is called before the first frame update
    void Start()
    {
        menu = GameObject.FindGameObjectWithTag("InGameMenu").GetComponent<InGameMenu>();
        scoreDisplay = GameObject.FindGameObjectWithTag("ScoreDisplay").GetComponent<TextMeshProUGUI>();
        addDishScore = GameObject.FindGameObjectWithTag("ScoreAddedCounter").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        score = scoreFromDishes + (int)timer;
        
        if (score <= 9)
        {
            scoreFill = "0000";
        } else if (score <= 99)
        {
            scoreFill = "000";
        } else if (score <= 999)
        {
            scoreFill = "00";
        } else if (score <= 9999)
        {
            scoreFill = "0";
        }

        scoreDisplay.text = scoreFill + score.ToString();

        if(((int)timer - (int)timeToShowdish) > 2 && (int)timeToShowdish != 0){
            addDishScore.text = "";
            timeToShowdish = 0;
        }

        // Grey out text if paused
        if (menu.GameIsPaused)
        {
            scoreDisplay.color = new Color32(255, 255, 255, 155);
            addDishScore.color = new Color32(255, 255, 255, 155);
        }
        else
        {
            scoreDisplay.color = new Color32(255, 255, 255, 255);
            addDishScore.color = new Color32(255, 255, 255, 255);
        }
    }

    public void addScore(int dishScore)
    {
        timeToShowdish = timer; 
        addDishScore.text = "+" + dishScore.ToString();
        scoreFromDishes = scoreFromDishes + dishScore;
    }

    public int GetScore()
    {
        return score;
    }
}
