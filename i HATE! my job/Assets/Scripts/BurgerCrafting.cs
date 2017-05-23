using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BurgerCrafting : MonoBehaviour
{
    [Header("Burger Variables")]
    [SerializeField]
    public List<GameObject> burgerComponent = new List<GameObject>();
    [SerializeField]
    GameObject orderCheck;
    public List<GameObject> customerOrderCheck = new List<GameObject>();

    OrderGenerator gen;

    [Header("Score Handler")]
    public GameObject scoreObject;
    Score score;

    // Use this for initialization
    void Start ()
    {
        gen = orderCheck.GetComponent<OrderGenerator>();
        customerOrderCheck = gen.customerOrder;

        score = scoreObject.GetComponent<Score>();
    }
    
    // Update is called once per frame
    void Update ()
    {
        score.pushString();
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Burger")
        {
            c.transform.parent = gameObject.transform.parent;
            burgerComponent.Add(c.gameObject);

            score.addScore(50);
        }

        if (c.gameObject.tag == "Cheese")
        {
            c.transform.parent = gameObject.transform.parent;
            burgerComponent.Add(c.gameObject);

            score.addScore(50);
        }

        if (c.gameObject.tag == "Tomato")
        {
            c.transform.parent = gameObject.transform.parent;
            burgerComponent.Add(c.gameObject);

            score.addScore(50);
        }

        if (c.gameObject.tag == "Pickle")
        {
            c.transform.parent = gameObject.transform.parent;
            burgerComponent.Add(c.gameObject);

            score.addScore(50);
        }

        if (c.gameObject.tag == "Lettuce")
        {
            c.transform.parent = gameObject.transform.parent;
            burgerComponent.Add(c.gameObject);

            score.addScore(50);
        }

        if (c.gameObject.tag == "Top Bun")
        {
            c.transform.parent = gameObject.transform.parent;

            if (burgerComponent.Count == customerOrderCheck.Count)
            {
                foreach (GameObject obj in customerOrderCheck)
                {
                    if (customerOrderCheck.Contains(obj))
                    {
                        score.addScore(100);
                        gen.newOrder();
                        gen.completedOrder = true;  
                    }
                }
            }

            burgerComponent.Add(c.gameObject);
        }
    }

    void OnTriggerStay(Collider c)
    {
        if (c.gameObject.tag == "Top Bun")
        {
            if (c.gameObject.GetComponent<Food>().isHeld == true)
            {
                c.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }

            else
            {
                foreach (GameObject obj in burgerComponent)
                {
                    obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                }
            }
        }
        
    }

    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Burger" || c.gameObject.tag == "Cheese" || c.gameObject.tag == "Pickle" || c.gameObject.tag == "Tomato"
            || c.gameObject.tag == "Lettuce")
        {
            c.transform.parent = null;

            burgerComponent.Remove(c.gameObject);
        }

        if (c.gameObject.tag == "Top Bun")
        {
            c.transform.parent = null;

            foreach (GameObject obj in burgerComponent)
            {
                obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }

            burgerComponent.Remove(c.gameObject);
        }
    }
}
