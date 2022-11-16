using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ragdollAnimator : MonoBehaviour
{
    public bool headIdle = true;
    public bool legsIdle = true;
    public ConfigurableJoint headJoint;
    public ConfigurableJoint legRightJoint;
    public ConfigurableJoint legLeftJoint;

    void Update()
    {
        if (headIdle)
        {
            StartCoroutine(HeadIdle());
        }
    }

    IEnumerator HeadIdle()
    {
        headIdle = false;
        headJoint.targetRotation = Quaternion.Euler(Random.Range(-5.0f, 5.0f), Random.Range(-50.0f, 50.0f), Random.Range(-5.0f, 5.0f));
        yield return new WaitForSeconds(Random.Range(1f, 5f));
        headIdle = true;
    }
}
