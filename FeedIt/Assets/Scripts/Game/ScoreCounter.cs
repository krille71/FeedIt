using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{

    private float timer = 0;
    private int score = 0;
    private TextMeshProUGUI scoreDisplay;

    // Start is called before the first frame update
    void Start()
    {
        scoreDisplay = GameObject.FindGameObjectWithTag("ScoreDisplay").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        score = (int)timer;
        scoreDisplay.text = score.ToString();
    }

    public int GetScore()
    {
        return score;
    }
}
