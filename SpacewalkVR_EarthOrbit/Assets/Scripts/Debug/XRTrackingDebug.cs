using UnityEngine;
using Unity.XR.CoreUtils;

public class XRTrackingDebug : MonoBehaviour
{
    [SerializeField] private XROrigin xrOrigin;

    private void Update()
    {
        if (xrOrigin == null || xrOrigin.Camera == null)
            return;

        Transform cam = xrOrigin.Camera.transform;

        Debug.Log(
            $"CAMERA LOCAL: {cam.localPosition} | " +
            $"CAMERA WORLD: {cam.position} | " +
            $"CC Height: {xrOrigin.GetComponent<CharacterController>().height} | " +
            $"CC Center: {xrOrigin.GetComponent<CharacterController>().center}"
        );
    }
}