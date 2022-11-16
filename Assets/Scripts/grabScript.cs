using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class grabScript : MonoBehaviour
{
    public SteamVR_Input_Sources inputController;
    public SteamVR_Action_Single squeezeForce;
    public float grabDetectionZone;
    public GameObject heldObj;
    public Collider handColl;

    void FixedUpdate()
    {
        GrabDetector();
    }

    void GrabDetector()
    {
        if (squeezeForce[inputController].axis >= 0.2f && !heldObj)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, grabDetectionZone);
            foreach (var hitCollider in hitColliders)
            {
                var scanObj = hitCollider.GetComponent<grabbable>();

                if (scanObj)
                {
                    heldObj = scanObj.gameObject;
                    Grab();
                    break;
                }
            }
        }
        if (squeezeForce[inputController].axis <= 0.01f && heldObj)
        {
            LetGo();
        }
    }

    void Grab()
    {
        handColl.enabled = false;
        var grabbable = heldObj.GetComponent<grabbable>();
        var heldObjRB = heldObj.GetComponent<Rigidbody>();
        grabbable.inputController = inputController;
        grabbable.onPickUp.Invoke();
        heldObjRB.isKinematic = true;
        heldObj.transform.parent = transform;
        heldObj.transform.position = transform.position + grabbable.objPositionOffset;
        heldObj.transform.localEulerAngles = transform.localEulerAngles + grabbable.objRotationOffset;
    }

    void LetGo()
    {
        handColl.enabled = true;
        var grabbable = heldObj.GetComponent<grabbable>();
        var heldObjRB = heldObj.GetComponent<Rigidbody>();
        grabbable.onDrop.Invoke();
        heldObjRB.isKinematic = false;
        heldObj.transform.parent = null;
        heldObj = null;
    }
}
