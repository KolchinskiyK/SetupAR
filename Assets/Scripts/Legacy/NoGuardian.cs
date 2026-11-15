using UnityEngine;
using System.Collections;

// LEGACY script to disable Room Guardian -> dont work with Passthrough
public class NoGuardian : MonoBehaviour
{
    void Update()
    {
        OVRManager.instance.shouldBoundaryVisibilityBeSuppressed = true;

        Debug.Log(
        "Guardian suppressed: " +
        OVRManager.instance.isBoundaryVisibilitySuppressed

    );
    }
}