using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderPreview : MonoBehaviour
{
	[Header("Variables")]
	[SerializeField]
	public RawImage[] screens;
	public Texture[] images;

	public GameObject orderHolder;
	OrderGenerator order;

	private void Awake()
	{
		order = orderHolder.GetComponent<OrderGenerator>();

		screens[0].enabled = false;
		screens[1].enabled = false;
		screens[2].enabled = false;
		screens[3].enabled = false;
		screens[4].enabled = false;
	}

	// Use this for initialization
	void Start ()
	{
        showOrder();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (order.completedOrder == true)
        {
            showOrder();
            order.completedOrder = false;
        }
	}

    public void showOrder()
    {
        for (int i = 0; i <= order.customerOrder.Count; i++)
        {
            if (order.customerOrder[i].tag == ("Cheese"))
            {
                screens[i].enabled = true;
                screens[i].texture = images[0];
            }

            if (order.customerOrder[i].tag == ("Lettuce"))
            {
                screens[i].enabled = true;
                screens[i].texture = images[1];
            }

            if (order.customerOrder[i].tag == ("Patty"))
            {
                screens[i].enabled = true;
                screens[i].texture = images[2];
            }

            if (order.customerOrder[i].tag == ("Pickle"))
            {
                screens[i].enabled = true;
                screens[i].texture = images[3];
            }

            if (order.customerOrder[i].tag == ("Tomato"))
            {
                screens[i].enabled = true;
                screens[i].texture = images[4];
            }
        }
    }
}
