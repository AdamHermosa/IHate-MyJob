using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_PlayArea playArea;

    [Header("Laser Variables")]
    public GameObject laserPrefab;
    private GameObject laser;
    private Transform laserTransform;
    private Vector3 hitPoint;
    private Vector3 tempHit;

    [Header("Teleport Variables")]
    public Transform cameraRigTransform;
    public GameObject teleportReticlePrefab;
    private GameObject reticle;
    private Transform teleportReticleTransform;
    public Transform headTransform;
    public Vector3 teleportReticleOffset;
    public LayerMask teleportMask;
    public LayerMask everythingMask;
    private bool shouldTeleport;
    private float fadeSpeed = 0.0f;
    private Vector3 endPoint;
    private bool teleCheck = true;

    private SteamVR_Controller.Device Controller
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }
    
    void Start()
    {
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;

        playArea = GetComponent<SteamVR_PlayArea>();

        reticle = Instantiate(teleportReticlePrefab);
        teleportReticleTransform = reticle.transform;

        endPoint = cameraRigTransform.position;
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void ShowLaser(RaycastHit hit)
    {
        laser.SetActive(true);
        laser.transform.parent = trackedObj.transform;
        Vector3 offset = new Vector3();
        offset.x += 0.1f;
        offset.y += 0.1f;
        offset.z -= 0.1f;
        laserTransform.position = Vector3.Lerp(trackedObj.transform.position + offset, hitPoint, .5f);
        laserTransform.LookAt(hitPoint);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, hit.distance);

    }

    private void Teleport()
    {
        shouldTeleport = false;
        reticle.SetActive(false);
        Vector3 difference = cameraRigTransform.position - headTransform.position;
        difference.y = 0;

        if (cameraRigTransform.position != hitPoint && fadeSpeed <= 1.0f)
        {
           cameraRigTransform.position = Vector3.Lerp(cameraRigTransform.position, hitPoint, fadeSpeed);
        }
    }

    void DifferentMethod()
    {
        cameraRigTransform.position = Vector3.Lerp(cameraRigTransform.position, hitPoint, 0.1f);
    }

    // Update is called once per frame
    void Update ()
    {
        if (cameraRigTransform.position != endPoint)
        {
            DifferentMethod();

            if (Vector3.Distance(cameraRigTransform.position, endPoint) < 0.01f)
            {
                cameraRigTransform.position = endPoint;
                teleCheck = true;
            }
        }

        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Grip))
        {
            RaycastHit hit;

            if (teleCheck == true)
            {
                if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100, teleportMask))
                {
                    hitPoint = hit.point;
                    tempHit = hitPoint;
                    ShowLaser(hit);

                    reticle.SetActive(true);
                    teleportReticleTransform.position = hitPoint + teleportReticleOffset;
                    shouldTeleport = true;
                }

                else if (!Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100, everythingMask))
                {
                    laser.SetActive(false);

                    teleportReticleTransform.position = tempHit + teleportReticleOffset;
                    shouldTeleport = false;
                }
            }
        }

        else
        {
            laser.SetActive(false);
            reticle.SetActive(false);
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip) && shouldTeleport)
        {
            if (cameraRigTransform.position != hitPoint && fadeSpeed <= 1.0f)
            {
                endPoint = hitPoint;
                teleCheck = false;
            }
        }
    }
}
