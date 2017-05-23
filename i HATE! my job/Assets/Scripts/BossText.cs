using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossText : MonoBehaviour
{
	public Text entryText;
	public GameObject player;

	public bool textCheck = false;

	// Use this for initialization
	void Start ()
	{
		entryText.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (textCheck == true)
		{
			entryText.enabled = true;
		}
	}
}
