using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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
    public float TravelSpeed = 10;

    [Header("Others")]
    public ParticleSystem WorldParticles;
    public Transform AimTarget;
    public CinemachineVirtualCamera PlayerCamera;

    private float playerPositionH;
    private float playerPositionV;
    private Transform playerModel;

    private bool travelBoostState = false;
    private bool travelSlowState = false;
    private float currentTravelSpeed;
    private float zoomSpeed = 10;


    

    private void Awake(){
        playerModel = transform.GetChild(0);
    }

    [System.Obsolete]
    void Update(){
        processInput();
        movePlayer();
    }


    [System.Obsolete]
    private void movePlayer(){
        moveArround(playerPositionH, playerPositionV, MovementSpeed);
        rotationLook(playerPositionH, playerPositionV, LookSpeed);
        horizontalLean(playerModel, playerPositionH, 50, .1f);
        travelForward();

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
        if (Input.GetKey(KeyCode.Q)) travelBoostState = true; else travelBoostState = false;
        if (Input.GetKey(KeyCode.E)) travelSlowState = true; else travelSlowState = false;
            


    }

    private void moveArround(float x, float y, float speed){
        transform.localPosition += new Vector3(x, y, 0) * speed * Time.deltaTime;
        lockPositionInsideCamera();

    }


    [System.Obsolete]
    private void travelForward(){
        if (travelBoostState) travelAccelerate(); else if (travelSlowState) travelBrake(); else travelNormal();
        transform.parent.position += new Vector3(0, 0, currentTravelSpeed) * Time.deltaTime;
        
    }

    [System.Obsolete]
    private void travelAccelerate(){
        currentTravelSpeed = TravelSpeed * 3;
        WorldParticles.playbackSpeed = 6;

        if (PlayerCamera.m_Lens.FieldOfView < 100)
            PlayerCamera.m_Lens.FieldOfView += zoomSpeed * Time.deltaTime * 3;

    }

    [System.Obsolete]
    private void travelBrake(){
        currentTravelSpeed = TravelSpeed / 2;
        WorldParticles.playbackSpeed = 1;
        

        if (PlayerCamera.m_Lens.FieldOfView > 30)
            PlayerCamera.m_Lens.FieldOfView -= zoomSpeed * Time.deltaTime ;
    }

    [System.Obsolete]
    private void travelNormal(){
        currentTravelSpeed = TravelSpeed;
        WorldParticles.playbackSpeed = 2;
        PlayerCamera.m_Lens.FieldOfView = 60;


    }

    private void lockPositionInsideCamera() {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);

    }

    private void rotationLook(float h, float v, float speed){
        AimTarget.parent.position = Vector3.zero;
        AimTarget.localPosition = new Vector3(h, v, 1);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(AimTarget.position), Mathf.Deg2Rad * speed * Time.deltaTime);
    }

    
    private void horizontalLean(Transform target, float axis, float leanLimit, float lerpTime){
        Vector3 targetEulerAngels = target.localEulerAngles;
        target.localEulerAngles = new Vector3(targetEulerAngels.x, targetEulerAngels.y, Mathf.LerpAngle(targetEulerAngels.z, axis * leanLimit, lerpTime));
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(AimTarget.position, .5f);
        Gizmos.DrawSphere(AimTarget.position, .15f);

    }

}
