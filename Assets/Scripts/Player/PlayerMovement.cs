using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{

    [Header("Speed Parameters")]
    [Space]
    [Header("Movement Speed")]
    public float MovementSpeed = 18;
    public float LookSpeed = 10;

    [Header("Travel Speed")]
    public int FastSpeed = 30;
    public int NormalSpeed = 15;
    public int SlowSpeed = 7;
    public float TravelSpeed = 2;

    public Transform aimTarget;

    private float playerPositionH;
    private float playerPositionV;
    private Transform playerModel;


    private void Awake(){
        playerModel = transform.GetChild(0);
    }

    void Update(){
        processInput();
        movePlayer();
    }

    private void movePlayer(){
        moveArround(playerPositionH, playerPositionV, MovementSpeed);
        rotationLook(playerPositionH, playerPositionV, LookSpeed);
        horizontalLean(playerModel, playerPositionH, 50, .1f);

    }

    private void processInput(){
        mapAxisToPosition();
        processButtons();
        
    }

    private void mapAxisToPosition(){
        playerPositionH = Input.GetAxis("Horizontal");
        playerPositionV = Input.GetAxis("Vertical");
    }

    private void processButtons(){

    }

    private void moveArround(float x, float y, float speed){
        transform.localPosition += new Vector3(x, y, 0) * speed * Time.deltaTime;
        lockPositionInsideCamera();

    }

    private void lockPositionInsideCamera() {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);

    }

    private void rotationLook(float h, float v, float speed){
        aimTarget.parent.position = Vector3.zero;
        aimTarget.localPosition = new Vector3(h, v, 1);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(aimTarget.position), Mathf.Deg2Rad * speed * Time.deltaTime);
    }

    
    private void horizontalLean(Transform target, float axis, float leanLimit, float lerpTime){
        Vector3 targetEulerAngels = target.localEulerAngles;
        target.localEulerAngles = new Vector3(targetEulerAngels.x, targetEulerAngels.y, Mathf.LerpAngle(targetEulerAngels.z, -axis * leanLimit, lerpTime));
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(aimTarget.position, .5f);
        Gizmos.DrawSphere(aimTarget.position, .15f);

    }

}
