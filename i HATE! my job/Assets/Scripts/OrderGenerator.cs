using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderGenerator : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> ingredients = new List<GameObject>();
    [SerializeField]
    public List<GameObject> customerOrder = new List<GameObject>();
    [SerializeField]
    public System.Random ingredientTotal = new System.Random();

    public bool completedOrder = false;

    // Use this for initialization

    private void Awake()
    {
        int total = ingredientTotal.Next(1, ingredients.Count);

        for (int i = 1; i <= total; i++)
        {
            GameObject newIngredient = ingredients[Random.Range(0, ingredients.Count)];
            customerOrder.Add(newIngredient);
        }
    }

    void Start ()
    {
        
    }
    
    // Update is called once per frame
    void Update ()
    {

    }

    public void newOrder()
    {
        int total = ingredientTotal.Next(1, ingredients.Count);

        for (int i = 1; i <= total; i++)
        {
            GameObject newIngredient = ingredients[Random.Range(0, ingredients.Count)];
            customerOrder.Add(newIngredient);
        }
    }
}
