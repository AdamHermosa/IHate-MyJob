using UnityEngine;
using System.Collections;

public class handanimations : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObj;

    Animator anim;
    int Idle = Animator.StringToHash("Idle");
    int Point = Animator.StringToHash("Point");
    int GrabLarge = Animator.StringToHash("GrabLarge");
    int GrabSmall = Animator.StringToHash("GrabSmall");
    int GrabStickUp = Animator.StringToHash("GrabStickUp");
    int GrabStickFront = Animator.StringToHash("GrabStickFront");
    int ThumbUp = Animator.StringToHash("ThumbUp");
    int Fist = Animator.StringToHash("Fist");
    int Gun = Animator.StringToHash("Gun");
    int GunShoot = Animator.StringToHash("GunShoot");
    int PushButton = Animator.StringToHash("PushButton");
    int Spread = Animator.StringToHash("Spread");
    int MiddleFinger = Animator.StringToHash("MiddleFinger");
    int Peace = Animator.StringToHash("Peace");
    int OK = Animator.StringToHash("OK");
    int Phone = Animator.StringToHash("Phone");
    int Rock = Animator.StringToHash("Rock");
    int Natural = Animator.StringToHash("Natural");
    int Number3 = Animator.StringToHash("Number3");
    int Number4 = Animator.StringToHash("Number4");

    private SteamVR_Controller.Device Controller
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }

    void Start ()
    {
        anim = GetComponent<Animator>();
        trackedObj = GetComponentInParent<SteamVR_TrackedObject>();
    }

    void Update()
    {
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            anim.SetTrigger(Point);
        }

        else if (Controller.GetHairTriggerDown())
        {
            anim.SetTrigger(GrabLarge);
        }

        else if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            anim.SetTrigger(PushButton);
        }

        else if (Controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0)[0] > 0.7f 
            && Controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            anim.SetTrigger(ThumbUp);
        }

        else if (Controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0)[0] < -0.7f
            && Controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            anim.SetTrigger(MiddleFinger);
        }

        if (Controller.GetHairTriggerUp() || Controller.GetPressUp(SteamVR_Controller.ButtonMask.ApplicationMenu)
            || Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip) || Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            anim.SetTrigger(Idle);
        }
    }
  
}