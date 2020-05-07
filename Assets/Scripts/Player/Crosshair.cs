using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{

    public Transform CannonReference;



    void Update(){
        transform.position = new Vector3(CannonReference.position.x, CannonReference.position.y, CannonReference.position.z + 100);
        transform.eulerAngles = new Vector3(0, -180, 0);
    }
}
