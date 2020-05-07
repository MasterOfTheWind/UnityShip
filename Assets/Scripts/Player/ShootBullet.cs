using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{

    public GameObject Bullet;

    
    void Update(){
        processInput();
    }

    private void processInput(){
        if (Input.GetButtonDown("Fire1")) fireBullet();
    }

    private void fireBullet(){
        GameObject shootedBullet = Instantiate(Bullet, transform.position, transform.rotation);
        Destroy(shootedBullet, 1f);

    }
}
