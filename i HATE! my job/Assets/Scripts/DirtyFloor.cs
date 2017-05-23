using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyFloor : MonoBehaviour
{
    public GameObject scoreObject;
    Score score;

    // Use this for initialization
    void Start ()
    {
        score = scoreObject.GetComponent<Score>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Cheese")
        {
            score.subtractScore(25);
        }

        if (c.gameObject.tag == "Lettuce")
        {
            score.subtractScore(25);
        }

        if (c.gameObject.tag == "Patty")
        {
            score.subtractScore(25);
        }

        if (c.gameObject.tag == "Pickle")
        {
            score.subtractScore(25);
        }

        if (c.gameObject.tag == "Tomato")
        {
            score.subtractScore(25);
        }
    }
}
