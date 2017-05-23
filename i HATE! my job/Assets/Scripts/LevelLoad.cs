using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoad : MonoBehaviour
{
    public Text entryText;
    public GameObject player;

    // Use this for initialization
    void Start ()
	{
        entryText.enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
	{

    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Paper")
        {
            StartCoroutine(LoadText(1));
            StartCoroutine(LoadLevel(3));
        }
    }

    private IEnumerator LoadLevel(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            AsyncOperation Async = Application.LoadLevelAsync("Building"); 
            //Application.LoadLevel("Building");
        }
    }

    private IEnumerator LoadText(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            entryText.enabled = true;
        }
    }
}
