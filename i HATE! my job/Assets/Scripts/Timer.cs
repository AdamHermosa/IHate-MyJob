using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
	public Text timerMinutes;
	public Text timerSeconds;
    public Text GameOver;
    public GameObject player;
	public string minutesText;
	public string secondsText;

    public Vector3 newPos = new Vector3();

	public float seconds = 10;
	public float minutes = 0;

	public bool stopTimer = false;

	// Use this for initialization
	void Start ()
	{
        GameOver.enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
	{
		if (stopTimer == false)
		{
			seconds -= Time.deltaTime;

			if (seconds < 0)
			{
				minutes--;
				seconds = 60;
			}

			if (seconds < 0.3 && minutes == 0)
			{
				minutes = 0;
				seconds = 0;
				stopTimer = true;

                GameOver.enabled = true;
            }

			Mathf.Round(minutes);
			Mathf.Round(seconds);

			int secondsInt = (int)seconds;
			minutesText = minutes.ToString();
			secondsText = secondsInt.ToString();

			timerMinutes.text = minutesText + (": ");

            if (secondsText.Length == 1)
            {
                timerSeconds.text = ("0") + secondsText;
            }

            else
            {
                timerSeconds.text = secondsText;
            }
			
		}
        
    }
}
