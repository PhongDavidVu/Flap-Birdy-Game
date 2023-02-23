using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringGunBullet : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject bulletPre;
    public float fireSpeed = 80;

    // slow down gun fire rate
    private float cd = 0.5f;
    private float fireDelay = 0.8f;

    public static Vector3 dir;
    void Update()
    {      
        if (Input.GetButtonDown("Fire1") && FPSControl.gunOn && Time.time > cd)
        {
            var bullet = Instantiate(bulletPre,spawnPoint.position,spawnPoint.rotation);
         
            // experiment with changing bullet speed
            bullet.GetComponent<Rigidbody>().velocity -= 2*spawnPoint.right * fireSpeed;
            dir = bullet.GetComponent<Rigidbody>().velocity;
            cd = Time.time + fireDelay;
        }
    }
}
