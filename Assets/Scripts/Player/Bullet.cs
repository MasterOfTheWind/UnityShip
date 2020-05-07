using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int TravelSpeed = 10;

    void Update(){
        travel();
    }

    private void travel(){
        transform.position += new Vector3(0, 0, TravelSpeed) * Time.deltaTime;
    }
}
