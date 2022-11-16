using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

public class grabbable : MonoBehaviour
{
    public SteamVR_Input_Sources inputController;
    public Vector3 objRotationOffset;
    public Vector3 objPositionOffset;
    public UnityEvent onPickUp;
    public UnityEvent onDrop;
}
