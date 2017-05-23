using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PattyCooking : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField]
    private int pattyScore = 0;
    private float pattyThreshold = 0.0f;
    private int overtime = 0;
    private bool isCooking = false;
    public bool decook = false;

    [SerializeField]
    public GameObject scoreObject;
    Score score;
    public ParticleSystem smoke;


    private void Awake()
    {
        smoke.Stop();
    }

	// Use this for initialization
	void Start ()
    {
        score = scoreObject.GetComponent<Score>();
        smoke.transform.position = this.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        smoke.transform.position = this.transform.position;

        if (isCooking)
        {
            pattyThreshold += Time.deltaTime;

            if (pattyThreshold > 1.0f && pattyScore < 50 && overtime < 5)
            {
                pattyScore = pattyScore + 10;
                pattyThreshold = 0;
                overtime++;

                score.addScore(10);

                if (overtime >= 5)
                {
                    decook = true;
                }
            }

            if (decook == true)
            {
                if (pattyThreshold > 1.0f)
                {
                    pattyScore = pattyScore - 10;
                    pattyThreshold = 0;

                    score.subtractScore(10);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Stove")
        {
            isCooking = true;

            smoke.Play();
            smoke.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Stove")
        {
            isCooking = false;

            smoke.Stop();
            smoke.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }
}
