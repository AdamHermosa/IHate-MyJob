using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	[Header("Score Handlers")]
	public int playerScore;
	public GameObject scoreHolder;
	public Text scoreText;

	// Use this for initialization
	void Start ()
	{
		playerScore = 0;
		scoreHolder = GameObject.FindGameObjectWithTag("ScoreScreen");
		scoreText = scoreHolder.GetComponent<Text>();

		scoreText.text = playerScore.ToString();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public int addScore(int mod)
	{
		playerScore = playerScore + mod;
		scoreText.text = playerScore.ToString();

        return playerScore;
	}

	public int subtractScore(int mod)
	{
        playerScore = playerScore - mod;
        scoreText.text = playerScore.ToString();

        return playerScore;
    }

    public void pushString()
    {
        scoreText.text = playerScore.ToString();
    }
}
