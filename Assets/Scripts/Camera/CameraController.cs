using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour{
    [Header("Target")]
    public Transform Target;

    [Space]

    [Header("Offset")]
    public Vector3 Offset = Vector3.zero;

    [Space]

    [Header("ScreenLimits")]
    public Vector2 ScreenLimits = new Vector2(5, 3);

    [Space]

    [Header("Smooth Damp Time")]
    [Range(0, 1)]
    public float SmoothTime;

    private Vector3 Velocity = Vector3.zero;

    private void Update(){
        followTarget(Target);
    }

    void LateUpdate(){
        Vector3 localPos = transform.localPosition;

        transform.localPosition = new Vector3(Mathf.Clamp(localPos.x, -ScreenLimits.x, ScreenLimits.x), Mathf.Clamp(localPos.y, -ScreenLimits.y, ScreenLimits.y), localPos.z);
    }

    private void followTarget(Transform t){
        Vector3 localPos = transform.localPosition;
        Vector3 targetLocalPos = t.transform.localPosition;
        transform.localPosition = Vector3.SmoothDamp(localPos, new Vector3(targetLocalPos.x + Offset.x, targetLocalPos.y + Offset.y, localPos.z), ref Velocity, SmoothTime);
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(-ScreenLimits.x, -ScreenLimits.y, transform.position.z), new Vector3(ScreenLimits.x, -ScreenLimits.y, transform.position.z));
        Gizmos.DrawLine(new Vector3(-ScreenLimits.x, ScreenLimits.y, transform.position.z), new Vector3(ScreenLimits.x, ScreenLimits.y, transform.position.z));
        Gizmos.DrawLine(new Vector3(-ScreenLimits.x, -ScreenLimits.y, transform.position.z), new Vector3(-ScreenLimits.x, ScreenLimits.y, transform.position.z));
        Gizmos.DrawLine(new Vector3(ScreenLimits.x, -ScreenLimits.y, transform.position.z), new Vector3(ScreenLimits.x, ScreenLimits.y, transform.position.z));
    }

}
