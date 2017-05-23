using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerClass : MonoBehaviour
{
    [Header("Hand Objects")]
    [SerializeField]
    private GameObject collidingObj;
    [SerializeField]
    private GameObject objInHand;

    private SteamVR_TrackedObject trackedObj;
    
    private SteamVR_Controller.Device Controller
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void SetCollidingObj(Collider c)
    {
        if (collidingObj || !c.GetComponent<Rigidbody>())
        {
            return;
        }

        collidingObj = c.gameObject;
    }

    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObj(other);
    }

    public void OnTriggerStay(Collider other)
    {
        SetCollidingObj(other);
    }

    public void OnTriggerExit(Collider other)
    {
        if (!collidingObj)
        {
            return;
        }

        collidingObj = null;
    }

    private void GrabObject()
    {
        objInHand = collidingObj;
        collidingObj = null;

        var joint = AddFixedJoint();
        joint.connectedBody = objInHand.GetComponent<Rigidbody>();
    }

    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    private void ReleaseObject()
    {
        if (GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());

            objInHand.GetComponent<Rigidbody>().velocity = Controller.velocity * 3;
            objInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity * 9.8f;
        }

        objInHand = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Controller.GetAxis() != Vector2.zero)
        {
            Debug.Log(gameObject.name + Controller.GetAxis());
        }

        if (Controller.GetHairTriggerDown())
        {
            if (collidingObj)
            {
                GrabObject();
            }

            Debug.Log(gameObject.name + "Trigger Press");
        }

        if (Controller.GetHairTriggerUp())
        {
            if (objInHand)
            {
                ReleaseObject();
            }

            Debug.Log(gameObject.name + "Trigger Release");
        }

        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log(gameObject.name + "Grip Press");
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log(gameObject.name + "Grip Release");
        }

        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            Debug.Log(gameObject.name + "Menu Button Press");
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            Debug.Log(gameObject.name + "Menu Button Release");
        }
    }
}
